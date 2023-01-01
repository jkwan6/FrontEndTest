using Autofac;
using Autofac.Core;
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
        private readonly IContainer _container;

        public CityService_Tests()  // Constructor acting as the DI Container
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DbMock>().As<IDbMock>().InstancePerLifetimeScope();    // Scoped DI
            _container = builder.Build();
        }

        [Fact]
        public async void GetCities()
        {
            // ARRANGE
            var dbMockPrep = _container.Resolve<IDbMock>();
            var dbMock = dbMockPrep.InMemoryDb();
            var mockMapper = new Mock<IMapper>();

            var _service = new CityService(dbMock, mockMapper.Object);
            var pageParams = new PageParameters() 
            { filterColumn = null, filterQuery = null, pageIndex = 0, pageSize = 45, sortColumn = null, sortOrder = null };

            // ACT --> Calling the GetCity Method on the Class
            var methodOutput = await _service.GetCitiesAsync(pageParams);
            var methodOutputCount = methodOutput.Value!.Data.Count();
            var dbContextCount = dbMock.Cities.Count();


            // ASSERT --> Asserting the Values that we are expecting
            Assert.Equal<int>(pageParams.pageSize, methodOutputCount);
        }
    }
}
