using Azure.Core;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data_Models.DTO;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DbAccessLayer.DataServices;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEndTestAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        // Properties
        private readonly IAuthService _authService;

        // Constructor
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // Login Request will Model Bind Email and Password from HTTP Post
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            string ipAd = ipAdress();
            // Data Access Layer
            var loginResult = await _authService.Login(loginRequest, ipAdress());

            bool isAuthorized = (loginResult.success) ? true : false;

            return (isAuthorized) ? Ok(loginResult) : Unauthorized(loginResult);
        }

        private string ipAdress()
        {
            if (this.Request.Headers.ContainsKey("X-Forwarded-For"))
                return this.Request.Headers["X-Forwarded-For"];
            else
                // Will convert IPV6 to IPV4. Will keep IPV4 to IPV4
                return this.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
