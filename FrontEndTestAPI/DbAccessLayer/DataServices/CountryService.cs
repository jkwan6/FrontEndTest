using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using FrontEndTestAPI.DbAccessLayer.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.DataServices
{
    public class CountryService: ICountryService
    {
        private readonly ApplicationDbContext _context;         // Properties
        public CountryService(ApplicationDbContext context)     // Ctor
        {
            _context = context;
        }

        public async Task<ActionResult<ApiResult<CountryDTO>>> GetCountriesAsync(PageParameters pageParams)
        {
             var results = await ApiResult<CountryDTO>.CreateAsync(
                _context.Countries.AsNoTracking()
                .Select(c => new CountryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ISO2 = c.ISO2,
                    ISO3 = c.ISO3,
                    CitiesCount = c.Cities!.Count
                }),
                pageParams);

            return results;
        }

        public async Task<ActionResult<Country>> GetCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            return country!;
        }



    }
}
