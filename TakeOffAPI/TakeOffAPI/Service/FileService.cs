using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using TakeOffAPI.Configuration;
using TakeOffAPI.Entities;
using TakeOffAPI.Entities.Request;
using TakeOffAPI.WebAPIClient.Commons;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Org.BouncyCastle.Utilities.IO.Pem;

public class FileService : IFileService {
    private readonly QldDataContext _dbContextClass;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileService> _logger;
    public FileService( IConfiguration configuration , QldDataContext dbContextClass, ILogger<FileService> logger)
    {
        _configuration = configuration;
        _dbContextClass = dbContextClass;
        _logger = logger;
        
    }
    public async Task postFileAsync(List<FileUploadModelRequest> fileUploadModelRequests)
    {

        try
        {

            //string filePathPath = Path.Combine(Directory.GetCurrentDirectory(), "CSV Output");
            //string[] all_files = Directory.GetFiles(filePathPath);
            //foreach (string file in all_files)
            //{


            //    var fileDetail = new FileUploadDetails()
            //    {
            //        id = 0,
            //        FileName = Path.GetFileNameWithoutExtension(file),
            //        FileType = System.IO.Path.GetExtension(file)
            //    };
            //    fileDetail.FileData = System.IO.File.ReadAllBytes(file);
            //    var result = _dbContextClass.FileUploadDetails.Add(fileDetail);
            //    await _dbContextClass.SaveChangesAsync();
            //}
            //foreach (var fileRequest in fileUploadModelRequests)
            //{
            //    var fileDetail = new FileUploadDetails(fileRequest);
            //    var result = _dbContextClass.FileUploadDetails.Add(fileDetail);
            //    await _dbContextClass.SaveChangesAsync();
            //}
            await convertBlobToCsv(3);
        }
        catch (Exception ex) {
            throw ex;
        }
    }
    public async Task convertBlobToCsv(int Id)
    {
        try
        {
            String folder = "";
            if (!Directory.Exists(folder))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(folder);
            }
            var file = _dbContextClass.FileUploadDetails.Where(x => x.id == Id).FirstOrDefault();
            var content = new System.IO.MemoryStream(file.FileData);
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "FileDownloads",
                file.FileName+file.FileType);
            using( var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await content.CopyToAsync(stream);
            }
        }catch(Exception e)
        {
            throw e;
        }
    }
    private bool IsDirectoryExists(SftpClient sftp, string path)
    {
        bool isDirectoryExist = false;

        try
        {
            sftp.ChangeDirectory(path);
            isDirectoryExist = true;
        }   
        catch (SftpPathNotFoundException)
        {
            return false;
        }
        return isDirectoryExist;
    }

    private bool IsFileExists(SftpClient sftp, string path, string file_name)
    {
        return sftp.Exists(path  +"/"+ file_name);
    }
    public async Task uploadFTP()
    {
        var client = new SftpClient("139.99.212.132", 22, "hungdang", "Sox17981");
        var FolderSaveCSV = _configuration.GetSection("Context_Data:FolderSaveCSV").Value;
        var RelativePath = _configuration.GetSection("Context_Data:RelativePath").Value;
        try
        {
            client.Connect();
            if (client.IsConnected)
            {
      

                if (!IsDirectoryExists(client, RelativePath + FolderSaveCSV))
                {
                    client.ChangeDirectory(RelativePath);
                    client.CreateDirectory(FolderSaveCSV);
                }                 
                string fileCurrentPathLocal = Path.Combine(Directory.GetCurrentDirectory(), "CSV Output");

                string[] all_files = Directory.GetFiles(fileCurrentPathLocal);
                foreach (string file in all_files)
                {
                    client.UploadFile(File.OpenRead(file), Path.GetFileName(file));
                    SftpFile files = client.Get(RelativePath + FolderSaveCSV +"/"+Path.GetFileName(file));
                    long remotefilsize = files.Attributes.Size;
                    bool fileExists = client.Exists(RelativePath + FolderSaveCSV + "/" + Path.GetFileName(file));
                    if(fileExists && remotefilsize == new FileInfo(fileCurrentPathLocal+"/" + Path.GetFileName(file)).Length)
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

    public async Task excuteFileCsv(string file_name)
    {
        string? connectionString = _configuration.GetConnectionString("MySQLConnectionString");
        var bad = new List<string>();
        var FolderSaveCSV = _configuration.GetSection("OnPrimse:FolderSaveCSV").Value;
        var RelativePath = _configuration.GetSection("OnPrimse:RelativePath").Value;
        try
        {
            string csvFilePath = RelativePath + FolderSaveCSV + "/" + file_name;

            if (File.Exists(csvFilePath))
            {
                using var reader = new StreamReader(csvFilePath);
                using var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = context =>
                    {
                        bad.Add(context.RawRecord);
                    }
                });
                var records = csv.GetRecords<DispatchDetail>().ToList();
                foreach (var record in records)
                {
                    try
                    {
                        //4 condition: fileName, jobNo, handle, Itemno
                        if (record.description.ToLower().Contains("ff"))
                        {
                            var existingRecord = _dbContextClass.FactoryFits.FirstOrDefault(x => x.filename == record.filename
                              && x.jobno == record.jobno
                              && x.itemno == record.itemno
                              && x.handle == record.handle);
                            if (existingRecord != null)
                            {
                                _logger.LogInformation($"Record is exists on table FactoryFit: filename ={record.filename} , jobno ={record.jobno} " +
                                    $", item ={record.itemno}, handle ={record.handle}");

                                //error because it has the key - this function cannot update the key
                                //_dbContextClass.Entry(existingRecord).CurrentValues.SetValues(record);
                                //existingRecord = new FactoryFit(record);


                                existingRecord.operatorID = record.operatorID;
                                existingRecord.jobday = record.jobday;
                                existingRecord.jobtime = record.jobtime;
                                existingRecord.duration = record.duration;
                                existingRecord.receiverName = record.receiverName;
                                existingRecord.receiverEmail = record.receiverEmail;
                                existingRecord.stationName = record.stationName;
                                existingRecord.jobno = record.jobno;
                                existingRecord.drawingno = record.drawingno;
                                existingRecord.handle = record.handle;
                                existingRecord.itemno = record.itemno;
                                existingRecord.insulation = record.insulation;
                                existingRecord.galvenized = record.galvenized;
                                existingRecord.notes = record.notes;
                                existingRecord.weight = record.weight;
                                existingRecord.status = record.status;
                                existingRecord.qty = record.qty;
                                existingRecord.cuttype = record.cuttype;
                                existingRecord.cid = record.cid;
                                existingRecord.description = record.description;
                                existingRecord.doublewall = record.doublewall;
                                existingRecord.pathId = record.pathId;
                                existingRecord.insulationarea = record.insulationarea;
                                existingRecord.metalarea = record.metalarea;
                                existingRecord.boughtout = record.boughtout;
                                existingRecord.linearmeter = record.linearmeter;
                                existingRecord.sectionindex = record.sectionindex;
                                existingRecord.sectiondescription = record.sectiondescription;
                                existingRecord.prefixstring = record.prefixstring;
                                existingRecord.insulationSpec = record.insulationSpec;
                                existingRecord.widthDim = record.widthDim;
                                existingRecord.depthDim = record.depthDim;
                                existingRecord.lengthangle = record.lengthangle;
                                existingRecord.connector = record.connector;
                                existingRecord.material = record.material;
                                existingRecord.equipmentTag = record.equipmentTag;
                                existingRecord.jobArea = record.jobArea;
                                existingRecord.storageInfo = record.storageInfo;
                                existingRecord.packingID = record.packingID;
                                existingRecord.resetDay = record.resetDay;
                                existingRecord.resetTime = record.resetTime;
                                existingRecord.filename = record.filename;
                                existingRecord.prodQABy = record.prodQABy;
                                existingRecord.prodQADay = record.prodQADay;
                                existingRecord.prodQATime = record.prodQATime;
                                _dbContextClass.FactoryFits.Update(existingRecord);

                            }
                            else
                            {
                                var objectFFConvertDD = new FactoryFit(record);
                                _dbContextClass.FactoryFits.Add(objectFFConvertDD);
                            }
                        }
                        else
                        {
                            var existingRecord = _dbContextClass.DispatchDetails.FirstOrDefault(x => x.filename == record.filename
                          && x.jobno == record.jobno
                          && x.itemno == record.itemno
                          && x.handle == record.handle);
                            if (existingRecord != null)
                            {
                                _logger.LogInformation($"Record is exists on table DispatchDetail: filename ={record.filename} , jobno ={record.jobno} " +
                                    $", item ={record.itemno}, handle ={record.handle}");
                                //error because it has the key - this function cannot update the key
                                //_dbContextClass.Entry(existingRecord).CurrentValues.SetValues(record);
                                //existingRecord = new DispatchDetail(record);

                                existingRecord.operatorID = record.operatorID;
                                existingRecord.jobday = record.jobday;
                                existingRecord.jobtime = record.jobtime;
                                existingRecord.duration = record.duration;
                                existingRecord.receiverName = record.receiverName;
                                existingRecord.receiverEmail = record.receiverEmail;
                                existingRecord.stationName = record.stationName;
                                existingRecord.jobno = record.jobno;
                                existingRecord.drawingno = record.drawingno;
                                existingRecord.handle = record.handle;
                                existingRecord.itemno = record.itemno;
                                existingRecord.insulation = record.insulation;
                                existingRecord.galvenized = record.galvenized;
                                existingRecord.notes = record.notes;
                                existingRecord.weight = record.weight;
                                existingRecord.status = record.status;
                                existingRecord.qty = record.qty;
                                existingRecord.cuttype = record.cuttype;
                                existingRecord.cid = record.cid;
                                existingRecord.description = record.description;
                                existingRecord.doublewall = record.doublewall;
                                existingRecord.pathId = record.pathId;
                                existingRecord.insulationarea = record.insulationarea;
                                existingRecord.metalarea = record.metalarea;
                                existingRecord.boughtout = record.boughtout;
                                existingRecord.linearmeter = record.linearmeter;
                                existingRecord.sectionindex = record.sectionindex;
                                existingRecord.sectiondescription = record.sectiondescription;
                                existingRecord.prefixstring = record.prefixstring;
                                existingRecord.insulationSpec = record.insulationSpec;
                                existingRecord.widthDim = record.widthDim;
                                existingRecord.depthDim = record.depthDim;
                                existingRecord.lengthangle = record.lengthangle;
                                existingRecord.connector = record.connector;
                                existingRecord.material = record.material;
                                existingRecord.equipmentTag = record.equipmentTag;
                                existingRecord.jobArea = record.jobArea;
                                existingRecord.storageInfo = record.storageInfo;
                                existingRecord.packingID = record.packingID;
                                existingRecord.resetDay = record.resetDay;
                                existingRecord.resetTime = record.resetTime;
                                existingRecord.filename = record.filename;
                                existingRecord.prodQABy = record.prodQABy;
                                existingRecord.prodQADay = record.prodQADay;
                                existingRecord.prodQATime = record.prodQATime;
                                _dbContextClass.DispatchDetails.Update(existingRecord);
                            }
                            else
                            {
                                _dbContextClass.DispatchDetails.Add(record);
                            }

                        }
                    }
                    catch(DbUpdateException ex)
                    {
                        _logger.LogError($"Error inserting data: {ex.InnerException?.Message}");
                    }
                }
                try
                {
                    _dbContextClass.SaveChanges();

                }
                catch(Exception e)
                {
                    _logger.LogError($"Error saving changes in batch:" + e.InnerException?.Message);
                }
            }


            //using (var client = new SftpClient("139.99.212.132", 22, "hungdang", "Sox17981"))
            //{
            //    client.Connect();
            //    if (client.IsConnected)
            //    {
            //        bool fileExists = IsFileExists(client, RelativePath+FolderSaveCSV, file_name);

            //        if (fileExists)
            //        {



            //        }
            //        else
            //        {
            //            Console.Write($"File '{file_name}' not");

            //        }
            //    }

            //    client.Disconnect();
            //}
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


}