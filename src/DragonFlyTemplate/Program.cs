// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.Identity.AspNetCore.MongoDB;
using DragonFly.ImageWizard;
using DragonFly.MongoDB;
using DragonFly.Proxy;
using DragonFly.Proxy.Query;
using DragonFly.Query;
using DragonFly.Storage;
using DragonFlyTemplate;
using DragonFlyTemplate.Models;
using DragonFlyTemplate.Startup;
using ImageWizard;
using ImageWizard.Caches;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

//DragonFly
builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

//ASP.NET Core
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    //options.KnownNetworks.Clear();
    //options.KnownProxies.Clear();
});

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
                        x.AddType<ProjectModel>();
                        x.AddType<PageLayoutModel>();
                    })
                    .AddPermissions()
                    ;

builder.Services.AddRazorPages();

builder.Services.AddSingleton<DataSeeding>();
//builder.Services.AddHttpsRedirection(x => x.HttpsPort = 443);

DataCache cache = new DataCache();

builder.Services.AddSingleton(cache);

var app = builder.Build();

//init DragonFly CMS
IDragonFlyApi api = app.Services.GetRequiredService<IDragonFlyApi>();
await api.InitAsync();

//data seeding
//DataSeeding seeding = app.Services.GetRequiredService<DataSeeding>();
//await seeding.StartAsync();

IContentStorage contentStorage = app.Services.GetRequiredService<IContentStorage>();

cache.PageLayouts = (await contentStorage.QueryAsync<PageLayoutModel>()).Items;
cache.FooterPages = (await contentStorage.QueryAsync<StandardPageModel>(x => x.AddBoolQuery(x => x.IsFooterPage, true))).Items;

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
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
