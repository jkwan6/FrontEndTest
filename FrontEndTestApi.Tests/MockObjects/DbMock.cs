using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DataServices;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrontEndTestApi.Tests.Controllers.MockObjects
{
    public class DbMock : IDbMock
    {
        public ApplicationDbContext InMemoryDb()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };

            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            // Creating a new instance of an AppDbContext
            // AppDbContext from the Back-End
            var context = new ApplicationDbContext(options);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            IEnumerable<City> cities = new List<City>()
            {
            new City() { Id = 1, Name = "Tokyo", Lat = 35.6839m, Lon = 139.7744m, CountryId = 1 },
            new City() { Id = 2, Name = "Jakarta", Lat = -6.2146m, Lon = 106.8451m, CountryId = 2 },
            new City() { Id = 3, Name = "Delhi", Lat = 28.6667m, Lon = 77.2167m, CountryId = 3 },
            new City() { Id = 4, Name = "Manila", Lat = 14.6000m, Lon = 120.9833m, CountryId = 4 },
            new City() { Id = 5, Name = "São Paulo", Lat = -23.5504m, Lon = -46.6339m, CountryId = 5 },
            new City() { Id = 6, Name = "Seoul", Lat = 37.5600m, Lon = 126.9900m, CountryId = 6 },
            new City() { Id = 7, Name = "Mumbai", Lat = 19.0758m, Lon = 72.8775m, CountryId = 3 },
            new City() { Id = 8, Name = "Shanghai", Lat = 31.1667m, Lon = 121.4667m, CountryId = 7 },
            new City() { Id = 9, Name = "Mexico City", Lat = 19.4333m, Lon = -99.1333m, CountryId = 8 },
            new City() { Id = 10, Name = "Guangzhou", Lat = 23.1288m, Lon = 113.2590m, CountryId = 7 },
            new City() { Id = 11, Name = "Cairo", Lat = 30.0444m, Lon = 31.2358m, CountryId = 9 },
            new City() { Id = 12, Name = "Beijing", Lat = 39.904m, Lon = 116.4075m, CountryId = 7 },
            new City() { Id = 13, Name = "New York", Lat = 40.6943m, Lon = -73.9249m, CountryId = 10 },
            new City() { Id = 14, Name = "Kolkāta", Lat = 22.5727m, Lon = 88.3639m, CountryId = 3 },
            new City() { Id = 15, Name = "Moscow", Lat = 55.7558m, Lon = 37.6178m, CountryId = 11 },
            new City() { Id = 16, Name = "Bangkok", Lat = 13.75m, Lon = 100.5167m, CountryId = 12 },
            new City() { Id = 17, Name = "Dhaka", Lat = 23.7289m, Lon = 90.3944m, CountryId = 13 },
            new City() { Id = 18, Name = "Buenos Aires", Lat = -34.5997m, Lon = -58.3819m, CountryId = 14 },
            new City() { Id = 19, Name = "Ōsaka", Lat = 34.752m, Lon = 135.4582m, CountryId = 1 },
            new City() { Id = 20, Name = "Lagos", Lat = 6.45m, Lon = 3.4m, CountryId = 15 },
            new City() { Id = 21, Name = "Istanbul", Lat = 41.01m, Lon = 28.9603m, CountryId = 16 },
            new City() { Id = 22, Name = "Karachi", Lat = 24.86m, Lon = 67.01m, CountryId = 17 },
            new City() { Id = 23, Name = "Kinshasa", Lat = -4.3317m, Lon = 15.3139m, CountryId = 18 },
            new City() { Id = 24, Name = "Shenzhen", Lat = 22.535m, Lon = 114.054m, CountryId = 7 },
            new City() { Id = 25, Name = "Bangalore", Lat = 12.9791m, Lon = 77.5913m, CountryId = 3 },
            new City() { Id = 26, Name = "Ho Chi Minh City", Lat = 10.8167m, Lon = 106.6333m, CountryId = 19 },
            new City() { Id = 27, Name = "Tehran", Lat = 35.7m, Lon = 51.4167m, CountryId = 20 },
            new City() { Id = 28, Name = "Los Angeles", Lat = 34.1139m, Lon = -118.4068m, CountryId = 10 },
            new City() { Id = 29, Name = "Rio de Janeiro", Lat = -22.9083m, Lon = -43.1964m, CountryId = 5 },
            new City() { Id = 30, Name = "Chengdu", Lat = 30.66m, Lon = 104.0633m, CountryId = 7 },
            new City() { Id = 31, Name = "Baoding", Lat = 38.8671m, Lon = 115.4845m, CountryId = 7 },
            new City() { Id = 32, Name = "Chennai", Lat = 13.0825m, Lon = 80.275m, CountryId = 3 },
            new City() { Id = 33, Name = "Lahore", Lat = 31.5497m, Lon = 74.3436m, CountryId = 17 },
            new City() { Id = 34, Name = "London", Lat = 51.5072m, Lon = -0.1275m, CountryId = 21 },
            new City() { Id = 35, Name = "Paris", Lat = 48.8566m, Lon = 2.3522m, CountryId = 22 },
            new City() { Id = 36, Name = "Tianjin", Lat = 39.1467m, Lon = 117.2056m, CountryId = 7 },
            new City() { Id = 37, Name = "Linyi", Lat = 35.0606m, Lon = 118.3425m, CountryId = 7 },
            new City() { Id = 38, Name = "Shijiazhuang", Lat = 38.0422m, Lon = 114.5086m, CountryId = 7 },
            new City() { Id = 39, Name = "Zhengzhou", Lat = 34.7492m, Lon = 113.6605m, CountryId = 7 },
            new City() { Id = 40, Name = "Nanyang", Lat = 32.9987m, Lon = 112.5292m, CountryId = 7 },
            new City() { Id = 41, Name = "Hyderābād", Lat = 17.3617m, Lon = 78.4747m, CountryId = 3 },
            new City() { Id = 42, Name = "Wuhan", Lat = 30.5872m, Lon = 114.2881m, CountryId = 7 },
            new City() { Id = 43, Name = "Handan", Lat = 36.6116m, Lon = 114.4894m, CountryId = 7 },
            new City() { Id = 44, Name = "Nagoya", Lat = 35.1167m, Lon = 136.9333m, CountryId = 1 },
            new City() { Id = 45, Name = "Weifang", Lat = 36.7167m, Lon = 119.1m, CountryId = 7 },
            new City() { Id = 46, Name = "Lima", Lat = -12.06m, Lon = -77.0375m, CountryId = 23 },
            new City() { Id = 47, Name = "Zhoukou", Lat = 33.625m, Lon = 114.6418m, CountryId = 7 },
            new City() { Id = 48, Name = "Luanda", Lat = -8.8383m, Lon = 13.2344m, CountryId = 24 },
            new City() { Id = 49, Name = "Ganzhou", Lat = 25.8292m, Lon = 114.9336m, CountryId = 7 },
            new City() { Id = 50, Name = "Tongshan", Lat = 34.261m, Lon = 117.1859m, CountryId = 7 },
            new City() { Id = 51, Name = "Kuala Lumpur", Lat = 3.1478m, Lon = 101.6953m, CountryId = 25 },
            new City() { Id = 52, Name = "Chicago", Lat = 41.8373m, Lon = -87.6862m, CountryId = 10 },
            new City() { Id = 53, Name = "Heze", Lat = 35.2333m, Lon = 115.4333m, CountryId = 7 },
            new City() { Id = 54, Name = "Chongqing", Lat = 29.55m, Lon = 106.5069m, CountryId = 7 },
            new City() { Id = 55, Name = "Hanoi", Lat = 21.0245m, Lon = 105.8412m, CountryId = 19 },
            new City() { Id = 56, Name = "Fuyang", Lat = 32.8986m, Lon = 115.8045m, CountryId = 7 },
            new City() { Id = 57, Name = "Changsha", Lat = 28.1987m, Lon = 112.9709m, CountryId = 7 },
            new City() { Id = 58, Name = "Dongguan", Lat = 23.0475m, Lon = 113.7493m, CountryId = 7 },
            new City() { Id = 59, Name = "Jining", Lat = 35.4m, Lon = 116.5667m, CountryId = 7 },
            new City() { Id = 60, Name = "Jinan", Lat = 36.6667m, Lon = 116.9833m, CountryId = 7 },
            new City() { Id = 61, Name = "Pune", Lat = 18.5196m, Lon = 73.8553m, CountryId = 3 },
            new City() { Id = 62, Name = "Foshan", Lat = 23.0292m, Lon = 113.1056m, CountryId = 7 },
            new City() { Id = 63, Name = "Bogotá", Lat = 4.6126m, Lon = -74.0705m, CountryId = 26 },
            new City() { Id = 64, Name = "Ahmedabad", Lat = 23.03m, Lon = 72.58m, CountryId = 3 },
            new City() { Id = 65, Name = "Nanjing", Lat = 32.05m, Lon = 118.7667m, CountryId = 7 },
            new City() { Id = 66, Name = "Changchun", Lat = 43.9m, Lon = 125.2m, CountryId = 7 },
            new City() { Id = 67, Name = "Tangshan", Lat = 39.6292m, Lon = 118.1742m, CountryId = 7 },
            new City() { Id = 68, Name = "Cangzhou", Lat = 38.3037m, Lon = 116.8452m, CountryId = 7 },
            new City() { Id = 69, Name = "Dar es Salaam", Lat = -6.8m, Lon = 39.2833m, CountryId = 27 },
            new City() { Id = 70, Name = "Hefei", Lat = 31.8639m, Lon = 117.2808m, CountryId = 7 },
            };

            IEnumerable<Country> countries = new List<Country>()
            {
            new Country() { Id = 1, Name = "Japan", ISO2 = "JP", ISO3 = "JPN" },
            new Country() { Id = 2, Name = "Indonesia", ISO2 = "ID", ISO3 = "IDN" },
            new Country() { Id = 3, Name = "India", ISO2 = "IN", ISO3 = "IND" },
            new Country() { Id = 4, Name = "Philippines", ISO2 = "PH", ISO3 = "PHL" },
            new Country() { Id = 5, Name = "Brazil", ISO2 = "BR", ISO3 = "BRA" },
            new Country() { Id = 6, Name = "South Korea", ISO2 = "KR", ISO3 = "KOR" },
            new Country() { Id = 7, Name = "China", ISO2 = "CN", ISO3 = "CHN" },
            new Country() { Id = 8, Name = "Mexico", ISO2 = "MX", ISO3 = "MEX" },
            new Country() { Id = 9, Name = "Egypt", ISO2 = "EG", ISO3 = "EGY"},
            new Country() { Id = 10, Name = "United States", ISO2 = "US", ISO3 = "USA"},
            new Country() { Id = 11, Name = "Russia", ISO2 = "RU", ISO3 = "RUS"},
            new Country() { Id = 12, Name = "Thailand", ISO2 = "TH", ISO3 = "THA"},
            new Country() { Id = 13, Name = "Bangladesh", ISO2 = "BD", ISO3 = "BGD"},
            new Country() { Id = 14, Name = "Argentina", ISO2 = "AR", ISO3 = "ARG"},
            new Country() { Id = 15, Name = "Nigeria", ISO2 = "NG", ISO3 = "NGA"},
            new Country() { Id = 16, Name = "Turkey", ISO2 = "TR", ISO3 = "TUR"},
            new Country() { Id = 17, Name = "Pakistan", ISO2 = "PK", ISO3 = "PAK"},
            new Country() { Id = 18, Name = "Congo (Kinshasa)", ISO2 = "CD", ISO3 = "COD"},
            new Country() { Id = 19, Name = "Vietnam", ISO2 = "VN", ISO3 = "VNM"},
            new Country() { Id = 20, Name = "Iran", ISO2 = "IR", ISO3 = "IRN"},
            new Country() { Id = 21, Name = "United Kingdom", ISO2 = "GB", ISO3 = "GBR"},
            new Country() { Id = 22, Name = "France", ISO2 = "FR", ISO3 = "FRA"},
            new Country() { Id = 23, Name = "Peru", ISO2 = "PE", ISO3 = "PER"},
            new Country() { Id = 24, Name = "Angola", ISO2 = "AO", ISO3 = "AGO"},
            new Country() { Id = 25, Name = "Malaysia", ISO2 = "MY", ISO3 = "MYS"},
            new Country() { Id = 26, Name = "Colombia", ISO2 = "CO", ISO3 = "COL"},
            new Country() { Id = 27, Name = "Tanzania", ISO2 = "TZ", ISO3 = "TZA"},
            new Country() { Id = 28, Name = "Hong Kong", ISO2 = "HK", ISO3 = "HKG"},
            new Country() { Id = 29, Name = "Chile", ISO2 = "CL", ISO3 = "CHL"},
            new Country() { Id = 30, Name = "Saudi Arabia", ISO2 = "SA", ISO3 = "SAU"},
            };

            context.AddRange(cities);
            context.AddRange(countries);

            //// Adding a new city
            //context.Add(new City()
            //{
            //    Id = 1,
            //    CountryId = 1,
            //    Lat = 1.25m,
            //    Lon = 1,
            //    Name = "TestCity1",
            //    //Country = null
            //});
            //context.Add(new City()
            //{
            //    Id = 2,
            //    CountryId = 1,
            //    Lat = 3,
            //    Lon = 3,
            //    Name = "TestCity3",
            //    //Country = null
            //});

            //context.Add(new Country()
            //{
            //    Id = 1,
            //    Name = "TestCounty",
            //    ISO2 = "ef",
            //    ISO3 = "efe",
            //    Cities = new List<City>()
            //});

            context.SaveChanges();

            return context;
        }
    }
}


//context.AddRange(
//    new City() { Id = 1, Name = "Tokyo", Lat = 35.6839m, Lon = 139.7744m, CountryId = 1 },
//    new City() { Id = 2, Name = "Jakarta", Lat = -6.2146m, Lon = 106.8451m, CountryId = 2 },
//    new City() { Id = 3, Name = "Delhi", Lat = 28.6667m, Lon = 77.2167m, CountryId = 2 }
//    );