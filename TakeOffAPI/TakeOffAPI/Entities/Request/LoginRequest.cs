﻿using System.ComponentModel.DataAnnotations;

namespace TakeOffAPI.Entities.Request
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public string DeviceId { get; set; } = "";
    }
}
