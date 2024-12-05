using BHF.MS.test22.Models;

namespace BHF.MS.test22.Services
{
    public interface IExampleService
    {
        Task<HttpResponseMessage> PostSomething(ExampleModel model);
        Task<HttpResponseMessage> GetSomething();
    }
}
