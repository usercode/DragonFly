using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Rest.Middlewares.Logins;
using DragonFly.AspNet.Middleware;
using DragonFly.AspNet.Middleware.Builders;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore.Middleware;
using DragonFly.AspNetCore.Services;
using DragonFly.Contents.Content.Fields;
using DragonFly.Core.Assets;
using DragonFly.Core.Builders;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content.Parts.Base;
using ImageWizard;
using ImageWizard.DocNET;
using ImageWizard.MongoDB;

namespace DragonFly.Core
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddDragonFly(this IServiceCollection services)
        {
            services.AddHttpClient<IContentInterceptor, WebHookInterceptor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                            .AddCookie(x =>
                                            {
                                                x.Events.OnRedirectToLogin = context =>
                                                {
                                                    context.Response.StatusCode = StatusCodes.Status401Unauthorized; return Task.CompletedTask;
                                                };
                                            } );
            services.AddAuthorization();

            services.AddSingleton<DragonFlyContext>();
            services.AddSingleton<IDragonFlyApi, DragonFlyApi>();

            //ImageWizard
            services.ConfigureOptions<HttpLoaderConfigureOptions>();
            //services.ConfigureOptions<MongoDbConfigureOptions>();

            services.AddImageWizard(x =>
            {
                x.AllowUnsafeUrl = true;
            })
                .AddImageSharp()
                .AddSvgNet()
                .AddDocNET()
                .AddHttpLoader()
                .SetFileCache();

            services.AddSingleton(ContentFieldManager.Default);

            return new DragonFlyBuilder(services);
        }

        public static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder, Action<IDragonFlyApplicationBuilder> dragonFlyBuilder)
        {
            return UseDragonFly(builder, "/dragonfly", dragonFlyBuilder);
        }

        private static IApplicationBuilder UseDragonFly(this IApplicationBuilder builder, PathString basePath, Action<IDragonFlyApplicationBuilder> dragonFlyBuilder)
        {
            builder.Map(basePath,
                                    x =>
                                    {
                                        x.UseRouting();
                                        x.UseAuthentication();
                                        x.UseAuthorization();
                                        x.UseMiddleware<InternalApiKeyMiddleware>();
                                        x.UseMiddleware<LoginMiddleware>();
                                        x.UseMiddleware<RequireAuthentificationMiddleware>();
                                        x.UseImageWizard();

                                        dragonFlyBuilder(new DragonFlyApplicationBuilder(x));
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
                                        //x.UseStaticFiles(basePath);
                                        x.UseRouting();
                                        x.UseEndpoints(endpoints => endpoints.MapFallbackToFile("index.html"));
                                    }
                );

            return builder;
        }
    }
}
