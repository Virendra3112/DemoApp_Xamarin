using System.Threading.Tasks;

namespace DemoAppXamarin.WebServices
{
    public interface IWebService
    {
        Task<string> GetData<T>(string url) where T : class, new();
    }
}
