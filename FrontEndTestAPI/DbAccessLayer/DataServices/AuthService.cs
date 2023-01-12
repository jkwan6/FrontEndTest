using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data_Models.DTO;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEndTestAPI.DbAccessLayer.DataServices
{
    public class AuthService: IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtCreatorService _jwtCreator;

        public AuthService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            JwtCreatorService jwtCreator
            )
        {
            _context = context;
            _userManager = userManager;
            _jwtCreator = jwtCreator;
        }

        public async Task<LoginResult> Login(LoginRequest loginRequest, string ipAddress)
        {
            // UserManager is from Identity Nuget
            var user = await _userManager.FindByNameAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return new LoginResult()
                {
                    success = false,
                    message = "Invalid Email or Password."
                };
            }

            // Token Preparation
            var tokenPrep = await _jwtCreator.GetTokenAsync(user);
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokenPrep);

            // Return JSON Object to Client
            return new LoginResult()
            {
                success = true,
                message = "Login Successful",
                token = tokenToReturn
            };
        }
    }
}
