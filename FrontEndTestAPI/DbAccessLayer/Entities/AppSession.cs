using FrontEndTestAPI.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class AppSession
    {
        // Properties
        [Key][Required] public int AppSessionId { get; set; }
        public string IpAdress { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public string UserFingerprint { get; set; }
        public DateTime? Revoked { get; set; }

        // Method Properties
        public bool IsActive => (this.Revoked is null) && (!this.IsExpired);
        public bool IsExpired => DateTime.UtcNow >= this.Expires;


        // <-- Relationships --> //


        //// Parent Relationship One-to-Many
        public ApplicationUser User { get; set; }                           // Navigation Prop
        public string ApplicationUserId { get; set; }                       // Foreign Key

        //// Child Relationship One-to-Many
        public List<RefreshToken> RefreshTokens { get; set; }    // List of Many Tokens
    }
}

