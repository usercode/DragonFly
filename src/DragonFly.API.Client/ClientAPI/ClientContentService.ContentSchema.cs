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
public partial class ClientContentService : ISchemaStorage
{

    public async Task<ContentSchema> GetSchemaAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/schema/{id}");

        var e = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.RestContentSchema);

        return e.ToModel();
    }

    public async Task<ContentSchema> GetSchemaAsync(string name)
    {
        var response = await Client.GetAsync($"api/schema/{name}");

        var e = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.RestContentSchema);

        return e.ToModel();
    }

    public async Task CreateAsync(ContentSchema entity)
    {
        var response = await Client.PostAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerContext.Default.RestContentSchema);

        var result = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.ResourceCreated);

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentSchema entity)
    {
        await Client.PutAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerContext.Default.RestContentSchema);
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

            QueryResult<RestContentSchema>? queryResult = await response.Content.ReadFromJsonAsync(ApiJsonSerializerContext.Default.QueryResultRestContentSchema);

            return queryResult.Convert(x => x.ToModel());
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);

            throw;
        }
    }        
}
