// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.WebHooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DragonFly.API;
using DragonFly.AspNetCore.Builders;
using Microsoft.AspNetCore.Http;
using DragonFly.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace DragonFly.AspNetCore;

/// <summary>
/// StartupExtensions
/// </summary>
public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds the DragonFly services.
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
    /// <see cref="IAssetProcessing"/> -> <see cref="ImageAssetProcessing"/>, <see cref="PdfAssetProcessing"/>
    /// <br/><br/>
    /// Default permissions:<br/>
    /// <see cref="ContentPermissions"/>, <see cref="SchemaPermissions"/>, <see cref="AssetPermissions"/>, <see cref="BackgroundTaskPermissions"/>, <see cref="WebHookPermissions"/>
    /// </summary>
    public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
    {
        services.AddSingleton<IDragonFlyApi, DragonFlyApi>();
        services.AddSingleton<IDateTimeService, LocalDateTimeService>();
        services.AddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();

        services.AddSingleton(ContentFieldManager.Default);
        services.AddSingleton(AssetMetadataManager.Default);

        services.AddSingleton<ISlugService, SlugService>();

        services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        //assets
        services.AddTransient<IAssetProcessing, ImageAssetProcessing>();
        services.AddTransient<IAssetProcessing, PdfAssetProcessing>();

        //permissions
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddSignalR();

        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder.Init(api =>
        {
            api.ContentFields().AddDefaults();
            api.AssetMetadatas().AddDefaults();
            api.Permission().AddDefaults();
        });

        return builder;
    }

    /// <summary>
    /// Initialize the DragonFLy API by executing the following services: <see cref="IPreInitialize"/>, <see cref="IInitialize"/> and <see cref="IPostInitialize"/>.
    /// </summary>
    public static async Task InitDragonFlyAsync(this IHost host)
    {
        IDragonFlyApi api = host.Services.GetRequiredService<IDragonFlyApi>();
        await api.InitAsync();
    }

    /// <summary>
    /// Maps the DragonFly routings.
    /// </summary>
    public static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder, Action<IDragonFlyMiddlewareBuilder> fullbuilder)
    {
        builder.Map("/dragonfly",
                                x =>
                                {
                                    DragonFlyMiddlewareBuilder end = new DragonFlyMiddlewareBuilder();
                                    fullbuilder(end);

                                    x.UseRouting();
                                    x.UseAuthentication();
                                    x.UseAuthorization();

                                    end.PreAuthBuilders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

                                    //allows only requests from authenticated users
                                    x.Use(async (context, next) =>
                                    {
                                        if (context.User.Identity?.IsAuthenticated == false)
                                        {
                                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                        }
                                        else
                                        {
                                            Permission.Enable();
                                            Permission.SetCurrentPrincipal(context.User);

                                            await next(context);
                                        }
                                    });

                                    end.Builders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

                                    x.UseEndpoints(e =>
                                    {
                                        end.EndpointList.Foreach(a => a(new DragonFlyEndpointBuilder(e)));

                                        e.MapHub<BackgroundTaskHub>("/taskhub").RequireAuthorization(BackgroundTaskPermissions.BackgroundTaskQuery);
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
