// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Mediator;
using SmartResults;

namespace DragonFly.AspNetCore.Handlers;

/// <summary>
/// UpdateContentItemHandler
/// </summary>
public class UpdateContentItemHandler : ICommandHandler<UpdateContentItem, Result>
{
    public UpdateContentItemHandler(
        IDragonFlyApi api, 
        IContentStorage storage, 
        ISchemaStorage schemaStorage, 
        IPrincipalContext principalContext,
        IEnumerable<IContentInterceptor> contentInterceptors)
    {
        Api = api;
        Storage = storage;
        SchemaStorage = schemaStorage;
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
    /// SchemaStorage
    /// </summary>
    private ISchemaStorage SchemaStorage { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    private IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// ContentInterceptors
    /// </summary>
    private IEnumerable<IContentInterceptor> ContentInterceptors { get; }

    public async ValueTask<Result> Handle(UpdateContentItem request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Content);

        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(request.Content.Schema.Name, ContentAction.Update))
            .ThenAsync(async x =>
            {
                ContentSchema? schema = await SchemaStorage.GetSchemaAsync(request.Content.Schema.Name).ConfigureAwait(false);

                ArgumentNullException.ThrowIfNull(schema);

                request.Content.ApplySchema(schema);
                request.Content.Validate();

                if (request.Content.ValidationState.Result == ValidationResult.Invalid)
                {
                    return Result.Failed(ContentErrors.InvalidState);
                }

                await Storage.UpdateAsync(request.Content).ConfigureAwait(false);

                ContentItem? content = await Storage.GetContentAsync(request.Content.Schema.Name, request.Content.Id).ConfigureAwait(false);

                ArgumentNullException.ThrowIfNull(content, $"Content item with schema '{request.Content.Schema.Name}' and ID '{request.Content.Id}' was not found after update.");

                //execute interceptors
                foreach (IContentInterceptor interceptor in ContentInterceptors)
                {
                    await interceptor.OnUpdatedAsync(content).ConfigureAwait(false);
                }

                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
