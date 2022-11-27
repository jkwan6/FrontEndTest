using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.Data.ApiResults
{
    // Generic Class
    // The ApiResult Class cannot be instantiated from the outside
    // The only way to instantiate it is by using the static class
    public class ApiResult<T>
    {
        //Private Constructor means it can only be instantiated from within
        private ApiResult(
            List<T> data,
            int count,
            int pageIndex,
            int pageSize)
        {
            Data = data;
            Count = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }



        // Static Class can be called without instantiating the class
        // Calling CreateAsync that takes in 3 parameters
        // The method signature means that it needs to return an ApiResult<T>
        // The static class returns an ApiResult<T>
        public static async Task<ApiResult<T>> CreateAsync(
            IQueryable<T> source,   // Source is the DbContext which implements the IQueryable<T> Interface
            int pageIndex,
            int pageSize)
        {
            // Creating the values of the remaining parameters required to instantiate the class
            var count = await source.CountAsync();  // Method to get the total of rows in AppDbContext.Cities
            source = source                         // Using Linq queries on the AppDbContext
                .Skip(pageIndex * pageSize)         // Skip the first N Values on the Db
                .Take(pageSize);                    // Take the first N Values on the Db after the skipped values
            var data = await source.ToListAsync();  // Getting a List of the Values taken


            // Instantiate the class from the static class
            // Can only be instantiated by calling this method
            // This was done because it is not possible to create an async constructor
            // This was made this way because this is the only reasonable way to instantiate this class
            return new ApiResult<T>(
                data,           // List Parameter
                count,
                pageIndex,
                pageSize);
            // This will return a Class with the above parameters as properties
        }

        // The ApiResult will return a List of the Class used in the Generic Class
        // The ApiResult will also return the other properties
        // The ApiResult will be serialized into JSON when sent back
        // I guess it only send back the properties on the object. No business logic
        public List<T> Data { get; private set; }
        public int Count { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 0;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 < TotalPages;
            }
        }
    }
}
