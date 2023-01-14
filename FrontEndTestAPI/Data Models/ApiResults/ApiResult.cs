using FrontEndTestAPI.Data_Models.POCO;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace FrontEndTestAPI.Data.ApiResult
{

    enum SortEnum {ASC=1, DESC=2}
    // Generic Class
    // The ApiResult Class cannot be instantiated from the outside
    // The only way to instantiate it is by using the static class
    public partial class ApiResult<T>  // This class encapsulates the DTOs
    {
        private ApiResult(              // Private Constructor - Instantiate from Within
            List<T> data,                                                               
            int count,                                                                  
            int pageIndex,                                                              
            int pageSize,
            string? sortColumn,
            string? sortOrder,
            string? filterColumn,
            string? filterQuery)
        {
            Data = data;
            Count = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }

        #region Properties
        public List<T> Data { get; private set; }                                       // Property
        public int Count { get; private set; }                                          // Property
        public int PageIndex { get; private set; }                                      // Property
        public int PageSize { get; private set; }                                       // Property
        public int TotalPages { get; private set; }                                     // Property
        public bool HasPreviousPage { get { return PageIndex > 0; } }                   // Property
        public bool HasNextPage { get { return PageIndex + 1 < TotalPages; } }          // Property
        public string? SortColumn { get; set; }                                         // Property
        public string? SortOrder { get; set; }                                          // Property
        public string? FilterColumn { get; set; }                                       // Property
        public string? FilterQuery { get; set; }                                        // Property
        #endregion

        // Static Class can be called without instantiating the class
        // Calling CreateAsync that takes in 3 parameters
        // The method signature means that it needs to return an ApiResult<T>
        // The static class returns an ApiResult<T>
        public static async Task<ApiResult<T>> CreateAsync(IQueryable<T> queryable, PageParameters pageParams)
        {
            if (IsNotNullOrEmptyProperty(pageParams.filterColumn))
            {
                if (IsNotNullOrEmptyProperty(pageParams.filterQuery))
                {
                    if (IsValidProperty(pageParams.filterColumn!))
                    {
                        // Executing the Queryable to a List
                        queryable = queryable.Where(
                            string.Format("{0}.Contains(@0)", pageParams.filterColumn), pageParams.filterQuery);
                    }

                }
            }

            var count = await queryable.CountAsync();  // Get Total Rows in AppDbContext

            if(IsValidAndNotNullProperty(pageParams.sortColumn))   // If Property is NOT Null AND Property is Valid
            {
                if (IsNotNullOrEmptyProperty(pageParams.sortOrder) && pageParams.sortOrder!.ToUpper() == SortEnum.ASC.ToString())
                {
                    pageParams.sortOrder = SortEnum.ASC.ToString();
                }
                else
                {
                    pageParams.sortOrder = SortEnum.DESC.ToString();
                }
                queryable = queryable.OrderBy(string.Format("{0} {1}", pageParams.sortColumn, pageParams.sortOrder)); // String Interpolation
            }

            queryable = queryable                         // Linq after sorting                 
                .Skip(pageParams.pageIndex * pageParams.pageSize)         // Skip the first N Values on the Db
                .Take(pageParams.pageSize);                    // Take the first N Values on the Db after the skipped values
            var data = await queryable.ToListAsync();  // Getting a List of the Values taken

            #region comments
            // Instantiate the class from the static class
            // Can only be instantiated by calling this method
            // This was done because it is not possible to create an async constructor
            // This was made this way because this is the only reasonable way to instantiate this class
            #endregion
            return new ApiResult<T>(
                data,                   //  <-- List of Objects
                count,
                pageParams.pageIndex,
                pageParams.pageSize,
                pageParams.sortColumn,
                pageParams.sortOrder,
                pageParams.filterColumn,
                pageParams.filterQuery);
        }
            #region comments
        // The ApiResult will return a List of the Class used in the Generic Class
        // The ApiResult will also return the other properties
        // The ApiResult will be serialized into JSON when sent back
        // I guess it only send back the properties on the object. No business logic
        #endregion
    }
}
