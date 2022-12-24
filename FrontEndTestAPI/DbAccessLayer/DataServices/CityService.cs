using AutoMapper;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace FrontEndTestAPI.DataServices
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;                                     // Property
        private readonly IMapper _mapper;                                                   // Property - If Automapper to be used

        public CityService(ApplicationDbContext context, IMapper mapper)                    // Constructor
        {
            _context = context;                                                             // DI
            _mapper = mapper;                                                               // DI
        }

        public async Task<ActionResult<ApiResult<CityDTO>>>GetCitiesAsync
            (PageParameters pageParams)                                                     // Params from Model Binding
        {

            var dtoObject = _context.Cities.AsNoTracking().Select(c => new CityDTO
            {
                Id = c.Id,                          // Similar
                Name = c.Name,                      // Similar
                Lat = c.Lat,                        // Similar
                Lon = c.Lon,                        // Similar
                CountryId = c.CountryId,            // Similar
                CountryName = c.Country!.Name       // DTO PROCESSED PROPERTY
            });     // Mapping Entity to DTO
            #region Automapper Alternative
            //var dtoObject = _context.Cities
            //    .AsNoTracking()
            //    .Include(c => c.Country)
            //    .Select(c => _mapper.Map<CityDTO>(c));
            #endregion

            var results = ApiResult<CityDTO>.CreateAsync(dtoObject , pageParams);           // Calling ApiResult

            #region
            // The Async Method returns and ApiResult<City> Instantiation
            // The return that instantiation, you call the static class on ApiResult
            // CreateAsync returns an ApiResult<City>
            #endregion
            return await results;                                                           // Return Results
        }


        public async Task<ActionResult<City>> GetCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            return city!;
        }





    }
}
