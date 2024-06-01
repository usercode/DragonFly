// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentStructureStorage
/// </summary>
internal class ContentStructureApiStorage : IStructureStorage
{
    public ContentStructureApiStorage(HttpClient client)
    {
        Client = client;
    }

    private HttpClient Client { get; }

    public async Task<ContentStructure> GetStructureAsync(Guid id)
    {
        var result = await Client
                            .GetAsync($"api/structure/{id}")
                            .ToResultAsync<RestContentStructure>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.ToModel());
    }

    public async Task<ContentStructure> GetStructureAsync(string name)
    {
        var result = await Client
                                .GetAsync($"api/structure/{name}")
                                .ToResultAsync<RestContentStructure>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.ToModel());
    }

    public async Task CreateAsync(ContentStructure entity)
    {
        var result = await Client
                                .PostAsJsonAsync($"api/structure", entity.ToRest(), ApiJsonSerializerDefault.Options)
                                .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            entity.Id = result.Value.Id;
        }
    }

    public async Task UpdateAsync(ContentStructure entity)
    {
        await Client.PutAsJsonAsync($"api/structure/{entity.Id}", entity.ToRest(), ApiJsonSerializerDefault.Options);
    }

    public async Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query)
    {
        var result = await Client
                                .PostAsJsonAsync("api/structure/query", query, ApiJsonSerializerDefault.Options)
                                .ToResultAsync<QueryResult<RestContentStructure>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(x => x.ToModel()));
    }

    public async Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query)
    {
        var result = await Client
                                .PostAsync($"api/node/query/{query.Structure}?parentId={query.ParentId}", new StringContent(""))
                                .ToResultAsync<QueryResult<RestContentNode>>(ApiJsonSerializerDefault.Options);

        return result.ToResult(x => x.Convert(x => x.ToModel()));
    }

    public async Task CreateAsync(ContentNode node)
    {
        var result = await Client
                            .PostAsJsonAsync($"api/node", node.ToRest(), ApiJsonSerializerDefault.Options)
                            .ToResultAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        if (result.IsSucceeded)
        {
            node.Id = result.Value.Id;
        }
    }

    public async Task UpdateAsync(ContentNode node)
    {
        await Client.PutAsJsonAsync($"api/node/{node.Id}", node.ToRest(), ApiJsonSerializerDefault.Options);
    }
}
