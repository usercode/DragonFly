// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using DragonFly.Core;
using DragonFly.Init;
using DragonFly.Permissions;
using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DragonFly.AspNetCore.Permissions;
using AspNetCore.Decorator;
using DragonFly.AspNetCore.Permissions.Storages;
using Microsoft.AspNetCore.SignalR;
using DragonFly.Client;

namespace DragonFly.AspNetCore;

/// <summary>
/// StartupExtensions
/// </summary>
public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds DragonFly services.
    /// <br /><br />
    /// Default fields: <br />
    /// <see cref="BoolField"/>, <see cref="StringField"/>, <see cref="SlugField"/>, <see cref="TextField"/>, <see cref="IntegerField"/>, <see cref="FloatField"/>, <see cref="HtmlField"/>, <see cref="ColorField"/>, <see cref="GeolocationField"/>, <see cref="BlockField"/>,<br />
    /// <see cref="AssetField"/>, <see cref="ReferenceField"/>
    /// <br /><br />
    /// Default asset metadata: <br/>
    /// <see cref="ImageMetadata"/>, <see cref="PdfMetadata"/>, <see cref="VideoMetadata"/><br /><br />
    /// Default services::<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br />
    /// <see cref="IDateTimeService"/> -> <see cref="LocalDateTimeService"/><br />
    /// <br/><br/>
    /// Default manager:<br/>
    /// <see cref="FieldManager"/>, <see cref="MetadataManager"/>
    /// <br/><br/>
    /// Default permissions:<br/>
    /// <see cref="ContentPermissions"/>, <see cref="SchemaPermissions"/>, <see cref="AssetPermissions"/>, <see cref="BackgroundTaskPermissions"/>, <see cref="WebHookPermissions"/>
    /// <br /><br />
    /// At the end all storage interfaces are decorated by permission storages.
    /// </summary>
    public static IServiceCollection AddDragonFly(this IServiceCollection services, Action<IDragonFlyBuilder>? config = null)
    {
        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder
            .AddCore()
            .PostInit<CreateContentPermissionsInitializer>();

        builder.Services.AddRazorComponents()
                           .AddInteractiveServerComponents();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Services.TryAddSingleton<IDateTimeService, LocalDateTimeService>();
        builder.Services.TryAddSingleton<BackgroundTaskManager>();
        builder.Services.TryAddSingleton<IBackgroundTaskManager>(x => x.GetRequiredService<BackgroundTaskManager>());
        builder.Services.TryAddSingleton<IBackgroundTaskService>(x => x.GetRequiredService<IBackgroundTaskManager>());

        builder.Services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        builder.Services.AddSingleton<IPrincipalContext, PrincipalContext>();

        //builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        config?.Invoke(builder);

        //adds authorization layer to storages
        builder.Services.Decorate<IContentStorage, ContentPermissionStorage>();
        builder.Services.Decorate<IAssetStorage, AssetPermissionStorage>();
        builder.Services.Decorate<IAssetFolderStorage, AssetFolderPermissionStorage>();
        builder.Services.Decorate<ISchemaStorage, SchemaPermissionStorage>();
        builder.Services.Decorate<IWebHookStorage, WebHookPermissionStorage>();
        builder.Services.Decorate<IBackgroundTaskManager, BackgroundTaskPermissionStorage>();

        return services;
    }

    /// <summary>
    /// Initializes the DragonFly CMS by executing the following services: <see cref="IPreInitialize"/>, <see cref="IInitialize"/> and <see cref="IPostInitialize"/>.
    /// </summary>
    public static async Task InitDragonFlyAsync(this IHost host)
    {
        IDragonFlyApi api = host.Services.GetRequiredService<IDragonFlyApi>();
        await api.InitAsync();
    }

    /// <summary>
    /// Maps the DragonFly routings.
    /// </summary>
    public static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder)
    {
        builder.Map("/dragonfly",
                                x =>
                                {
                                    IEnumerable<DragonFlyBuilderHandler> handlers = x.ApplicationServices.GetServices<DragonFlyBuilderHandler>();

                                    foreach (DragonFlyBuilderHandler handler in handlers)
                                    {
                                        handler(x);
                                    }

                                    x.UseRouting();
                                    x.UseAuthentication();
                                    x.UseAuthorization();

                                    x.Use(async (context, next) =>
                                    {
                                        Endpoint? endpoint = context.GetEndpoint();

                                        //Allows only authenticated access (except for explicit allowed anonymous endpoints).
                                        if (endpoint == null || endpoint.Metadata.Any(m => m is AllowAnonymousAttribute) == false)
                                        {
                                            if (context.User == null || context.User.Identities.Any(i => i.IsAuthenticated) == false)
                                            {
                                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                                return;
                                            }
                                        }

                                        IPrincipalContext principal = context.RequestServices.GetRequiredService<IPrincipalContext>();
                                        principal.Current = context.User;

                                        await next(context);
                                    });

                                    x.UseEndpoints(e =>
                                    {
                                        IEnumerable<DragonFlyEndpointHandler> endpointHandlers = e.ServiceProvider.GetServices<DragonFlyEndpointHandler>();

                                        foreach (DragonFlyEndpointHandler endpointHandler in endpointHandlers)
                                        {
                                            endpointHandler(e);
                                        }
                                    });
                                }
            );

        return builder;
    }

    /// <summary>
    /// Maps the DragonFly manager. (Blazor Server)
    /// </summary>
    public static IApplicationBuilder UseDragonFlyManager<TApp>(this IApplicationBuilder builder)
    {
        builder.Map("/manager", x =>
        {
            x.UseRouting();
            x.UseAuthentication();
            x.UseAuthorization();
            x.UseAntiforgery();
            x.UseEndpoints(endpoints =>
            {
                endpoints
                        .MapStaticAssets();

                endpoints
                        .MapRazorComponents<TApp>()
                        .AddInteractiveServerRenderMode()
                        .AddAdditionalAssemblies(RazorRoutingManager.Default.Items.ToArray());
            });
        });

        return builder;
    }

    /// <summary>
    /// Adds an endpoint to DragonFly.
    /// </summary>
    public static IDragonFlyBuilder AddEndpoint(this IDragonFlyBuilder builder, Action<IEndpointRouteBuilder> endpoint)
    {
        builder.Services.AddTransient(x => new DragonFlyEndpointHandler(endpoint));

        return builder;
    }

    /// <summary>
    /// UseApplicationBuilder
    /// </summary>
    public static IDragonFlyBuilder UseApplicationBuilder(this IDragonFlyBuilder builder, Action<IApplicationBuilder> action)
    {
        builder.Services.AddTransient(x => new DragonFlyBuilderHandler(action));

        return builder;
    }

    /// <summary>
    /// Adds SignalR hub for background tasks.
    /// </summary>
    public static IDragonFlyBuilder AddBackgroundTaskHub(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSignalR();

        builder.AddEndpoint(x => x.MapHub<BackgroundTaskHub>("/background-task-hub")
                                            .RequirePermission(BackgroundTaskPermissions.QueryBackgroundTask)
                                            .WithDisplayName("DragonFly.BackgroundTaskHub"));

        builder.PostInit(x =>
        {
            var backgroundManager = x.ServiceProvider.GetRequiredService<BackgroundTaskManager>();
            var hub = x.ServiceProvider.GetRequiredService<IHubContext<BackgroundTaskHub>>();

            backgroundManager.BackgroundTaskChanged += async (state, task) =>
            {
                await hub.Clients.All.SendAsync("TaskChanged", state, task);
            };
        });

        return builder;
    }
}
