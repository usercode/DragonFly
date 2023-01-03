// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.AspNetCore;
using DragonFly.AspNet.Options;
using DragonFly.MongoDB;
using DragonFlyTemplate.Models;
using DragonFlyTemplate.Pages;
using DragonFlyTemplate.Startup;
using ImageWizard;
using ImageWizard.Caches;

var builder = WebApplication.CreateBuilder(args);

//DragonFly
builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

//ImageWizard
builder.Services.Configure<ImageWizardOptions>(builder.Configuration.GetSection("ImageWizard"));
builder.Services.Configure<FileCacheOptions>(builder.Configuration.GetSection("AssetCache"));

builder.Services.AddRazorPages();
builder.Services.AddSingleton<DataSeeding>();

//DragonFly services
builder.Services.AddDragonFly()
                    .AddImageWizard()
                    .AddRestApi()
                    .AddMongoDbStorage()
                    .AddMongoDbIdentity()
                    .AddBlockField()                    
                    .AddApiKeys()
                    .AddPermissions()
                    .AddProxy(x => x
                                    .AddType<StandardPageModel>()
                                    .AddType<BlogPostModel>());

var app = builder.Build();

//init DragonFly
await app.InitDragonFly();

//data seeding
DataSeeding seeding = app.Services.GetRequiredService<DataSeeding>();
await seeding.StartAsync();

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseDragonFly(x => x
                        .MapImageWizard(requireAuthentication: false)
                        .MapApiKey()
                        .MapIdentity()
                        .MapRestApi()
                        .MapPermission());
app.UseDragonFlyManager();
app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
