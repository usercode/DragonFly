// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IContentStorage, ISchemaStorage, IAssetStorage, IAssetFolderStorage, IWebHookStorage, IStructureStorage
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
