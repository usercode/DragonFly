// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using Results;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class ContentVersionPermissionStorage : IContentVersionStorage
{
    public ContentVersionPermissionStorage(IContentVersionStorage storage, IDragonFlyApi api)
    {
        Storage = storage;
        Api = api;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IContentVersionStorage Storage { get; }

    public async Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id)
    {
        return await Api.AuthorizeContentAsync(schema, DragonFly.Permissions.ContentAction.Read)
                        .ThenAsync(x => Storage.GetContentByVersionAsync(schema, id));
    }

    public async Task<Result<IEnumerable<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        return await Api.AuthorizeContentAsync(schema, DragonFly.Permissions.ContentAction.Read)
                        .ThenAsync(x => Storage.GetContentVersionsAsync(schema, id));
    }
}
