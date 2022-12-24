using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndTestApi.Tests.Controllers.MockObjects
{
    public class DbMock
    {

        public static ApplicationDbContext InMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "WorldCities")
            .Options;

            // Creating a new instance of an AppDbContext
            // AppDbContext from the Back-End
            var context = new ApplicationDbContext(options);

            // Adding a new city
            context.Add(new City()
            {
                Id = 1,
                CountryId = 1,
                Lat = 1,
                Lon = 1,
                Name = "TestCity1",
                //Country = null
            });
            context.Add(new City()
            {
                Id = 2,
                CountryId = 1,
                Lat = 3,
                Lon = 3,
                Name = "TestCity3",
                //Country = null
            });

            context.Add(new Country()
            {
                Id = 1,
                Name = "TestCounty",
                ISO2 = "ef",
                ISO3 = "efe",
                Cities = new List<City>()
            }); ;





            context.SaveChanges();

            return context;
        }





    }
}
