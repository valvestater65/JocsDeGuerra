using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    //An extremely simple REST Client service. 
    public class ApiService : IApiService
    {

        private const string KEY = "API_KEY";
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(NamedClients.FIREBASE_CLIENT);
        }

        public async Task<string> Get(string uri)
        {
            try
            {
                var response = await _client.GetAsync($"{uri}.json?auth={KEY}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Post<T>(string url, T payload) where T : new()
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(payload));
                var response = await _client.PostAsync($"{url}.json?auth={KEY}", content);

                if (!response.IsSuccessStatusCode)
                {
                    return -9;
                }

                return 0;
            }
            catch (Exception)
            {
                return -9;
            }
        }

        public async Task<int> Put<T>(string url, T payload) where T : new()
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(payload));
                var response = await _client.PutAsync($"{url}.json?auth={KEY}", content);

                if (!response.IsSuccessStatusCode)
                {
                    return -9;
                }

                return 0;
            }
            catch (Exception)
            {

                return -9;
            }
        }

        public async Task<bool> Delete(string url)
        {
            try
            {
                var response = await _client.DeleteAsync($"{url}.json?auth={KEY}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Patch<T>(string url, T payload) where T : new()
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(payload));
                var response = await _client.PatchAsync($"{url}.json?auth={KEY}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
