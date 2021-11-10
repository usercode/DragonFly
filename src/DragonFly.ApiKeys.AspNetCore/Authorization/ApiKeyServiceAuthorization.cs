using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using DragonFLy.ApiKeys;
using DragonFLy.ApiKeys.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ApiKeys.AspNetCore.Authorization
{
    /// <summary>
    /// ApiKeyServiceAuthorization
    /// </summary>
    class ApiKeyServiceAuthorization : IApiKeyService
    {
        public ApiKeyServiceAuthorization(
            IApiKeyService service,
            IDragonFlyApi api
            )
        {
            Service = service;
            Api = api;
        }

        /// <summary>
        /// IdentityService
        /// </summary>
        public IApiKeyService Service { get; }

        /// <summary>
        /// PermissionService
        /// </summary>
        public IDragonFlyApi Api { get; }

        public async Task CreateApiKey(ApiKey apiKey)
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyCreate);

            await Service.CreateApiKey(apiKey);
        }

        public async Task DeleteApiKey(ApiKey apiKey)
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyDelete);

            await Service.DeleteApiKey(apiKey);
        }

        public async Task<IEnumerable<ApiKey>> GetAllApiKeys()
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyQuery);

            return await Service.GetAllApiKeys();
        }

        public async Task<ApiKey> GetApiKey(string value)
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyRead);

            return await Service.GetApiKey(value);
        }

        public async Task<ApiKey> GetApiKey(Guid id)
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyRead);

            return await Service.GetApiKey(id);
        }

        public async Task UpdateApiKey(ApiKey apiKey)
        {
            await Api.AuthorizeAsync(ApiKeyPermissions.ApiKeyUpdate);

            await Service.UpdateApiKey(apiKey);
        }
    }
}
