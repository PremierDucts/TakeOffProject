using Autodesk.Fabrication.DB;
using Autodesk.Fabrication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using Serilog;
using System.Diagnostics;
using System.Net.Sockets;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class FileUltil
    {
         static readonly Serilog.ILogger log = SerilogClass._log;
         readonly string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Job.Info.FileName);
         readonly string pathDirectoryContainsFile = Path.GetDirectoryName(Job.Info.FileName);
         readonly string fileName = Path.GetFileName(Job.Info.FileName);
        public  void HanldeAndExportData()
        {
            var csv = new StringBuilder();
            string filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "CSV Output");
            var temp = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            CreateRandomDirectory(filePath);
            filePath = Path.Combine(filePath,fileNameWithoutExtension +".csv");
           
            List<DataObject> list_dataObject = new List<DataObject>();
            //File.Delete(filePath);
            csv.AppendLine(initCSV());



            string currentDateTime = DateTime.Now.ToString("d/M/yyyy?HH:mm") ;
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AddInData");
            UserData userData = new UserData();
            string pathContainUserDataFile = Path.Combine(appFolder, "userdata.json");
            if (File.Exists(pathContainUserDataFile))
            {
                string json = File.ReadAllText(pathContainUserDataFile);
                userData =  JsonConvert.DeserializeObject<UserData>(json);
            }

            try
            {
                foreach (Item itm in Job.Items)
                {
                    DataObject dataObject = new DataObject();
                    //Get operatorId from local file UserData local
                    dataObject.operatorId = userData.Username;

                    //Split current date into date & time
                    dataObject.jobday = currentDateTime.Split('?')[0];
                    dataObject.jobtime = currentDateTime.Split('?')[1];

                    //Job No. - CustomData field
                    CustomItemData data = itm.CustomData[1];
                    CustomDataStringValue myCustomData = data as CustomDataStringValue;
                    dataObject.jobno = myCustomData.Value.ToString();
                    dataObject.jobno = RemoveSillyMarks(dataObject.jobno);

                    //Drawing No. - CustomData field
                    data = itm.CustomData[2];
                    myCustomData = data as CustomDataStringValue;
                    dataObject.drawingno = myCustomData.Value.ToString();
                    dataObject.drawingno = RemoveSillyMarks(dataObject.drawingno);
                    
                    //Handle
                    dataObject.handle = itm.Handle.ToString("X");

                    //Item No.
                    dataObject.itemno = itm.Number.ToString();
                    dataObject.itemno = RemoveSillyMarks(dataObject.itemno);

                    //Insulation
                    if (itm.Insulation != null)
                    {
                        dataObject.insulation = itm.Insulation.Status.ToString();
                    }
                    else
                    {
                        dataObject.insulation = "off";
                    }

                    //Material - Galvenized
                    if (itm.Gauge != null)
                    {
                        dataObject.galvenized = itm.Gauge.Thickness.ToString();
                    }
                    else
                    {
                        dataObject.galvenized = "None";
                    }

                    //Notes
                    dataObject.notes = itm.Notes.ToString();
                    dataObject.notes = RemoveSillyMarks(dataObject.notes);

                    //Weight
                    double tempWeight = itm.Weight;
                    tempWeight = Math.Round(tempWeight, 2);
                    dataObject.weight = tempWeight.ToString();

                    //Status
                    dataObject.status = itm.Status.Name.ToString();

                    //Qty
                    dataObject.qty = itm.Quantity.ToString();

                    //Cut Type
                    dataObject.cuttype = itm.CutType.ToString();

                    //CID
                    dataObject.cid = itm.CID.ToString();

                    //Description
                    dataObject.description = itm.Description.ToString();
                    dataObject.description = RemoveSillyMarks(dataObject.description);

                    //Double Wall
                    dataObject.doublewall = itm.IsDoubleWall.ToString();

                    //Path Id by call api to save FileInfo and get unique id
                    dataObject.pathid = 0;
                    

                    //Insulation Area
                    dataObject.insulationarea = Math.Round(itm.Insulation.Area/1000000,2);

                    //Metal Area
                    dataObject.metalarea = Math.Round(itm.Area,2);

                    //Bought Out flag
                    dataObject.boughtout = itm.BoughtOut.ToString();

                    //Linear meter null
                    dataObject.linearmeter = null;

                    //Section idex & section Description
                    if (itm.Section != null)
                    {
                        //Section color index
                        dataObject.sectionindex = itm.Section.Index.ToString();

                        //Section color description
                        dataObject.sectiondescription = itm.Section.Description;
                    }
                    else
                    {
                        //Section color index
                        dataObject.sectionindex = "NULL";

                        //Section color description
                        dataObject.sectiondescription = "NULL";
                    }

                    //Prefix string
                    data = itm.CustomData[0];
                    myCustomData = data as CustomDataStringValue;
                    dataObject.prefixstring = myCustomData.Value.ToString();
                    dataObject.prefixstring = RemoveSillyMarks(dataObject.prefixstring);

                    //Insulation Spec - Thickness only
                    dataObject.insulationSpec = itm.Insulation.Gauge.Thickness.ToString();

                    // widthDim - depthDim - lengthangle -
                    List<string> CIDexclusions = new List<string>();
                    CIDexclusions.Add("0");
                    if (!CIDexclusions.Contains(dataObject.cid))
                    {
                        if (itm.Dimensions.Count > 0)
                        {
                            dataObject.widthDim = Math.Round(itm.Dimensions[0].Value, 0).ToString();
                        }
                        else
                        {
                            dataObject.widthDim = "null";
                        }

                        if (itm.Dimensions.Count > 1)
                        {
                            dataObject.depthDim = Math.Round(itm.Dimensions[1].Value, 0).ToString();
                        }
                        else
                        {
                            dataObject.depthDim = "null";
                        }

                        if (itm.Dimensions.FirstOrDefault(x => x.Name == "Length") != null)
                        {
                            //_builder.AppendLine("-Length/Angle: " + itm.Dimensions.FirstOrDefault(x => x.Name == "Length").Value);
                            dataObject.lengthangle = Math.Round(itm.Dimensions.FirstOrDefault(x => x.Name == "Length").Value, 0).ToString();
                        }
                        else if (itm.Dimensions.FirstOrDefault(x => x.Name == "Angle") != null)
                        {
                            //_builder.AppendLine("-Length/Angle: " + itm.Dimensions.FirstOrDefault(x => x.Name == "Angle").Value);
                            dataObject.lengthangle = Math.Round(itm.Dimensions.FirstOrDefault(x => x.Name == "Angle").Value, 0).ToString();
                        }
                        else if (itm.Dimensions.FirstOrDefault(x => x.Name == "Height") != null)
                        {
                            //_builder.AppendLine("-Length/Angle: " + itm.Dimensions.FirstOrDefault(x => x.Name == "Height").Value);
                            dataObject.lengthangle = Math.Round(itm.Dimensions.FirstOrDefault(x => x.Name == "Height").Value, 0).ToString();
                        }
                        else if ((itm.Dimensions.FirstOrDefault(x => x.Name == "Right Angle") != null) && (itm.Dimensions.FirstOrDefault(x => x.Name == "Left Angle") != null))
                        {
                            //_builder.AppendLine("-Length/Angle: " + itm.Dimensions.FirstOrDefault(x => x.Name == "Right Angle") + "/" + itm.Dimensions.FirstOrDefault(x => x.Name == "Left Angle"));
                            dataObject.lengthangle = Math.Round(itm.Dimensions.FirstOrDefault(x => x.Name == "Right Angle").Value, 0) + "/" + Math.Round(itm.Dimensions.FirstOrDefault(x => x.Name == "Left Angle").Value, 0);
                        }
                        else
                        {
                            //_builder.AppendLine("-Length/Angle: 0");
                            dataObject.lengthangle = "0";
                        }
                    }

                    //Connector
                    if (itm.Connectors.Count > 0)
                    {
                        dataObject.connector = itm.Connectors[0].Info.Name;
                        dataObject.connector = RemoveSillyMarks(dataObject.connector);
                    }

                    //Material
                    dataObject.material = itm.Material.Name.ToString();

                    //Equipment tag
                    dataObject.equipmentTag = itm.EquipmentTag.ToString();

                    //Job Area - CustomData field
                    data = itm.CustomData[3];
                    myCustomData = data as CustomDataStringValue;
                    dataObject.jobArea = myCustomData.Value.ToString();
                    dataObject.jobArea = RemoveSillyMarks(dataObject.jobArea);
                    if (string.IsNullOrEmpty(dataObject.jobArea))
                        dataObject.jobArea = dataObject.custom4;

                    //File name
                    dataObject.filename = fileName;


                    //Others customization
                    data = itm.CustomData[4];
                    myCustomData = data as CustomDataStringValue;
                    dataObject.custom4 = myCustomData.Value.ToString();
                    dataObject.custom4 = RemoveSillyMarks(dataObject.custom4);

                    if (string.IsNullOrEmpty(dataObject.drawingno))
                        dataObject.drawingno = dataObject.custom4;

           
                    data = itm.CustomData[4];
                    myCustomData = data as CustomDataStringValue;
                    dataObject.custom4 = myCustomData.Value.ToString();
                    dataObject.custom4 = RemoveSillyMarks(dataObject.custom4);

                    //list_dataObject.Add(dataObject);
                    var newLine = ToCSVStringValue(dataObject);
                    csv.AppendLine(newLine);
                }
            } 
            catch(Exception exportDataException)
            {
                 log.Debug(fileNameWithoutExtension + " wrong value with " + exportDataException.Message.ToString());
            }
            try
            {
                //FileStream stream = File.Create(filePath);
                //stream.Write(csv.ToString());
                //stream.Close();
                File.WriteAllText(filePath, csv.ToString());
            }
            catch (Exception ex)
            {
                 log.Debug(fileNameWithoutExtension + " can't save file with " + ex.Message.ToString());
            }
            log.Debug(fileNameWithoutExtension +" export CSV successfully!");
            
        }

        public  string RemoveSillyMarks(string inputString)
        {
            string outputString = inputString;

            outputString = outputString.Replace(",", "-");
            outputString = outputString.Replace("'", "");
            outputString = outputString.Replace("\\", "");
            outputString = outputString.Replace(System.Environment.NewLine, " ");

            return outputString;
        }
        private bool IsDirectoryExists(SftpClient client, string path)
        {
            bool isDirectoryExist = false;

            try
            {
                client.ChangeDirectory(path);
                isDirectoryExist = true;
            }
            catch (SftpPathNotFoundException)
            {
                return false;
            }
            return isDirectoryExist;
        }
        public void UploadFileToServer()
        {
            //SftpClient client = new SftpClient("139.99.212.132", 22, "hungdang", "Sox17981");
            var FolderSaveCSV = "CSV Output";
            var RelativePath = "//var/www/takeoff.dummy.pd.com.au/";
            try
            {
                using (var client = new SftpClient("139.99.241.245", 22, "henryqld", "phatTran"))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        if (!IsDirectoryExists(client, RelativePath + FolderSaveCSV))
                        {
                            client.ChangeDirectory(RelativePath);
                            client.CreateDirectory(FolderSaveCSV);
                        }
                        string fileCurrentPathLocal = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "CSV Output");
                        string[] all_files = Directory.GetFiles(fileCurrentPathLocal);
                        foreach (string file in all_files)
                        {
                            client.UploadFile(File.OpenRead(file), Path.GetFileName(file));
                            SftpFile files = client.Get(RelativePath + FolderSaveCSV + "/" + Path.GetFileName(file));
                            long remotefilsize = files.Attributes.Size;
                            bool fileExists = client.Exists(RelativePath + FolderSaveCSV + "/" + Path.GetFileName(file));
                            if (fileExists && remotefilsize == new FileInfo(fileCurrentPathLocal + "/" + Path.GetFileName(file)).Length)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {

                            }
                        }
                        client.Disconnect();
                    }
                }
                
            }
            catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                Console.WriteLine($"Error connecting to server: {e.Message}");
            }
            catch (SshAuthenticationException e)
            {
                Console.WriteLine($"Failed to authenticate: {e.Message}");
            }
            catch (SftpPermissionDeniedException e)
            {
                Console.WriteLine($"Operation denied by the server: {e.Message}");
            }
            catch (SshException e)
            {
                Console.WriteLine($"Sftp Error: {e.Message}");
            }
        }
        public  void saveFileXlSX(List<DataObject> itemList)
        {
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet("sheetName");
            var ws = workbook.Worksheet("sheetName");

            int row = 1;
            foreach (DataObject item in itemList)
            {
                if (!string.IsNullOrEmpty(item.jobno))
                {
                    ws.Cell(row, 1).Value = item.jobno;
                    ws.Cell(row, 2).Value = item.drawingno;
                    ws.Cell(row, 3).Value = item.handle;
                    ws.Cell(row, 4).Value = item.itemno;
                    ws.Cell(row, 5).Value = item.insulation;
                    ws.Cell(row, 6).Value = item.galvenized;
                    ws.Cell(row, 7).Value = item.notes;
                    ws.Cell(row, 8).Value = item.weight;
                    ws.Cell(row, 9).Value = item.status;
                    ws.Cell(row, 10).Value = item.qty;
                    ws.Cell(row, 11).Value = item.cuttype;
                    ws.Cell(row, 12).Value = item.cid;
                    ws.Cell(row, 13).Value = item.description;
                    ws.Cell(row, 14).Value = item.doublewall;
                    ws.Cell(row, 15).Value = item.emptyString;
                    ws.Cell(row, 16).Value = item.insulationarea;
                    ws.Cell(row, 17).Value = item.metalarea;
                    ws.Cell(row, 18).Value = item.boughtout;
                    ws.Cell(row, 19).Value = item.linearmeter;
                    ws.Cell(row, 20).Value = item.sectionindex;
                    ws.Cell(row, 21).Value = item.sectiondescription;
                    ws.Cell(row, 22).Value = item.prefixstring;
                    ws.Cell(row, 23).Value = item.insulationSpec;
                    ws.Cell(row, 24).Value = item.widthDim;
                    ws.Cell(row, 25).Value = item.depthDim;
                    ws.Cell(row, 26).Value = item.lengthangle;
                    ws.Cell(row, 27).Value = item.connector;
                    ws.Cell(row, 28).Value = item.material;
                    ws.Cell(row, 29).Value = item.equipmentTag;
                    ws.Cell(row, 30).Value = item.jobArea;
                    ws.Cell(row, 31).Value = item.filename;
                    row++;
                }

            }

            workbook.SaveAs("test.xlsx");
        }

        public  void UpdateRandomFileName()
        {
            try
            {
                string newFilename = Path.Combine(pathDirectoryContainsFile, fileNameWithoutExtension);
                File.Delete(newFilename + ".MLK");
                if (!fileNameWithoutExtension.Contains("rand"))
                {
                    var tempNewName = "rand_" + DateTime.Now.ToString("yMdHmsfff") + "_" + fileName;
                    var newPathRandomFileName = pathDirectoryContainsFile + "\\" + tempNewName;

                    File.Copy(Job.Info.FileName, newPathRandomFileName);

                    System.Diagnostics.Process.Start(newPathRandomFileName);
                    Process theprocess = System.Diagnostics.Process.GetCurrentProcess();
                    string processdetails;

                    processdetails = theprocess.ProcessName.ToString() + " -" + theprocess.Id.ToString();
                    //sw.WriteLine(processdetails);

                    if (processdetails.Contains("CAMduct"))
                    {
                        //builder.AppendLine(processdetails);
                        File.Delete(Job.Info.FileName);
                        theprocess.Kill();
                    }
                    processdetails = "";
                    log.Debug("Change new filename successfully");

                }
                else
                {
                    log.Debug("Filename was kept as original");

                }
                Job.Save();
            }
            catch (Exception exception) {
                log.Debug(exception.Message);
            }

        }
        public  string initCSV()
        {
         
            return
                $"operatorID," +
                $"jobday," +
                $"jobtime," +
                $"jobno," +
                $"drawingno," +
                $"handle," +
                $"itemno," +
                $"insulation," +
                $"galvenized," +
                $"notes," +
                $"weight," +
                $"status," +
                $"qty," +
                $"cuttype," +
                $"cid," +
                $"description," +
                $"doublewall," +
                $"pathId," +
                $"insulationarea," +
                $"metalarea," +
                $"boughtout," +
                $"linearmeter," +
                $"sectionindex," +
                $"sectiondescription," +
                $"prefixstring," +
                $"insulationSpec," +
                $"widthDim," +
                $"depthDim," +
                $"lengthangle," +
                $"connector," +
                $"material," +
                $"equipmentTag," +
                $"jobArea,"+
                $"filename,";
        }
        public  string ToCSVStringValue(DataObject dataObject)
        {
            /*******
            1       2       3       4       5
            jobno,drawingno,handle,itemno,insulation,
            galvenized,notes,weight,status,qty,
            cuttype,cid,description,doublewall,updateID,
            insulationarea,metalarea,boughtout,sectionindex,sectiondescription,
            prefixString,insulationSpec,widthDim,depthDim,lengthAngle
            *******/
            return $"" +
                $"{dataObject.operatorId}," +
                $"{dataObject.jobday}," +
                $"{dataObject.jobtime}," +
                $"{dataObject.jobno}," +
                $"{dataObject.drawingno}," +
                $"{dataObject.handle}," +
                $"{dataObject.itemno}," +
                $"{dataObject.insulation}," +
                $"{dataObject.galvenized}," +
                $"{dataObject.notes}," +
                $"{dataObject.weight}," +
                $"{dataObject.status}," +
                $"{dataObject.qty}," +
                $"{dataObject.cuttype}," +
                $"{dataObject.cid}," +
                $"{dataObject.description}," +
                $"{dataObject.doublewall}," +
                $"{dataObject.pathid}," +
                $"{dataObject.insulationarea}," +
                $"{dataObject.metalarea}," +
                $"{dataObject.boughtout}," +
                $"{dataObject.linearmeter}," +
                $"{dataObject.sectionindex}," +
                $"{dataObject.sectiondescription}," +
                $"{dataObject.prefixstring}," +
                $"{dataObject.insulationSpec}," +
                $"{dataObject.widthDim}," +
                $"{dataObject.depthDim}," +
                $"{dataObject.lengthangle}," +
                $"{dataObject.connector}," +
                $"{dataObject.material}," +
                $"{dataObject.equipmentTag}," +
                $"{dataObject.jobArea},"+
                 $"{dataObject.filename},"
                ;
        }

        public  void CreateRandomDirectory(string DirectoryName)
        {
            string[] ChopDirectoryName = DirectoryName.Split('\\');
            string tempPath = "C:";

            for (int i = 1; i < ChopDirectoryName.Length; i++)
            {
                tempPath += "\\" + ChopDirectoryName[i];
                Console.WriteLine(tempPath);
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);
            }
        }
    }
}
