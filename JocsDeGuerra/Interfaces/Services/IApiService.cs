using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IApiService
    {
        Task<bool> Delete<T>(string url) where T : new();
        Task<string> Get(string uri);
        Task<int> Post<T>(string url, T payload) where T : new();
        Task<int> Put<T>(string url, T payload) where T : new();
    }
}