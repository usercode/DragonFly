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
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : ISchemaStorage
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
