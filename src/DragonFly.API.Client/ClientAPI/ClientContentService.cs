// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IDataStorage
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
