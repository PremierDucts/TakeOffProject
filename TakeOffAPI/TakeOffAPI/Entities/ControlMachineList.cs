using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeOffAPI.Entities
{
    [Table("tbl_ControlLists")]
	public class ControlMachineList
	{
		public ControlMachineList()
		{
		}
        public string ip_address { get; set; }
        public string uid { get; set; }
        public string mac_address { get; set; }
        public string user_name { get; set; }

        public ControlMachineList(string ip_address_, string uid_, string mac_address_, string user_name_)
        {
            this.ip_address = ip_address_;
            this.uid = uid_;
            this.mac_address = mac_address_;
            this.user_name = user_name_;
        }
    }
}

