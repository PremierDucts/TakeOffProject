using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class UserData
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IP { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public string UID { get; set; } = string.Empty;

    }
}
