﻿using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models.WebHooks;
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
using DragonFly.AspNetCore.API.Exports;

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IWebHookStorage
{

    public async Task<WebHook> GetAsync(Guid id)
    {
        var response = await Client.GetAsync($"api/webhook/{id}");

        var e = await response.Content.ReadFromJsonAsync<RestWebHook>();

        return e.ToModel();
    }

    public async Task CreateAsync(WebHook entity)
    {
        var response = await Client.PostAsJsonAsync($"api/webhook", entity);

        var result = await response.Content.ReadFromJsonAsync<ResourceCreated>();

        entity.Id = result.Id;
    }

    public async Task UpdateAsync(WebHook entity)
    {
        await Client.PutAsJsonAsync($"api/webhook/{entity.Id}", entity);
    }

    public async Task DeleteAsync(WebHook webHook)
    {
        await Client.DeleteAsync($"api/webhook/{webHook.Id}");
    }

    public async Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query)
    {
        var response = await Client.PostAsJsonAsync("api/webhook/query", query);

        QueryResult<RestWebHook>? result = await response.Content.ReadFromJsonAsync<QueryResult<RestWebHook>>();

        IList<WebHook> items = result.Items.Select(x => x.ToModel()).ToList();

        return new QueryResult<WebHook>() { Items = items };
    }
}
