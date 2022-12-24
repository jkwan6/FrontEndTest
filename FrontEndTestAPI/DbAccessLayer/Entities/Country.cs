using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEndTestAPI.Data.Models
{
    [Table("Countries")] // Naming the Table Countries
    [Index(nameof(Name))] // Setting Indexes --> MAkes looking up values more efficient
    [Index(nameof(ISO2))]
    [Index(nameof(ISO3))]
    public class Country
    {
        [Key][Required]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonPropertyName("iso2")]
        public string ISO2 { get; set; } = null!;
        [JsonPropertyName("iso3")]
        public string ISO3 { get; set; } = null!;

        // Gotta remember about Eager/Lazy/Explicit Loading
        public ICollection<City>? Cities { get; set; } = null!; //Could be null, but default value is not null

    }
}
