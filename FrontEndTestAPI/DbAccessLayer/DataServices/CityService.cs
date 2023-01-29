using AutoMapper;
using AutoMapper.QueryableExtensions;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using FrontEndTestAPI.DbAccessLayer.DataServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FrontEndTestAPI.DataServices
{
    public class CityService : ICityService
    {
        #region Properties
        private readonly ApplicationDbContext _context;                                     // Property
        private readonly IMapper _mapper;                                                   // Property - If Automapper to be used
        #endregion

        #region Constructor
        public CityService(ApplicationDbContext context, IMapper mapper)                    // Constructor
        {
            _context = context;                                                             // DI
            _mapper = mapper;                                                               // DI
        }
        #endregion

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

        public async Task<HttpResponseMessage> PutCityAsync(int id, City city)
        {
            if (id != city.Id) { return new HttpResponseMessage(HttpStatusCode.BadRequest); }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);

            // Internal Method
            bool CityExists(int id)
            {
                return _context.Cities.Any(e => e.Id == id);
            }
        }

        public async Task<HttpResponseMessage> PostCityAsync(City city)
        {
            try
            {
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public async Task<HttpResponseMessage> DeleteCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        #region Private Methods
        public bool IsDupeCity(City city)
        {
            var isDupeCity = _context.Cities.Any( e =>
                e.Name == city.Name
            &&  e.Lat == city.Lat
            &&  e.Lon == city.Lon
            &&  e.CountryId == city.CountryId
            &&  e.Id != city.Id);

            return isDupeCity;
        }
        #endregion
    }
}
