using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEndTestAPI.Data.Models
{

    [Table("Cities")] // Naming the Table Cities
    [Index(nameof(Name))]
    [Index(nameof(Lat))]
    [Index(nameof(Lon))]
    public class City
    {

        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; } = null!;  //null! --> Promising that the Variable wont be null

        [Column(TypeName = "decimal(7,4)")]
        public decimal Lat { get; set; }

        [Column(TypeName = "decimal(7,4)")]
        public decimal Lon { get; set; }


        // Foreign Key
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country? Country { get; set; } = null!;  //Country may be null


    }
}
