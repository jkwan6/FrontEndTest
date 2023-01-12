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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtCreatorService _jwtCreator;

        // Constructor
        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            JwtCreatorService jwtHandler )
        {
            _context = context;
            _userManager = userManager;
            _jwtCreator = jwtHandler;
        }

        // Login Request will Model Bind Email and Password from HTTP Post
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // UserManager is from Identity Nuget
            var user = await _userManager.FindByNameAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new LoginResult()
                {
                    success = false,
                    message = "Invalid Email or Password."
                });
            }

            // Token Preparation
            var tokenPrep = await _jwtCreator.GetTokenAsync(user);
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokenPrep);

            // Return JSON Object to Client
            return Ok(new LoginResult()
            {
                success = true,
                message = "Login Successful",
                token = tokenToReturn
            });
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
