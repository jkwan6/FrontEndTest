using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrontEndTestAPI.DbAccessLayer.DataServices
{
    public class JwtCreatorService 
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtCreatorService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        // Method to create Tokens.  Calls the Private Method beneath
        public async Task<JwtSecurityToken> GetTokenAsync(ApplicationUser user)
        {
            var jwtOptions = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: await GetClaimsAsync(user, _userManager),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationTimeInMinutes"])),
                signingCredentials: GetSigningCredentials()
                );

            return jwtOptions;
        }

        // Method To Create a Key and then Creating Signing Credentials from the Key
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecurityKey"]!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        // Method to Create the User Claims to be placed in the JWT
        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }


    }
}
