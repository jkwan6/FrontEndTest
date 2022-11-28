using AutoMapper;
using FrontEndTestAPI.Data.ApiResults;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.DataServices
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;    
            
        public CityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<ApiResult<CityDTO>>> GetCitiesAsync(
        int pageIndex, //Parameters from Front-End Via Model Binding
        int pageSize) //Parameters from Front-End Via Model Binding

        {
            // The Async Method returns and ApiResult<City> Instantiation
            // The return that instantiation, you call the static class on ApiResult
            // CreateAsync returns an ApiResult<City>

            ApiResult<CityDTO>.CreateAsync(_context.Cities.ToListAsync());

            return await ApiResult<CityDTO>.CreateAsync(
                _context.Cities.AsNoTracking().Select(c => new CityDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Lat = c.Lat,
                    Lon = c.Lon,
                    CountryId = c.CountryId,
                    CountryName = c.Country!.Name
                }),
                pageIndex,
                pageSize
                ); ;
        }

        public async Task<ActionResult<City>> GetCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            return city!;
        }





    }
}
