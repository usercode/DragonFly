// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.Identity.AspNetCore.MongoDB;
using DragonFly.ImageWizard;
using DragonFly.MongoDB;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
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

//DragonFly
builder.Services.AddDragonFly()
                    .AddImageWizard(x =>
                    {
                        x.AddOpenGraphLoader();
                    })
                    .AddRestApi()
                    .AddMongoDbStorage()
                    .AddMongoDbIdentity()
                    .AddBlockField()
                    .AddProxies(x => 
                    {
                        x.AddType<StandardPageModel>();
                        x.AddType<BlogPostModel>();
                    })
                    .AddPermissions()
                    ;

builder.Services.AddRazorPages();

builder.Services.AddSingleton<DataSeeding>();

var app = builder.Build();

//init DragonFly CMS
IDragonFlyApi api = app.Services.GetRequiredService<IDragonFlyApi>();
await api.InitAsync();

//data seeding
DataSeeding seeding = app.Services.GetRequiredService<DataSeeding>();
await seeding.StartAsync();

IContentStorage contentStorage = app.Services.GetRequiredService<IContentStorage>();

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseDragonFly(x =>
{
    x.MapImageWizard(requireAuthentication: false);
    x.MapIdentity();
    x.MapRestApi();
    x.MapPermission();
});
app.UseDragonFlyManager();
app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
