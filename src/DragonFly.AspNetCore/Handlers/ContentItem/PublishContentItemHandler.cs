// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Mediator;
using SmartResults;

namespace DragonFly.AspNetCore.Handlers;

/// <summary>
/// PublishContentItemHandler
/// </summary>
public class PublishContentItemHandler : ICommandHandler<PublishContentItem, Result>
{
    public PublishContentItemHandler(
        IDragonFlyApi api, 
        IContentStorage storage, 
        IPrincipalContext principalContext,
        IEnumerable<IContentInterceptor> contentInterceptors)
    {
        Api = api;
        Storage = storage;
        PrincipalContext = principalContext;
        ContentInterceptors = contentInterceptors;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IContentStorage Storage { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    private IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// ContentInterceptors
    /// </summary>
    private IEnumerable<IContentInterceptor> ContentInterceptors { get; }

    public async ValueTask<Result> Handle(PublishContentItem request, CancellationToken cancellationToken)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(request.Schema, ContentAction.Publish))
            .ThenAsync(async x =>
            {
                bool published = await Storage.PublishAsync(request.Schema, request.ContentId).ConfigureAwait(false);

                if (published == false)
                {
                    return Result.Ok();
                }

                ContentItem? content = await Storage.GetContentAsync(request.Schema, request.ContentId).ConfigureAwait(false);

                if (content != null)
                {
                    //execute interceptors
                    foreach (IContentInterceptor interceptor in ContentInterceptors)
                    {
                        await interceptor.OnPublishedAsync(content).ConfigureAwait(false);
                    }
                }

                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
