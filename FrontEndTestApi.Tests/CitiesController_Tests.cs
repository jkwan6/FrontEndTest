using FrontEndTestAPI.Controllers;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.ContentModel;

namespace FrontEndTestApi.Tests
{
    public class CitiesController_Tests
    {
        [Fact] // Attribute which tells the App that this is a test
        public async Task GetCity() // Testing the GetCity Method of CitiesController
        {
            // ARRANGE

            ///////////// Creating Db Components to be used for testing //////////////////
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WorldCities")
                .Options;

            // Creating a new instance of an AppDbContext
            // AppDbContext from the Back-End
            using var context = new ApplicationDbContext(options);

            // Adding a new city
            context.Add(new City()
            {
                Id = 1,
                CountryId = 1,
                Lat = 1,
                Lon = 1,
                Name = "TestCity1"
            });
            context.SaveChanges();

            var mock = new Mock<CityService>();
            //////////////////////////////////////////////////////////////////////////////

            // Arranging Assets that will be used for the Test
            // Initializing a CitiesController that takes in the Test AppDbContext
            var controller = new CitiesController(context, mock.Object);
            City? city_existing = null;         // Properties that will be used in the Act Part
            City? city_notExisting = null;      // Properties that will be used in the Act Part

            // ACT --> Calling the GetCity Method on the Class
            city_existing = (await controller.GetCity(1)).Value;
            city_notExisting = (await controller.GetCity(2)).Value;

            // ASSERT --> Asserting the Values that we are expecting
            Assert.NotNull(city_existing);
            Assert.Null(city_notExisting);
        }


    }
}