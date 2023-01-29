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
    public class CountryService: ICountryService
    {
        #region Properties
        private readonly ApplicationDbContext _context;         // Properties
        private readonly IMapper _mapper;                       // Properties
        #endregion

        #region Constructor
        public CountryService(ApplicationDbContext context, IMapper mapper)     // Constructor
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public async Task<ActionResult<ApiResult<CountryDTO>>> GetCountriesAsync(PageParameters pageParams)
        {

            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Country, CountryDTO>()
                .ForMember(
                dest => dest.CitiesCount,                           // Method with Custom Mapping/
                opt => opt.MapFrom(src => src.Cities!.Count()
                )));

            var dtoObject = _context.Countries.ProjectTo<CountryDTO>(config);
            var results = await ApiResult<CountryDTO>.CreateAsync(dtoObject, pageParams);
            return results;
        }

        public async Task<ActionResult<Country>> GetCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            return country!;
        }

        public async Task<HttpResponseMessage> PutCountryAsync(int id, Country country)
        {
            if (id != country.Id) { return new HttpResponseMessage(HttpStatusCode.BadRequest);}

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);

            // Nested Method
            bool CountryExists(int id)
            {
                return _context.Countries.Any(e => e.Id == id);
            }
        }

        public async Task<HttpResponseMessage> PostCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public async Task<HttpResponseMessage> DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
