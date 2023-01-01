using AutoMapper;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataTransferObjects;

namespace FrontEndTestAPI.AutoMapper.Profiles
{
    public class CountryProfile: Profile
    {

        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>()
                .ForMember(
                dest => dest.CitiesCount,                           // Method with Custom Mapping/
                opt => opt.MapFrom(src => src.Cities!.Count())      // Custom Mapping
                );
        }
    }
}
