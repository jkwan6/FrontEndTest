﻿using System;
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
        private readonly ApplicationDbContext _context; // Properties
        private readonly ICountryService _service;       // Properties
        #endregion

        #region DI Contructor
        public CountriesController(ApplicationDbContext context, ICountryService service)    // Constructor
        {
            _context = context;     // DI Injection
            _service = service;     // DI Injection
        }
        #endregion

        // GET: Country List
        [HttpGet]
        public async Task<ActionResult<ApiResult<CountryDTO>>> GetCountries(
            [FromQuery] PageParameters pageParams)
        {
            var result = _service.GetCountriesAsync(pageParams);  // Calls the Service Layer
            return await result;                                                                            // Returns the Results
        }

        // GET: Individual City
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            Task<ActionResult<Country>> result = _service.GetCountryAsync(id);   // Calls the Service Layer 
            if (result is null) { return NotFound(); }                      // Check is Result is Null
            return await result;                                            // Returns Result if not Null
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
