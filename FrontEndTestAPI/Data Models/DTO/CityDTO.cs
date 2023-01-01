using FrontEndTestAPI.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEndTestAPI.DataTransferObjects
{
    public class CityDTO // Simple POCO Object with some difference with the Entity Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; } = null!;   // Prop can be Mapped via Flattening
    }

}
