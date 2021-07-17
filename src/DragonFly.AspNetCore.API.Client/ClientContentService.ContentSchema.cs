using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Core;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService
    {

        public async Task<ContentSchema> GetContentSchemaAsync(Guid id)
        {
            var response = await Client.GetAsync($"api/schema/{id}");

            var e = await response.Content.ParseJsonAsync<RestContentSchema>();

            return e.ToModel();
        }

        public async Task<ContentSchema> GetContentSchemaAsync(string name)
        {
            var response = await Client.GetAsync($"api/schema/{name}");

            var e = await response.Content.ParseJsonAsync<RestContentSchema>();

            return e.ToModel();
        }

        public async Task CreateSchemaAsync(ContentBase entity)
        {
            var response = await Client.PostAsJson($"api/schema", entity);

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            entity.Id = result.Id;
        }

        public async Task UpdateSchemaAsync(ContentBase entity)
        {
            string type = entity.GetType().Name;

            await Client.PutAsJson($"api/schema/{entity.Id}", entity);
        }

        public async Task<QueryResult<ContentSchema>> GetContentSchemas()
        {
            try
            {
                var response = await Client.PostAsync("api/schema/query", new StringContent(""));

                var restQueryResult = await response.Content.ParseJsonAsync<QueryResult<RestContentSchema>>();

                QueryResult<ContentSchema> queryResult = new QueryResult<ContentSchema>();
                queryResult.Offset = restQueryResult.Offset;
                queryResult.Count = restQueryResult.Count;
                queryResult.TotalCount = restQueryResult.TotalCount;
                queryResult.Items = restQueryResult.Items.Select(x => x.ToModel()).ToList();

                return queryResult;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw;
            }
        }        
    }
}
