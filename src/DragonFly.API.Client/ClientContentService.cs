// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService
{
    public ClientContentService(HttpClient httpClient)
    {
        Client = httpClient;
    }

    /// <summary>
    /// Client
    /// </summary>
    public HttpClient Client { get; }

    
}
