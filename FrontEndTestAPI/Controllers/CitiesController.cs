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
using FrontEndTestAPI.DataServices;
using FrontEndTestAPI.Data.ApiResult;
using AutoMapper;
using FrontEndTestAPI.Data_Models.POCO;

// AutoGenerated Class from Visual Studio and EF Core
// Parameters in the Methods and the Parameter names from the Front End need to match
// Refer to Model Binding

namespace FrontEndTestAPI.Controllers
{
    [Route("api/[controller]")] // Routing Rule for class
    [ApiController]
    public class CitiesController : ControllerBase
    {
        #region Properties 
        private readonly ApplicationDbContext _context;     // Properties
        private readonly ICityService _service;             // Properties
        private readonly IMapper _mapper;                   // Properties
        #endregion

        #region Constructor / DI Injection
        public CitiesController(ApplicationDbContext context, ICityService service, IMapper mapper)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }
        #endregion

        // GET: City List
        // Originally Returned a JSON Array of all the Cities in the Database
        // Task<ActionResult> is needed for Async Stuff
        // The GetCities Method Signature means that it will return an ApiResult<City> Instance
        // The ApiResult<City> will have its properties set from the FrontEnd and Db Values
        // The Method will return an ApiResult<City> Instance in JSON Format
        // Tje returned class will be serialized in Json Format CamelCase
        [HttpGet]
        public async Task<ActionResult<ApiResult<CityDTO>>> GetCities( 
            [FromQuery] PageParameters pageParams)
        {
            var result = await _service.GetCitiesAsync(pageParams);     // Calls the Service Layer
            return result;                                              // Returns Value
        }





        // GET: Individual City
        // Returns a Single JSON Object containing a single City
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _service.GetCityAsync(id);           // Calls the Service Layer
            if (city is null) { return NotFound(); }        // Check if City is Null
            return city;                          // Returns City if not Null
        }







        // PUT: api/Cities/5
        // Allow us to modify an Existing City
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // Allow us to add a new City
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        // Allow us to delete an Existing City
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}