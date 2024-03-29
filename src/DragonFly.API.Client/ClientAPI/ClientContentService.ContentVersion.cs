﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IContentVersionStorage
{
    public async Task<IEnumerable<ContentVersionEntry>> GetContentVersionsAsync(string schema, Guid id)
    {
        QueryResult<ContentVersionEntry>? result = await Client.GetFromJsonAsync<QueryResult<ContentVersionEntry>>($"api/content/{schema}/{id}/versions", ApiJsonSerializerDefault.Options);

        if (result == null)
        {
            return Enumerable.Empty<ContentVersionEntry>();
        }

        return result.Items;
    }

    public async Task<ContentItem?> GetContentByVersionAsync(string schema, Guid id)
    {
        RestContentItem? entity = await Client.GetFromJsonAsync<RestContentItem>($"api/content/{schema}/{id}/version", ApiJsonSerializerDefault.Options);

        if (entity == null)
        {
            return null;
        }

        return entity.ToModel();
    }
}
