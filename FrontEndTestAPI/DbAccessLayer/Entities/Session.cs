using System;
using System.ComponentModel.DataAnnotations;


namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class Session
    {
        // Properties
        [Required] [Key] public int SessionId { get; set; }
        public string IpAdress { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public bool IsActive { get; set; }
        public string UserFingerprint { get; set; }


        // Parent Relationship One-to-Many
        [Required] public ApplicationUser User { get; set; }    // Navigation Prop
        [Required] public int ApplicationUserId { get; set; }   // Foreign Key
        
        // Child Relationship One-to-Many
        [Required] public List<RefreshToken> refreshTokens { get; set; }
    }
}
