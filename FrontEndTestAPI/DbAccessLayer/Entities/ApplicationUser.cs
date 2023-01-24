using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class ApplicationUser: IdentityUser      // Entity POCO Class Deriving from Identity User
    {
        // For Now, no need to implement anything.
        // Just gonna be deriving from the Identity User Base Class.


                // <-- Relationships --> //

        // Child Relationships
        [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; }      // List of Refresh Tokens
        [JsonIgnore] public List<AppSession> AppSessions { get; set; }          // List of AppSessions
    }
}
