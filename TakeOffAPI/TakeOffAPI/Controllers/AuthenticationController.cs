using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using TakeOffAPI.Service;
using TakeOffAPI.Entities.Request;
using TakeOffAPI.Service.Interface;
using TakeOffAPI.WebAPIClient.Commons;
using TakeOffAPI.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using TakeOffAPI.Entities;

namespace TakeOffAPI.Controllers
{
    [ApiController]
    [Route("take-off/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _userService;
        public AuthenticationController(IConfiguration configuration, IAuthenticationService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authentication([FromBody] AuthRequest authRequest)
        {
            var loginRes = await _userService.Login(authRequest);

            if (loginRes.Code != (int)ERROR_CODE.SUCCESS)
                return Unauthorized(new { Message = "Invalid credentials" });
            else
            {
                var data = loginRes.Data as LoginResponse;
                return Ok(new { Token = data.token });
            }
        }
    }
}
