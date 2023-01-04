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
using Azure;
using Microsoft.AspNetCore.Authorization;

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
        private readonly ICityService _service;             // Properties
        #endregion

        #region Constructor / DI Injection
        public CitiesController(ApplicationDbContext context, ICityService service, IMapper mapper)
        {
            _service = service;
        }
        #endregion

        #region Http Get All Method     || Using Service Layer
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
        #endregion

        #region Http Get(Id) Method     || Using Service Layer
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _service.GetCityAsync(id);             // Calls the Service Layer
            return city is null ? NotFound() : Ok(city);
            #region Non-Ternary
            //if (city is null) { return NotFound(); }                // Check if City is Null
            //return city;                                            // Returns City if not Null
            #endregion
        }
        #endregion

        #region Http Put(Id) Method     || Using Service Layer
        //[Authorize(Roles = "RegisteredUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)     // Id is coming from the Url Route
        {
            var response = await _service.PutCityAsync(id, city);
            var result = new ObjectResult(response.Content);
            return result;
        }
        #endregion

        #region Http Post(Model) Method || Using Service Layer
        //[Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            var result = await _service.PostCityAsync(city);
            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }
        #endregion

        #region Http Delete(Id) Method  || Using Service Layer
        //[Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var response = await _service.DeleteCityAsync(id);
            var result = new ObjectResult(response.Content);
            return result;
        }
        #endregion

        #region  Http Post IsDupeCity(Model) || Using Service Layer
        [HttpPost]
        [Route("isDupeCity")]
        public bool IsDupeCity(City city)
        {
            var results = _service.IsDupeCity(city);
            return results;
        }
        #endregion
    }
}
