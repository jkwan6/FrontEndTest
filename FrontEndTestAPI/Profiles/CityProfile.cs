using AutoMapper;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataTransferObjects;

namespace FrontEndTestAPI.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>();
            CreateMap<City, ApiResult<CityDTO>>();
        }

    }
}
