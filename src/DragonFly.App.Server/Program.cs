using System;
using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.Identity.AspNetCore.MongoDB;
using DragonFly.MongoDB.Options;
using DragonFLy.ApiKeys;
using ImageWizard.Core.ImageCaches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<FileCacheSettings>(builder.Configuration.GetSection("AssetCache"));

//DragonFly
builder.Services.AddDragonFly()
                    .AddRestApi()
                    .AddGraphQLApi()
                    .AddMongoDbStorage()
                    .AddMongoDbIdentity()
                    .AddSchemaBuilder()
                    .AddBlockField()
                    .AddApiKeys()
                    .AddPermissions()
                    ;

var app = builder.Build();

IDragonFlyApi api = app.Services.GetRequiredService<IDragonFlyApi>();
await api.InitAsync();

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseDragonFly(x =>
                    {
                        x.MapApiKey();
                        x.MapIdentity();
                        x.MapRestApi();
                        x.MapPermission();
                    });
app.UseDragonFlyManager();

app.Run();