using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys;

public interface IApiKeyService
{
    Task CreateApiKey(ApiKey apiKey);

    Task UpdateApiKey(ApiKey apiKey);

    Task DeleteApiKey(ApiKey apiKey);

    Task<ApiKey> GetApiKey(string value);

    Task<ApiKey> GetApiKey(Guid id);

    Task<IEnumerable<ApiKey>> GetAllApiKeys();
}
