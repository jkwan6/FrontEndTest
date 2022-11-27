using FrontEndTestAPI.Data.Models;

namespace FrontEndTestAPI.DataTransferObjects
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ISO2 { get; set; } = null!;
        public string ISO3 { get; set; } = null!;
        public int CitiesCount { get; set; }

    }
}
