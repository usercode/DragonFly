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
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Data.Models.Assets;
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
using DragonFly.Client.Core.Assets;
using DragonFly.MongoDB.Options;
using DragonFly.ContentTypes;
using DragonFly.Data.Content;
using DragonFly.Models;
using DragonFly.SampleData;
using DragonFly.AspNetCore.SchemaBuilder;
using DragonFly.Fields.BlockField.Storage;
using ImageWizard.Settings;
using ImageWizard.Core.ImageCaches;
using DragonFly.AspNetCore.Identity;
using DragonFly.AspNetCore.Identity.EF;
using Microsoft.EntityFrameworkCore;
using DragonFly.AspNetCore.Identity.MongoDB;

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
            services.Configure<FileCacheSettings>(Configuration.GetSection("AssetCache"));

            //DragonFly
            services.AddDragonFly()
                        .AddRestApi()
                        .AddGraphQLApi()
                        .AddMongoDbStorage()
                        //.AddIdentityEF(db => db.UseSqlite("Filename=./dragonfly_identity.db"))
                        //.AddIdentityMongoDb(db => db.ConnectionString = "mongodb://localhost/DragonFly_Identity")
                        .AddSchemaBuilder()
                        .AddBlockField();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDragonFlyApi api)
        {
            api.InitAsync().GetAwaiter().GetResult();

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
