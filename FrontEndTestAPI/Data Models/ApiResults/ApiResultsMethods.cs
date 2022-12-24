using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Reflection;

namespace FrontEndTestAPI.Data.ApiResult
{
    public partial class ApiResult<T>
    {
        // This one is to shield against SQL Injection Attacks. Not sure how.
        public static bool IsValidProperty(
            string propertyName,                            // propertyName is the name of the parameter
            bool throwExceptionIfNotFound = true)           // throwExceptionIfNotFound is the name of the parameter
        {
            var prop = typeof(T).GetProperty(               // T is the class type that is passsed into ApiResult
                propertyName,                               // Will if propertyName matches a property in <T>
                BindingFlags.IgnoreCase |                       // To Ignore the case of the name
                BindingFlags.Public |                           // To Include Public Properties in the Search
                BindingFlags.Instance);                         // Must specify Instance or Static to get a return

            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    String.Format(
                        $"Error: Property '{propertyName}' does not exist."
                        ));
            return prop != null;
        }

        public static bool IsNotNullOrEmptyProperty(string? prop)
        {
            if (!string.IsNullOrEmpty(prop))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidAndNotNullProperty(string? prop)
        {
            if (IsNotNullOrEmptyProperty(prop) && IsValidProperty(prop!))
            {
                return true;
            }
            return false;
        }
    }
}