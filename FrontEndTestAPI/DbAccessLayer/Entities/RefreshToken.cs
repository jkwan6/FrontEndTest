using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FrontEndTestAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class RefreshToken
    {
        // Properties
        [Key] [Required] public int Id { get; set; }
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        // Method Properties
        public bool IsActive => (this.Revoked is null) && (!this.IsExpired);
        public bool IsExpired => DateTime.UtcNow >= this.Expires;


                                // <-- Relationships --> //


        // Parent Relationship 1
        public AppSession AppSession { get; set; }
        public int AppSessionId { get; set; }

        public ApplicationUser User { get; set; } 
        public string ApplicationUserId { get; set; } 
    }
}
