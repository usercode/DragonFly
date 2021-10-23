using System;
using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Identity;
using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.MongoDB.Options;
using ImageWizard.Core.ImageCaches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
            
builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<FileCacheSettings>(builder.Configuration.GetSection("AssetCache"));

//DragonFly
builder.Services.AddDragonFly()
                    .AddRestApi()
                    .AddGraphQLApi()
                    .AddMongoDbStorage()
                    //.AddIdentityEF(db => db.UseSqlite("Filename=./dragonfly_identity.db"))
                    .AddIdentityMongoDb(db => db.ConnectionString = "mongodb://localhost/DragonFly_Identity")
                    .AddSchemaBuilder()
                    .AddBlockField();

var app = builder.Build();

IDragonFlyApi api = app.Services.GetRequiredService<IDragonFlyApi>();
await api.InitAsync();

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

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
                    x.UseIdentity();
                });
app.UseDragonFlyManager();

app.Run();
        
