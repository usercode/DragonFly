using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Data.Models;
using DragonFly.Data.Models.Assets;
using DragonFly.Data.Models.WebHooks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.Data
{
    /// <summary>
    /// MongoStore
    /// </summary>
    public partial class MongoStorage : IWebHookStorage
    {
        public async Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query)
        {
            IQueryable<MongoWebHook> q = WebHooks.AsQueryable();

            if (query.Event != null)
            {
                q = q.Where(x => x.EventName == query.Event);
            }

            var items = q.ToList().Select(x => x.ToModel()).ToList();

            return new QueryResult<WebHook>() { Items = items };
        }

        public async Task<WebHook> GetAsync(Guid id)
        {
            MongoWebHook? result = WebHooks.AsQueryable().FirstOrDefault(x => x.Id == id);

            if(result == null)
            {
                throw new Exception();
            }

            return result.ToModel();
        }

        public async Task CreateAsync(WebHook webHook)
        {
            MongoWebHook m = webHook.ToMongo();

            await WebHooks.InsertOneAsync(m);

            webHook.Id = m.Id;
        }

        public async Task UpdateAsync(WebHook webHook)
        {
            await WebHooks.ReplaceOneAsync(Builders<MongoWebHook>.Filter.Eq(x=> x.Id, webHook.Id), webHook.ToMongo());
        }

        public async Task DeleteAsync(WebHook webHook)
        {
            await WebHooks.DeleteOneAsync(Builders<MongoWebHook>.Filter.Eq(x => x.Id, webHook.Id));
        }
    }
}
