using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.DTO;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Security;

namespace FrontEndTestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        // Properties injected via DI
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;


        // Ctor with DI
        public SeedController(
                    ApplicationDbContext context,
                    RoleManager<IdentityRole> roleManager,
                    UserManager<ApplicationUser> userManager,
                    IWebHostEnvironment env,
                    IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _env = env;
            _configuration = configuration;
        }



        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            string role_RegisteredUser = "RegisteredUser";                                      // Setup Default Role Names
            string role_Administrator = "Administrator";                                        // Setup Default Role Names

            if (await _roleManager.FindByNameAsync(role_RegisteredUser) == null)                // Check if Default Dont Exist Yet
                await _roleManager.CreateAsync(new IdentityRole(role_RegisteredUser)); 

            if (await _roleManager.FindByNameAsync(role_Administrator) == null)                 // Check if Default Dont Exist Yet
                await _roleManager.CreateAsync(new IdentityRole(role_Administrator)); 


            var addedUserList = new List<ApplicationUser>();                                    // Create a List To Track Newly Added Users
            var email_Admin = "admin@email.com";                                                // Check If Admin User Already Exists    


            if (await _userManager.FindByNameAsync(email_Admin) == null)
            {
                var user_Admin = new ApplicationUser()                                          // Create a New Admin ApplicationUser Account
                {
                    SecurityStamp = Guid.NewGuid().ToString(),                                  // Instantiation
                    UserName = email_Admin,                                                     // Instantiation
                    Email = email_Admin,                                                        // Instantiation
                };

                // insert the admin user into the DB
                await _userManager.CreateAsync(user_Admin, _configuration["DefaultPasswords:Administrator"]);

                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);             // Assign "RegisteredUser" Role
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);              // Assign "Administrator" Role

                user_Admin.EmailConfirmed = true;                                               // Confirm Email
                user_Admin.LockoutEnabled = false;                                              // Remove Lockout

                addedUserList.Add(user_Admin);                                                  // Add Admin User to UserList
            }

            var email_User = "user@email.com";

            if (await _userManager.FindByNameAsync(email_User) == null)                         // Check If Standard User Already Exists
            {
                var user_User = new ApplicationUser()                                           // Create a new Application User Account
                {
                    SecurityStamp = Guid.NewGuid().ToString(),                                  // Instantiation
                    UserName = email_User,                                                      // Instantiation
                    Email = email_User                                                          // Instantiation
                };

                // insert the standard user into the DB
                await _userManager.CreateAsync(user_User, _configuration["DefaultPasswords:RegisteredUser"]);

                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);              // Assign "RegisteredUser" Role

                user_User.EmailConfirmed = true;                                                // Confirm
                user_User.LockoutEnabled = false;                                               // Remove Lockout

                addedUserList.Add(user_User);                                                   // Add Standard User to UserList
            }

            // if we added at least one user, persist the changes into the DB
            if (addedUserList.Count > 0)
                await _context.SaveChangesAsync();
            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList
            });
        }


        [HttpGet] // Method that will be run on an HttpGet Request
        public async Task<ActionResult> Import()
        {
            // prevents non-development environments from running this method
            // Seeding only allowed in development
            if (!_env.IsDevelopment()) throw new SecurityException("Not allowed");


            var path = Path.Combine(_env.ContentRootPath, "Data/Source/worldcities.xlsx");


            using var stream = System.IO.File.OpenRead(path);
            using var excelPackage = new ExcelPackage(stream);

            // get the first worksheet 
            var worksheet = excelPackage.Workbook.Worksheets[0];

            // define how many rows we want to process 
            var nEndRow = worksheet.Dimension.End.Row;

            // initialize the record counters 
            var numberOfCountriesAdded = 0;
            var numberOfCitiesAdded = 0;


            //////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////    Populating Countries   ///////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////
            // Countries are imported first because Cities require the Countries foreign key


            // create a lookup dictionary containing all the countries already existing into the Database
            // (it will be empty on first run).
            var countriesByName = _context.Countries
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];
                var countryName = row[nRow, 5].GetValue<string>();
                var iso2 = row[nRow, 6].GetValue<string>();
                var iso3 = row[nRow, 7].GetValue<string>();
                // skip this country if it already exists in the database
                if (countriesByName.ContainsKey(countryName))
                    continue;
                // create the Country entity and fill it with xlsx data 
                var country = new Country
                {
                    Name = countryName,
                    ISO2 = iso2,
                    ISO3 = iso3
                };
                // add the new country to the DB context 
                await _context.Countries.AddAsync(country);
                // store the country in our lookup to retrieve its Id later on
                countriesByName.Add(countryName, country);
                // increment the counter 
                numberOfCountriesAdded++;
            }

            // save all the countries into the Database 
            if (numberOfCountriesAdded > 0)
                await _context.SaveChangesAsync();


            //////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////    Populating Cities   //////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////

            // create a lookup dictionary
            // containing all the cities already existing 
            // into the Database (it will be empty on first run). 

            var cities = _context.Cities
                .AsNoTracking()
                .ToDictionary(x => (
                    Name: x.Name,
                    Lat: x.Lat,
                    Lon: x.Lon,
                    CountryId: x.CountryId));


            // iterates through all rows, skipping the first one 
            for (int nRow = 2; nRow <= nEndRow; nRow++)
            {
                var row = worksheet.Cells[
                    nRow, 1, nRow, worksheet.Dimension.End.Column];
                var name = row[nRow, 1].GetValue<string>();
                var nameAscii = row[nRow, 2].GetValue<string>();
                var lat = row[nRow, 3].GetValue<decimal>();
                var lon = row[nRow, 4].GetValue<decimal>();
                var countryName = row[nRow, 5].GetValue<string>();
                // retrieve country Id by countryName
                var countryId = countriesByName[countryName].Id;
                // skip this city if it already exists in the database
                if (cities.ContainsKey((
                    Name: name,
                    Lat: lat,
                    Lon: lon,
                    CountryId: countryId)))
                    continue;
                // create the City entity and fill it with xlsx data 
                var city = new City
                {
                    Name = name,
                    Lat = lat,
                    Lon = lon,
                    CountryId = countryId
                };
                // add the new city to the DB context 
                _context.Cities.Add(city);
                // increment the counter 
                numberOfCitiesAdded++;
            }


            // save all the cities into the Database
            if (numberOfCitiesAdded > 0)
                await _context.SaveChangesAsync();

            // Method will return a Json Object that will be displayed onto the browser when the method is called
            return new JsonResult(new
            {
                Cities = numberOfCitiesAdded,
                Countries = numberOfCountriesAdded
            });
        }
    }
}
