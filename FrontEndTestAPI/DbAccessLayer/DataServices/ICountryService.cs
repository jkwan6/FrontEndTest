﻿using FrontEndTestAPI.Data.ApiResult;
using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.Data_Models.POCO;
using FrontEndTestAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndTestAPI.DbAccessLayer.DataServices
{
    public interface ICountryService
    {
        Task<ActionResult<ApiResult<CountryDTO>>> GetCountriesAsync(PageParameters pageParams);
        Task<ActionResult<Country>> GetCountryAsync(int id);


    }
}
