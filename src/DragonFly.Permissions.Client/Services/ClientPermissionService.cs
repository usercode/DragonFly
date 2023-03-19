﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DragonFly.Permissions.Client;

/// <summary>
/// PermissionServiceClient
/// </summary>
public class ClientPermissionService : IPermissionService
{
    public ClientPermissionService(HttpClient client)
    {
        Client = client;
    }

    public HttpClient Client { get; }

    public async Task<IEnumerable<PermissionItem>> GetPermissionsAsync()
    {
        HttpResponseMessage response = await Client.PostAsync("permission/query", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        IEnumerable<PermissionItem>? permissions = await response.Content.ReadFromJsonAsync<IEnumerable<PermissionItem>>();

        if (permissions == null)
        {
            throw new ArgumentNullException(nameof(permissions));
        }

        return permissions;
    }
}
