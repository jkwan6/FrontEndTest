using AutoMapper;
using AutoMapper.QueryableExtensions;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());
            var dtoObject = _context.Cities.ProjectTo<CityDTO>(config);

            #region Automapper Alternative Using Include
            //var dtoObject = _context.Cities
            //    .AsNoTracking()
            //    .Include(c => c.Country)
            //    .Select(c => _mapper.Map<CityDTO>(c));
            #endregion
            #region HardCoded Mapping Alternative
            //var dtoObject = _context.Cities.AsNoTracking().Select(c => new CityDTO
            //{
            //    Id = c.Id,                          // Similar
            //    Name = c.Name,                      // Similar
            //    Lat = c.Lat,                        // Similar
            //    Lon = c.Lon,                        // Similar
            //    CountryId = c.CountryId,            // Similar
            //    CountryName = c.Country!.Name       // DTO PROCESSED PROPERTY
            //});     // Mapping Entity to DTO
            #endregion

            var results = await ApiResult<CityDTO>.CreateAsync(dtoObject , pageParams);           // Calling ApiResult

            #region Comments
            // The Async Method returns and ApiResult<City> Instantiation
            // The return that instantiation, you call the static class on ApiResult
            // CreateAsync returns an ApiResult<City>
            #endregion
            return results;                                                           // Return Results
        }

        public async Task<ActionResult<CityDTO>> GetCityAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>());

            var cityDTO = await _context.Cities
                .Where(c => c.Id == id)
                .ProjectTo<CityDTO>(config)
                .FirstOrDefaultAsync();

            #region Hard Coded Alternative
            //var cityDTO = await _context.Cities
            //    .Where(c => c.Id == id)
            //    .Select(c => new CityDTO
            //    {
            //        Id = c.Id,                      // Similar
            //        Name = c.Name,                  // Similar
            //        Lat = c.Lat,
            //        Lon = c.Lon,
            //        CountryId = c.CountryId,
            //        CountryName = c.Country!.Name
            //    })
            //    .FirstOrDefaultAsync();
            #endregion

            return cityDTO!;
        }

        //public async Task<ActionResult<CityDTO>> PutCityAsync(City city)
        //{
        //    _context.Entry(city).State = EntityState.Modified;
        //}



    }
}
