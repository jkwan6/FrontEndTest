using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndTestAPI.DataServices
{
    public interface ICityService
    {
        Task<ActionResult<ApiResult<CityDTO>>> GetCitiesAsync(PageParameters pageParams);
        Task<ActionResult<CityDTO>> GetCityAsync(int id);
        Task<HttpResponseMessage> PutCityAsync(int id, City city);
        Task<HttpResponseMessage> PostCityAsync(City city);
        Task<HttpResponseMessage> DeleteCityAsync(int id);
    }
}