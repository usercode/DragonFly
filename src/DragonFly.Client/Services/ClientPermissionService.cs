// Copyright (c) usercode
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

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        HttpResponseMessage response = await Client.PostAsync("api/permission/query", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        IEnumerable<Permission>? permissions = await response.Content.ReadFromJsonAsync<IEnumerable<Permission>>();

        ArgumentNullException.ThrowIfNull(permissions);

        return permissions;
    }
}
