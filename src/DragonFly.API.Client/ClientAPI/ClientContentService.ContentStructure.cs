// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IStructureStorage
{

    public async Task<ContentStructure> GetStructureAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/structure/{id}");

        var e = await response.Content.ReadFromJsonAsync<RestContentStructure>(ApiJsonSerializerDefault.Options);

        return e.ToModel();
    }

    public async Task<ContentStructure> GetStructureAsync(string name)
    {
        var response = await Client.GetAsync($"api/structure/{name}");

        var e = await response.Content.ReadFromJsonAsync<RestContentStructure>(ApiJsonSerializerDefault.Options);

        return e.ToModel();
    }

    public async Task CreateAsync(ContentStructure entity)
    {
        var response = await Client.PostAsJsonAsync($"api/structure", entity.ToRest(), ApiJsonSerializerDefault.Options);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentStructure entity)
    {
        await Client.PutAsJsonAsync($"api/structure/{entity.Id}", entity.ToRest(), ApiJsonSerializerDefault.Options);
    }

    public async Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/structure/query", query, ApiJsonSerializerDefault.Options);

        var restQueryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentStructure>>(ApiJsonSerializerDefault.Options);

        return restQueryResult.Convert(x => x.ToModel());
    }

    public async Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query)
    {
        var response = await Client.PostAsync($"api/node/query/{query.Structure}?parentId={query.ParentId}", new StringContent(""));

        var restQueryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentNode>>(ApiJsonSerializerDefault.Options);

        return restQueryResult.Convert(x => x.ToModel());
    }

    public async Task CreateAsync(ContentNode node)
    {
        var response = await Client.PostAsJsonAsync($"api/node", node.ToRest(), ApiJsonSerializerDefault.Options);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options);

        node.Id = result.Id;
    }

    public async Task UpdateAsync(ContentNode node)
    {
        await Client.PutAsJsonAsync($"api/node/{node.Id}", node.ToRest(), ApiJsonSerializerDefault.Options);
    }
}
