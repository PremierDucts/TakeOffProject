using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp2023
{
    public class UserDataManager
    {
        public static void SaveUserData(UserData userData)
        {
            string json = JsonConvert.SerializeObject(userData);
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AddInData");
            Directory.CreateDirectory(appFolder);
            string filePath = Path.Combine(appFolder, "userdata.json");
            File.WriteAllText(filePath, json);
        }
        public static UserData LoadUserData()
        {
            string filePath = "userdata.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<UserData>(json);
            }
            return null;
        }
    }
}
