namespace TakeOffAPI.Entities.Request
{
    public class AuthRequest
    {
        public String IpAddress { get; set; } = "";
        public String UId { get; set; } = "";
        public String MacAddress { get; set; } = "";
        public String Username { get; set; } = "";
        public String Password { get; set; } = "";
    }
}
