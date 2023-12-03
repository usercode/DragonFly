// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using DragonFly.Core;
using DragonFly.Init;
using DragonFly.Permissions;
using DragonFly.AspNetCore.Builders;

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
    /// <see cref="ImageMetadata"/>, <see cref="PdfMetadata"/><br /><br />
    /// Default services::<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br />
    /// <see cref="IDateTimeService"/> -> <see cref="LocalDateTimeService"/><br />
    /// <see cref="IAssetProcessing"/> -> <see cref="ImageProcessing"/>, <see cref="PdfProcessing"/>
    /// <br/><br/>
    /// Default permissions:<br/>
    /// <see cref="ContentPermissions"/>, <see cref="SchemaPermissions"/>, <see cref="AssetPermissions"/>, <see cref="BackgroundTaskPermissions"/>, <see cref="WebHookPermissions"/>
    /// </summary>
    public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
    {
        AuthenticationBuilder authenticationBuilder = services.AddAuthentication();
        services.AddAuthorization();

        IDragonFlyBuilder builder = new DragonFlyBuilder(services, authenticationBuilder);
        builder
            .AddCore()
            .Init(api =>
            {
                api.Permission().AddDefaults();
            })
            .PostInit<CreateContentPermissionsInitializer>();

        builder.Services.AddSingleton<IDateTimeService, LocalDateTimeService>();
        builder.Services.AddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();

        //manager
        builder.Services.AddSingleton(PermissionManager.Default);

        builder.Services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        //assets
        builder.Services.AddTransient<IAssetProcessing, ImageProcessing>();
        builder.Services.AddTransient<IAssetProcessing, PdfProcessing>();

        //builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        builder.Services.AddSignalR();        

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
    public static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder, Action<IDragonFlyMiddlewareBuilder>? subBuilder = null)
    {
        builder.Map("/dragonfly",
                                x =>
                                {
                                    DragonFlyMiddlewareBuilder end = new DragonFlyMiddlewareBuilder();
                                    subBuilder?.Invoke(end);

                                    end.Builders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

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

                                        PermissionPrincipal.SetCurrent(context.User);

                                        await next(context);
                                    });

                                    x.UseEndpoints(e =>
                                    {
                                        end.EndpointList.Foreach(a => a(new DragonFlyEndpointBuilder(e)));

                                        e.MapHub<BackgroundTaskHub>("/background-task-hub")
                                                        .RequirePermission(BackgroundTaskPermissions.QueryBackgroundTask)
                                                        .WithDisplayName("DragonFly.BackgroundTask.Hub");
                                    });
                                }
            );

        return builder;
    }

    /// <summary>
    /// Maps the DragonFly manager.
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
}
