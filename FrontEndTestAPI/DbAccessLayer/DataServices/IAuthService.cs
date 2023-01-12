using FrontEndTestAPI.Data_Models.DTO;
using FrontEndTestAPI.Data_Models.POCO;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEndTestAPI.DbAccessLayer.DataServices
{
    public interface IAuthService
    {
        public Task<LoginResult> Login(LoginRequest loginRequest, string ipAddress);
     
    }
}
