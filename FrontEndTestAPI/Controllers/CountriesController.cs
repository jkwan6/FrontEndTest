using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.DataTransferObjects;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.DataServices;
using System.Drawing;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DbAccessLayer.DataServices;

namespace FrontEndTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        #region Properties
        private readonly ApplicationDbContext _context;     // Properties
        private readonly ICountryService _service;          // Properties
        #endregion

        #region DI Contructor
        public CountriesController(ApplicationDbContext context, ICountryService service)    // Constructor
        {
            _context = context;     // DI Injection
            _service = service;     // DI Injection
        }
        #endregion

        #region Http Get All Method || Using Service Layer
        [HttpGet]
        public async Task<ActionResult<ApiResult<CountryDTO>>> GetCountries(
            [FromQuery] PageParameters pageParams)
        {
            var result = _service.GetCountriesAsync(pageParams);
            return await result;                                                                            // Returns the Results
        }
        #endregion

        #region Http Get(Id) Method || Using Service Layer
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            ActionResult<Country> result = await _service.GetCountryAsync(id);      // Calls the Service Layer 
            return result is null ? NotFound() : result;
            #region Non-Ternary Variation
            //if (result is null) { return NotFound(); }
            //return result;
            #endregion
        }
        #endregion

        #region Http Put(Id) Method || Using Service Layer
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            var result = await _service.PutCountryAsync(id, country);
            return (IActionResult)result;
        }
        #endregion

        #region Http Post(Model) Method || Using Service Layer
        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            var result = await _service.PostCountryAsync(country);
            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }
        #endregion

        #region Http Delete(Id) Method || Using Service Layer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var result = _service.DeleteCountryAsync(id);
            return (IActionResult)result;
        }
        #endregion
    }
}
