using Microsoft.VisualBasic.Logging;
using NewApp2023.Utilities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NewApp2023
{
    public partial class Login : Form
    {
        string domainName = string.Empty;
        bool is_Login = true;
        string webResponse = string.Empty;

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            username.Text = "testmfg";
            password.Text = "test@123";
            tb_ip.Text = InformationsDevice.GetLocalIPAddress();
            tb_macAddress.Text = InformationsDevice.getMacAddress();
            tb_uid.Text = InformationsDevice.getUID();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check network available or not
                if (IsNetworkAvailable())
                {
                    //call api login:https://appuserspd.y.fo/user/login to check 
                    using (HttpClient httpClient = new HttpClient())
                    {
                        string apiURL = "https://appuserspd.y.fo/user/login";
                        var credential = new
                        {
                            username = username.Text,
                            password = password.Text,
                            deviceId = ""
                        };
                        string json = JsonConvert.SerializeObject(credential);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(apiURL, content);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            //if Login success - copy file customize addin to Camduct app
                            copyFileAddinToCamduct();
                            var result = httpResponseMessage.Content.ReadAsStringAsync();
                            var responseData = JsonConvert.DeserializeObject<ResponseData>(result.Result);
                            if (responseData != null && responseData.Code == 0)
                            {
                                //if username and password are correct, save user data into local json file
                                UserDataManager.SaveUserData(new UserData
                                {
                                    Username = username.Text,
                                    Password = password.Text,
                                    IP = tb_ip.Text,
                                    MacAddress = tb_macAddress.Text,
                                    UID = tb_uid.Text,
                                });
                                this.Hide();

                                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Login fail! please check credentail again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                    }

                }
                //if network unavailable , load local json file and check it
                else
                {
                    string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AddInData");
                    UserData userData = new UserData();
                    string pathContainUserDataFile = Path.Combine(appFolder, "userdata.json");
                    if (File.Exists(pathContainUserDataFile))
                    {
                        string json = File.ReadAllText(pathContainUserDataFile);
                        //parse file local json file to UserData
                        userData = JsonConvert.DeserializeObject<UserData>(json);
                        if (userData.Username.Equals(username.Text) &&
                            userData.Password.Equals(password.Text) &&
                            userData.UID.Equals(tb_uid.Text) &&
                            userData.IP.Equals(tb_ip.Text) &&
                            userData.MacAddress.Equals(tb_macAddress.Text))
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Login fail! please check credentail again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No file userdata.json into C:\\Users\\{Environment.UserName}\\AddInData \n Please create it by logging by network connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Exception: {exception.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        private void copyFileAddinToCamduct()
        {
            List<string> yearList = new List<string>();
            yearList.Add("2022");
            yearList.Add("2024");
            string addinLocation = string.Empty;

            foreach (var item in yearList)
            {
                addinLocation = "C:/ProgramData/Autodesk/Fabrication/Addins/" + item;
                if (!Directory.Exists(addinLocation))
                    Directory.CreateDirectory(addinLocation);

                CopyFileToFolder(Application.StartupPath, addinLocation, "MainAddin.addin");
            }
        }
        private void CopyFileToFolder(string sourceLocation, string destinationLocation, string fileNameToCopy)
        {
            if (File.Exists(Path.Combine(destinationLocation, fileNameToCopy)))
                File.Delete(Path.Combine(destinationLocation, fileNameToCopy));
            File.Copy(Path.Combine(sourceLocation, fileNameToCopy), Path.Combine(destinationLocation, fileNameToCopy));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Directory.Exists("C:/ProgramData/Autodesk/Fabrication/Addins"))
                Directory.Delete("C:/ProgramData/Autodesk/Fabrication/Addins", true);

        }
    }
}