// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using DragonFly.Core;
using DragonFly.Init;
using DragonFly.Permissions;
using DragonFly.AspNetCore.Builders;
using Microsoft.AspNetCore.Components.Authorization;
using DragonFly.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using DragonFly.AspNetCore.Permissions;

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
    /// <see cref="BoolField"/>, <see cref="StringField"/>, <see cref="SlugField"/>, <see cref="TextField"/>, <see cref="IntegerField"/>, <see cref="FloatField"/>, <see cref="HtmlField"/>, <see cref="ColorField"/>, <see cref="GeolocationField"/><br />
    /// <see cref="AssetField"/>, <see cref="ReferenceField"/>
    /// <br /><br />
    /// Default asset metadata: <br/>
    /// <see cref="ImageMetadata"/>, <see cref="PdfMetadata"/>, <see cref="VideoMetadata"/><br /><br />
    /// Default services::<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br />
    /// <see cref="IDateTimeService"/> -> <see cref="LocalDateTimeService"/><br />
    /// <see cref="IAssetProcessing"/> -> <see cref="ImageProcessing"/>, <see cref="PdfProcessing"/>, <see cref="VideoProcessing"/>
    /// <br/><br/>
    /// Default permissions:<br/>
    /// <see cref="ContentPermissions"/>, <see cref="SchemaPermissions"/>, <see cref="AssetPermissions"/>, <see cref="BackgroundTaskPermissions"/>, <see cref="WebHookPermissions"/>
    /// </summary>
    public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
    {
        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder
            .AddCore()
            .PostInit<CreateContentPermissionsInitializer>();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

        builder.Services.TryAddSingleton<IDateTimeService, LocalDateTimeService>();
        builder.Services.TryAddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();
        builder.Services.TryAddSingleton<IBackgroundTaskService>(x => x.GetRequiredService<IBackgroundTaskManager>());

        builder.Services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        //assets
        builder.Services.AddTransient<IAssetProcessing, ImageProcessing>();
        builder.Services.AddTransient<IAssetProcessing, PdfProcessing>();
        builder.Services.AddTransient<IAssetProcessing, VideoProcessing>();

        builder.Services.AddSingleton<IPrincipalContext, PrincipalContext>();

        //builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        //builder.Services.AddScoped<AuthenticationStateProvider, BlazorServerAuthenticationStateProvider>();

        return builder;
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

                                        Principal.Current = context.User;

                                        await next(context);
                                    });

                                    x.UseEndpoints(e =>
                                    {
                                        IEnumerable<DragonFlyEndpointHandler> endpointHandlers =  e.ServiceProvider.GetServices<DragonFlyEndpointHandler>();

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
    /// Maps the DragonFly manager. (Blazor WebAssembly)
    /// </summary>
    public static IApplicationBuilder UseDragonFlyManager(this IApplicationBuilder builder)
    {
        builder.Map("/manager",
                                x =>
                                {
                                    x.UseBlazorFrameworkFiles();
                                    x.UseStaticFiles();
                                    x.UseRouting();
                                    x.UseEndpoints(endpoints => endpoints.MapFallbackToFile("index.html"));
                                }
            );

        return builder;
    }

    /// <summary>
    /// Maps the DragonFly manager. (Blazor Server)
    /// </summary>
    public static IApplicationBuilder UseDragonFlyManager<TApp>(this IApplicationBuilder builder, Assembly[]? AdditionalAssemblies = null)
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
                        .MapRazorComponents<TApp>()
                        .AddInteractiveServerRenderMode()
                        .AddInteractiveWebAssemblyRenderMode()
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
    /// Adds SignalR hub for background tasks.
    /// </summary>
    public static IDragonFlyBuilder AddBackgroundTaskHub(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSignalR();

        builder.AddEndpoint(x => x.MapHub<BackgroundTaskHub>("/background-task-hub")
                                                      .RequirePermission(BackgroundTaskPermissions.QueryBackgroundTask)
                                                      .WithDisplayName("DragonFly.BackgroundTask.Hub"));

        return builder;
    }
}
