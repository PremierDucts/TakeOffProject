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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TakeOffAPI.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileUtilController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        public FileUtilController(IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        //[HttpPost("upload")]
        //[Authorize]
        //public async Task<ActionResult> AutoUploadFile([FromBody] List<FileUploadModelRequest> fileUploadModelRequests)
        //{
        //    try
        //    {
        //        await _fileService.postFileAsync(fileUploadModelRequests);
        //        //await _fileService.uploadFTP();
        //        return Ok();
        //    }catch (Exception ex) { throw ex; }

        //}
        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult> AutoUploadFile()
        {
            try
            {
                await _fileService.uploadFTP();
                return Ok();
            }
            catch (Exception ex) { throw ex; }

        }

        [HttpPost("excute")]
        [Authorize]
        public async Task<ActionResult> ExcuteFileCSVOnServer([FromBody] string file_name)
        {
            try
            {
                await _fileService.excuteFileCsv(file_name);
                return Ok();
            }
            catch (Exception ex) { throw ex; }

        }
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
