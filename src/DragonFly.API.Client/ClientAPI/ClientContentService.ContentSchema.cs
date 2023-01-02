// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics;
using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
internal partial class ClientContentService : ISchemaStorage
{

    public async Task<ContentSchema> GetSchemaAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/schema/{id}");

        var e = await response.Content.ReadFromJsonAsync<RestContentSchema>();

        return e.ToModel();
    }

    public async Task<ContentSchema> GetSchemaAsync(string name)
    {
        var response = await Client.GetAsync($"api/schema/{name}");

        var e = await response.Content.ReadFromJsonAsync<RestContentSchema>();

        return e.ToModel();
    }

    public async Task CreateAsync(ContentSchema entity)
    {
        var response = await Client.PostAsJsonAsync($"api/schema", entity.ToRest());

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentSchema entity)
    {
        string type = entity.GetType().Name;

        await Client.PutAsJsonAsync($"api/schema", entity.ToRest());
    }

    public async Task DeleteAsync(ContentSchema entity)
    {
        await Client.DeleteAsync($"api/schema/{entity.Id}");
    }

    public async Task<QueryResult<ContentSchema>> QuerySchemasAsync()
    {
        try
        {
            var response = await Client.PostAsync("api/schema/query", new StringContent(""));

            var restQueryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentSchema>>();

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
