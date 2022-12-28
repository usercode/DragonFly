// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics;
using System.Net.Http.Json;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IStructureStorage
{

    public async Task<ContentStructure> GetStructureAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/structure/{id}");

        var e = await response.Content.ReadFromJsonAsync<RestContentStructure>();

        return e.ToModel();
    }

    public async Task<ContentStructure> GetStructureAsync(string name)
    {
        var response = await Client.GetAsync($"api/structure/{name}");

        var e = await response.Content.ReadFromJsonAsync<RestContentStructure>();

        return e.ToModel();
    }

    public async Task CreateAsync(ContentStructure entity)
    {
        var response = await Client.PostAsJsonAsync($"api/structure", entity);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentStructure entity)
    {
        string type = entity.GetType().Name;

        await Client.PutAsJsonAsync($"api/structure/{entity.Id}", entity);
    }

    public async Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query)
    {
        try
        {
            var response = await Client.PostAsJsonAsync("api/structure/query", query);

            var restQueryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentStructure>>();

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

        var restQueryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentNode>>();

        QueryResult<ContentNode> queryResult = new QueryResult<ContentNode>();
        queryResult.Offset = restQueryResult.Offset;
        queryResult.Count = restQueryResult.Count;
        queryResult.TotalCount = restQueryResult.TotalCount;
        queryResult.Items = restQueryResult.Items.Select(x => x.ToModel()).ToList();

        return queryResult;
    }

    public async Task CreateAsync(ContentNode node)
    {
        var response = await Client.PostAsJsonAsync($"api/node", node);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        node.Id = result.Id;
    }

    public async Task UpdateAsync(ContentNode node)
    {
        string type = node.GetType().Name;

        await Client.PutAsJsonAsync($"api/node/{node.Id}", node);
    }
}
