using FrontEndTestAPI.Data.ApiResults;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndTestAPI.DataServices
{
    public interface ICityService
    {
        Task<ActionResult<ApiResult<CityDTO>>> GetCitiesAsync(int pageIndex, int pageSize);
        Task<ActionResult<City>> GetCityAsync(int id);
    }
}