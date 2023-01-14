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

        // Db Access + Token Creation
        public async Task<LoginResult> Login(LoginRequest loginRequest, string ipAddress)
        {
            // Validate Username and Password Against DB
            var user = await _userManager.FindByNameAsync(loginRequest.Email);
            var password = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            // Early Return if Authentication Fails
            if ( user is null || password is false)
            return new LoginResult(false);

            // Token Preparation if Authentication Success
            var tokenPrep = await _jwtCreator.GetTokenAsync(user);

            // Creating JWT and Refresh JWT
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(tokenPrep);
            var refreshTokenToReturn = generateRefreshToken(ipAddress);

            _context.Users.Select(x => x.RefreshTokens);

            if (user.RefreshTokens is null)
                user.RefreshTokens = new List<RefreshToken>();

            user.RefreshTokens.Add(refreshTokenToReturn);

            var user1 = _context.Users.Entry(user);
            
            _context.Users.Update(user);
    

            _context.SaveChanges();

            // Assigning Token to Login Result
            var loginResult = new LoginResult(true) 
            { token = tokenToReturn, refreshToken = refreshTokenToReturn.Token };

            // Returning LoginResult to Controller
            return loginResult;
        }

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
