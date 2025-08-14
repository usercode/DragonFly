// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Mediator;
using SmartResults;

namespace DragonFly.AspNetCore.Handlers;

/// <summary>
/// CreateContentItemHandler
/// </summary>
public class CreateContentItemHandler : ICommandHandler<CreateContentItem, Result>
{
    public CreateContentItemHandler(
        IDragonFlyApi api,
        IContentStorage storage,
        ISchemaStorage schemaStorage,
        IPrincipalContext principalContext,
        IEnumerable<IContentInterceptor> contentInterceptors,
        IDateTimeService dateService)
    {
        Api = api;
        Storage = storage;
        SchemaStorage = schemaStorage;
        PrincipalContext = principalContext;
        ContentInterceptors = contentInterceptors;
        DateService = dateService;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// DateService
    /// </summary>
    private IDateTimeService DateService { get; }

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

    public async ValueTask<Result> Handle(CreateContentItem request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Content);

        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(request.Content.Schema.Name, ContentAction.Create))
            .ThenAsync(async x =>
            {
                if (request.Content.Id == Guid.Empty)
                {
                    request.Content.Id = Guid.NewGuid();
                }

                DateTime now = DateService.Current();

                request.Content.CreatedAt = now;
                request.Content.ModifiedAt = now;

                ContentSchema? schema = await SchemaStorage.GetSchemaAsync(request.Content.Schema.Name).ConfigureAwait(false);

                request.Content.ApplySchema(schema);
                request.Content.Validate();

                await Storage.CreateAsync(request.Content).ConfigureAwait(false);

                //execute interceptors
                foreach (IContentInterceptor interceptor in ContentInterceptors)
                {
                    await interceptor.OnCreatedAsync(request.Content).ConfigureAwait(false);
                }

                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
