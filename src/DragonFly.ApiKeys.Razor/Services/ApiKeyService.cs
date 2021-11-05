using DragonFLy.ApiKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;

namespace DragonFly.ApiKeys.Razor.Services
{
    /// <summary>
    /// ApiKeyService
    /// </summary>
    class ApiKeyService : IApiKeyService
    {
        public ApiKeyService(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task CreateApiKey(ApiKey apiKey)
        {
            throw new NotImplementedException();
        }

        public Task DeleteApiKey(ApiKey apiKey)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApiKey>> GetAllApiKeys()
        {
            HttpResponseMessage response = await Client.PostAsync("api/apikey/query", new StringContent(string.Empty) );

            response.EnsureSuccessStatusCode();

            IEnumerable<ApiKey>? result = await response.Content.ReadFromJsonAsync<IEnumerable<ApiKey>>();

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public Task<ApiKey> GetApiKey(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ApiKey> GetApiKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateApiKey(ApiKey apiKey)
        {
            throw new NotImplementedException();
        }
    }
}
