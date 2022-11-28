using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.DataServices
{
    public class CountryService
    {
        private readonly ApplicationDbContext _context;         // Properties
        public CountryService(ApplicationDbContext context)     // Ctor
        {
            _context = context;
        }

        public async Task<ActionResult<ApiResult<CountryDTO>>> GetCountries(
        int pageIndex,
        int pageSize)
        {
            return await ApiResult<CountryDTO>.CreateAsync(
                _context.Countries.AsNoTracking()
                .Select(c => new CountryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ISO2 = c.ISO2,
                    ISO3 = c.ISO3,
                    CitiesCount = c.Cities!.Count
                }),
                pageIndex, pageSize); ;
        }

        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            return country!;
        }



    }
}
