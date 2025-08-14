// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Mediator;
using SmartResults;

namespace DragonFly.AspNetCore.Handlers;

/// <summary>
/// DeleteContentItemHandler
/// </summary>
public class DeleteContentItemHandler : ICommandHandler<DeleteContentItem, Result>
{
    public DeleteContentItemHandler(
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

    public async ValueTask<Result> Handle(DeleteContentItem request, CancellationToken cancellationToken)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(request.Schema, ContentAction.Delete))
            .ThenAsync(async x =>
            {
                ContentItem? content = await Storage.GetContentAsync(request.Schema, request.ContentId).ConfigureAwait(false);

                if (content is null)
                {
                    return Result.Ok();
                }

                await Storage.DeleteAsync(request.Schema, request.ContentId).ConfigureAwait(false);

                //execute interceptors
                foreach (IContentInterceptor interceptor in ContentInterceptors)
                {
                    await interceptor.OnDeletedAsync(content).ConfigureAwait(false);
                }

                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
