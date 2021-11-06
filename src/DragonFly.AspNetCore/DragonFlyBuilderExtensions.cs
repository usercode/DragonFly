using DragonFly.AspNetCore;
using DragonFly.AspNet.Middleware;
using DragonFly.AspNet.Middleware.Builders;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore.Middleware;
using DragonFly.AspNetCore.Services;
using DragonFly.Core.Builders;
using DragonFly.Core.WebHooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;
using ImageWizard;
using ImageWizard.DocNET;
using DragonFly.Core;
using DragonFly.AspNetCore.Middleware.Builders;

namespace DragonFly.AspNetCore;

/// <summary>
/// StartupExtensions
/// </summary>
public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
    {
        services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();

        services.AddSingleton<DragonFlyContext>();
        services.AddSingleton<IDragonFlyApi, DragonFlyApi>();
        services.AddSingleton<IDateTimeService, DateTimeService>();

        services.AddSingleton(ContentFieldManager.Default);
        services.AddSingleton(AssetMetadataManager.Default);

        services.AddTransient<IAssetProcessing, ImageAssetProcessing>();
        services.AddTransient<IAssetProcessing, PdfAssetProcessing>();

        //ImageWizard
        //services.ConfigureOptions<HttpLoaderConfigureOptions>();

        services.AddImageWizard(x =>
        {
            x.AllowUnsafeUrl = true;
        })
            .AddImageSharp()
            .AddSvgNet()
            .AddDocNET()
            .AddHttpLoader()
            .SetFileCache();

        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder.Init(api =>
        {
            api.ContentField().AddDefaults();
            api.AssetMetadata().AddDefaults();
        });

        return builder;
    }

    public static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder, Action<IDragonFlyFullBuilder> fullbuilder)
    {
        builder.Map("/dragonfly",
                                x =>
                                {
                                    DragonFlyFullBuilder end = new DragonFlyFullBuilder();
                                    fullbuilder(end);

                                    x.UseRouting();
                                    x.UseAuthentication();
                                    x.UseAuthorization();

                                    end.PreAuthBuilders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

                                    x.UseMiddleware<RequireAuthentificationMiddleware>();

                                    end.PostAuthBuilders.Foreach(a => a(new DragonFlyApplicationBuilder(x)));

                                    x.UseImageWizard();

                                    x.UseEndpoints(e =>
                                    {
                                        end.EndpointList.Foreach(a => a(new DragonFlyEndpointRouteBuilder(e)));
                                    });
                                }
            );

        return builder;
    }

    public static IApplicationBuilder UseDragonFlyManager(this IApplicationBuilder builder)
    {
        return UseDragonFlyManager(builder, "/manager");
    }

    private static IApplicationBuilder UseDragonFlyManager(this IApplicationBuilder builder, PathString basePath)
    {
        builder.Map(basePath,
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
