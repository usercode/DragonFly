// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.AspNetCore;
using DragonFly.MongoDB;
using DragonFlyTemplate.Models;
using DragonFlyTemplate.Startup;
using ImageWizard;
using ImageWizard.Caches;

var builder = WebApplication.CreateBuilder(args);

//DragonFly
builder.Services.AddOptions<DragonFlyOptions>().BindConfiguration("General");
builder.Services.AddOptions<MongoDbOptions>().BindConfiguration("MongoDB");

//ImageWizard
builder.Services.AddOptions<ImageWizardOptions>().BindConfiguration("ImageWizard");
builder.Services.AddOptions<FileCacheOptions>().BindConfiguration("AssetCache");

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
                    .AddModels(x => x
                        .Add<BlogPostModel>()
                        .Add<StandardPageModel>()
                        );

//builder.Services.Configure<KestrelServerOptions>(options =>
//{
//    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
//});

var app = builder.Build();

//init DragonFly
await app.InitDragonFlyAsync();

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
                        .MapImageWizard(requireAuthorization : false)
                        .MapApiKey()
                        .MapIdentity()
                        .MapRestApi());
app.UseDragonFlyManager();
app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

await app.RunAsync();
