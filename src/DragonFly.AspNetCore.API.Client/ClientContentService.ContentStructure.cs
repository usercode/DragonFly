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
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService : IStructureStorage
    {

        public async Task<ContentStructure> GetStructureAsync(Guid id)
        {
            var response = await Client.GetAsync($"api/structure/{id}");

            var e = await response.Content.ParseJsonAsync<RestContentStructure>();

            return e.ToModel();
        }

        public async Task<ContentStructure> GetStructureAsync(string name)
        {
            var response = await Client.GetAsync($"api/structure/{name}");

            var e = await response.Content.ParseJsonAsync<RestContentStructure>();

            return e.ToModel();
        }

        public async Task CreateAsync(ContentStructure entity)
        {
            var response = await Client.PostAsJson($"api/structure", entity);

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            entity.Id = result.Id;
        }

        public async Task UpdateAsync(ContentStructure entity)
        {
            string type = entity.GetType().Name;

            await Client.PutAsJson($"api/structure/{entity.Id}", entity);
        }

        public async Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query)
        {
            try
            {
                var response = await Client.PostAsJson("api/structure/query", query);

                var restQueryResult = await response.Content.ParseJsonAsync<QueryResult<RestContentStructure>>();

                QueryResult<ContentStructure> queryResult = new QueryResult<ContentStructure>();
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

        public async Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query)
        {
            var response = await Client.PostAsync($"api/node/query/{query.Structure}?parentId={query.ParentId}", new StringContent(""));

            var restQueryResult = await response.Content.ParseJsonAsync<QueryResult<RestContentNode>>();

            QueryResult<ContentNode> queryResult = new QueryResult<ContentNode>();
            queryResult.Offset = restQueryResult.Offset;
            queryResult.Count = restQueryResult.Count;
            queryResult.TotalCount = restQueryResult.TotalCount;
            queryResult.Items = restQueryResult.Items.Select(x => x.ToModel()).ToList();

            return queryResult;
        }

        public async Task CreateAsync(ContentNode node)
        {
            var response = await Client.PostAsJson($"api/node", node);

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            node.Id = result.Id;
        }

        public async Task UpdateAsync(ContentNode node)
        {
            string type = node.GetType().Name;

            await Client.PutAsJson($"api/node/{node.Id}", node);
        }
    }
}
