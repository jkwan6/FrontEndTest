using FrontEndTestAPI.Controllers;
using FrontEndTestAPI.Data.ApiResults;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataServices;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndTestApi.Tests
{
    public class CityService_Tests
    {
        [Fact]
        public Task GetCities()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "WorldCities")
            .Options;

            using var context = new ApplicationDbContext(options);
            context.Add(new City()
            {
                Id = 1,
                CountryId = 1,
                Lat = 1,
                Lon = 1,
                Name = "TestCity1"
            });
            context.SaveChanges();
            CityService _service = new CityService(context);

            // ACT --> Calling the GetCity Method on the Class
            Task<ActionResult<ApiResult<CityDTO>>> city_existing = _service.GetCities(0, 10);

            // ASSERT --> Asserting the Values that we are expecting
            Assert.NotNull(city_existing);
            return Task.CompletedTask;
        }
    }
}
