using FrontEndTestAPI.Data.AppDbContext;

namespace FrontEndTestApi.Tests.Controllers.MockObjects
{
    public interface IDbMock
    {
        ApplicationDbContext InMemoryDb();
    }
}