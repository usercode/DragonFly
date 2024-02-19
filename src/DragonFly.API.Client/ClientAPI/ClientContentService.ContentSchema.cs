// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : ISchemaStorage
{
    public async Task<ContentSchema?> GetSchemaAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/schema/{id}");

        RestContentSchema? e = await response.Content.ReadFromJsonAsync<RestContentSchema>(ApiJsonSerializerDefault.Options);

        if (e == null)
        {
            return null;
        }

        return e.ToModel();
    }

    public async Task<ContentSchema?> GetSchemaAsync(string name)
    {
        var response = await Client.GetAsync($"api/schema/{name}");

        RestContentSchema? e = await response.Content.ReadFromJsonAsync<RestContentSchema>(ApiJsonSerializerDefault.Options);

        if (e == null)
        {
            return null;
        }

        return e.ToModel();
    }

    public async Task CreateAsync(ContentSchema entity)
    {
        var response = await Client.PostAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerDefault.Options);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(ContentSchema entity)
    {
        await Client.PutAsJsonAsync($"api/schema", entity.ToRest(), ApiJsonSerializerDefault.Options);
    }

    public async Task DeleteAsync(ContentSchema entity)
    {
        await Client.DeleteAsync($"api/schema/{entity.Id}");
    }

    public async Task<QueryResult<ContentSchema>> QuerySchemasAsync()
    {
        var response = await Client.PostAsync("api/schema/query", new StringContent(string.Empty));

        QueryResult<RestContentSchema>? queryResult = await response.Content.ReadFromJsonAsync<QueryResult<RestContentSchema>>(ApiJsonSerializerDefault.Options) ?? throw new ArgumentNullException();

        return queryResult.Convert(x => x.ToModel());
    }
}
