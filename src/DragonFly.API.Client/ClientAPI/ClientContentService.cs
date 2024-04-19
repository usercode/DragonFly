// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService
{
    public ClientContentService(HttpClient httpClient, NavigationManager navigationManager)
    {
        Client = httpClient;
        NavigationManager = navigationManager;
    }

    /// <summary>
    /// Client
    /// </summary>
    public HttpClient Client { get; }

    /// <summary>
    /// NavigationManager
    /// </summary>
    private NavigationManager NavigationManager { get; }
}
