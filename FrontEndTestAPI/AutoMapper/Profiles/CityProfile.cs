using AutoMapper;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataServices;
using FrontEndTestAPI.DataTransferObjects;

namespace FrontEndTestAPI.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile() // Put the mapping configuration in the constructor
        {
            CreateMap<City, CityDTO>();
                //.ForMember(
                //    dest => dest.CountryName, opt => opt.MapFrom(src => src.Country!.Name)
                //);



            CreateMap<City, ApiResult<CityDTO>>();
            CreateMap<PageParameters, CityService>();
        }

    }
}
