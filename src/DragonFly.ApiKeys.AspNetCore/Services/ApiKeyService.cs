using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFLy.ApiKeys.AspNetCore.Storage.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Services
{
    /// <summary>
    /// ApiKeyService
    /// </summary>
    class ApiKeyService : IApiKeyService
    {
        public ApiKeyService(MongoIdentityStore store)
        {
            Store = store;
        }

        /// <summary>
        /// Store
        /// </summary>
        private MongoIdentityStore Store { get; }

        public async Task CreateApiKey(ApiKey apiKey)
        {
            MongoApiKey mongo = apiKey.ToMongo();

            await Store.ApiKeys.InsertOneAsync(mongo);

            apiKey.Id = mongo.Id;
        }

        public async Task UpdateApiKey(ApiKey apiKey)
        {
            await Store.ApiKeys.UpdateOneAsync(
                                    Builders<MongoApiKey>.Filter.Eq(x => x.Id, apiKey.Id),
                                    Builders<MongoApiKey>.Update
                                                            .Set(x => x.Name, apiKey.Name)
                                                            .Set(x => x.Value, apiKey.Value)
                                                            .Set(x => x.Permissions, apiKey.Permissions));
        }

        public async Task DeleteApiKey(ApiKey apiKey)
        {
            await Store.ApiKeys.DeleteOneAsync(Builders<MongoApiKey>.Filter.Eq(x => x.Id, apiKey.Id));
        }

        public async Task<ApiKey> GetApiKey(Guid id)
        {
            var result = await Store.ApiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return result.ToModel();
        }

        public async Task<ApiKey> GetApiKey(string value)
        {
            MongoApiKey result = await Store.ApiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Value == value);

            return result.ToModel();
        }

        public async Task<IEnumerable<ApiKey>> GetAllApiKeys()
        {
            var result = await Store.ApiKeys.AsQueryable().ToListAsync();

            return result.Select(x => x.ToModel()).ToList();
        }       
    }
}
