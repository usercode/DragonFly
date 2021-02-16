using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore.GraphQL;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Parts.Base;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.ContentItems.Queries;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Data.Models.Assets;
using DragonFly.ImageSharp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ImageWizard;
using ImageWizard.DocNET;
using DragonFly.Client.Core.Assets;
using DragonFly.MongoDB.Options;
using DragonFly.ContentTypes;
using DragonFly.Contents.Content.Schemas;
using DragonFly.Data.Content;
using DragonFly.Models;

namespace DragonFly.AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DragonFlyOptions>(Configuration.GetSection("General"));
            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDB"));

            //DragonFly
            services.AddDragonFly()
                        .AddImageSharpProcessing()
                        .AddRestApi()
                        .AddGraphQLApi()
                        .AddMongoDbStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDragonFlyApi api)
        {
            api.Init();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();
            app.UseDragonFly(
                           x =>
                           {
                               x.UseRestApi();
                               x.UseGraphQLApi();
                           });
            app.UseDragonFlyManager();
        }
    }
}
