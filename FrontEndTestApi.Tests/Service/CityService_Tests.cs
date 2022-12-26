using AutoMapper;
using FrontEndTestApi.Tests.Controllers.MockObjects;
using FrontEndTestAPI.Controllers;
using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.AppDbContext;
using FrontEndTestAPI.Data.Migrations;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
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

namespace FrontEndTestApi.Tests.Service
{
    public class CityService_Tests
    {
        [Fact]
        public async void GetCities()
        {
            var inMemoryDb = DbMock.InMemoryDb();
            var mockMapper = new Mock<IMapper>();
            var _service = new CityService(inMemoryDb, mockMapper.Object);
            var pageParams = new PageParameters()
            {
                filterColumn = null,
                filterQuery = null,
                pageIndex = 0,
                pageSize = 10,
                sortColumn = null,
                sortOrder = null
            };

            // ACT --> Calling the GetCity Method on the Class
            var test = await _service.GetCitiesAsync(pageParams);

            Assert.NotNull(test);

            //Task<ActionResult<ApiResult<CityDTO>>> city_existing = _service.GetCities(0, 10);

            // ASSERT --> Asserting the Values that we are expecting

        }
    }
}
