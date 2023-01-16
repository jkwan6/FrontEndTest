using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data_Models.DTO;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace FrontEndTestAPI.DbAccessLayer.DataServices
{
    public class AuthService: IAuthService
    {
        // Properties
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtCreatorService _jwtCreator;

        // Constructor
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

        // Login Method - Returns JWT and Refresh Token
        public async Task<LoginResult> Login(LoginRequest loginRequest, string ipAddress)
        {
            // Validation against DB
            var user = await _userManager.FindByNameAsync(loginRequest.Email);
            var password = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            // Early Return if Authentication Fails
            if ( user is null || password is false) return new LoginResult(false);

            // Creation of Tokens
            var tokenPrep = await _jwtCreator.GetTokenAsync(user);  // Token Prep
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokenPrep);
            var refreshTokenToReturn = generateRefreshToken(ipAddress);

            // DbContext Logic
            if (user.RefreshTokens is null) { user.RefreshTokens = new List<RefreshToken>(); }
            user.RefreshTokens.Add(refreshTokenToReturn);
            _context.Users.Update(user);
            _context.SaveChanges();

            // Assigning Token to Login Result
            var loginResult = new LoginResult(true) 
            { token = tokenToReturn, refreshToken = refreshTokenToReturn.Token };

            // Returning LoginResult
            return loginResult;
        }



        public async Task<LoginResult> RefreshToken(string refreshToken, string ipAddress)
        {
            // Provides me with the user
            var user = _context.Users
                .SingleOrDefault(u => u.RefreshTokens
                .Any(t => t.Token == refreshToken && t.CreatedByIp == ipAddress));

            var irefreshToken = user.RefreshTokens.Single(t => t.Token == ipAddress);

            var newRefreshToken = generateRefreshToken(ipAddress);

            // Populate previous RefreshToken Properties


            return null;
        }








        /* <----------  Private Methods ----------> */
        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                var token = Convert.ToBase64String(randomBytes);

                var refreshToken = new RefreshToken
                {
                    Token = token,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };

                return refreshToken;
            }
        }
    }
}
