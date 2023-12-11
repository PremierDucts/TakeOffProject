using System.ComponentModel.DataAnnotations;

namespace TakeOffAPI.Entities
{
    public class Operator
    {
        public string Name { get; set; }
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Factory { get; set; }
        public int? FaceID { get; set; }
        public string profilePath { get; set; }
        public string position { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Manager { get; set; }
        public string Status { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string DOB { get; set; }
        public string address { get; set; }

        public Operator()
        {
        }
        public Operator(string Name_, string Username_, string Password_, string Role_, string Factory_, int FaceID_, string profilePath_, string position_, string Company_, string Country_, string Manager_, string Status_, string phone_, string email_, string DOB_, string address_)
        {
            Name = Name_;
            Username = Username_;
            Password = Password_;
            Role = Role_;
            Factory = Factory_;
            FaceID = FaceID_;
            profilePath = profilePath_;
            position = position_;
            Company = Company_;
            Country = Country_;
            Manager = Manager_;
            Status = Status_;
            phone = phone_;
            email = email_;
            DOB = DOB_;
            address = address_;
        }
    }
}
