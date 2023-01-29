// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using DragonFly.AspNet.Middleware.Builders;
using DragonFly.AspNetCore.Middleware;
using DragonFly.Core.WebHooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.AspNetCore.Middleware.Builders;
using Microsoft.Extensions.Hosting;
using DragonFly.API;
using DragonFly.AspNetCore.Builders;

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
    /// </summary>
    public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
    {
        services.AddSingleton<DragonFlyContext>();
        services.AddSingleton<IDragonFlyApi, DragonFlyApi>();
        services.AddSingleton<IDateTimeService, LocalDateTimeService>();

        services.AddSingleton(ContentFieldManager.Default);
        services.AddSingleton(AssetMetadataManager.Default);

        services.AddTransient<IAssetProcessing, ImageAssetProcessing>();
        services.AddTransient<IAssetProcessing, PdfAssetProcessing>();

        services.AddSingleton<ISlugService, SlugService>();

        services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder.Init(api =>
        {
            api.ContentFields().AddDefaults();
            api.AssetMetadatas().AddDefaults();
        });

        return builder;
    }

    /// <summary>
    /// Initialize the DragonFLy API by executing the following services: <see cref="IPreInitialize"/>, <see cref="IInitialize"/> and <see cref="IPostInitialize"/>.
    /// </summary>
    public static async Task InitDragonFly(this IHost host)
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

                                    x.UseMiddleware<RequireAuthentificationMiddleware>();

                                    end.Builders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

                                    x.UseEndpoints(e =>
                                    {
                                        end.EndpointList.Foreach(a => a(new DragonFlyEndpointBuilder(e)));
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
