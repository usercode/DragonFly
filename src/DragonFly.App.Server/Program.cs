// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Threading.Tasks;
using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.MongoDB;
using DragonFlyABC;
using DragonFlyTEST;
using ImageWizard;
using ImageWizard.Caches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();

//DragonFly options
builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

//ImageWizard options
builder.Services.Configure<ImageWizardOptions>(builder.Configuration.GetSection("ImageWizard"));
builder.Services.Configure<FileCacheOptions>(builder.Configuration.GetSection("AssetCache"));

//ASP.NET
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

//DragonFly services
builder.Services.AddDragonFly()
                    .AddImageWizard()
                    .AddRestApi()
                    .AddMongoDbStorage()
                    .AddMongoDbIdentity()
                    .AddBlockField()
                    .AddApiKeys()
                    .AddContentModel<Product>()
                    .AddContentModel<Customer>();

var app = builder.Build();

//init DragonFly
await app.InitDragonFlyAsync();

IContentStorage contentStorage = app.Services.GetRequiredService<IContentStorage>();

Product p = new Product();
p.Title = "chair";
p.IsActive = true;
p.Slug.Value = "product-a";

ContentItem ci = p.GetContentItem();

ci.ToModel<Product>();
ci.ToProduct();
ci.ToModel();

await contentStorage.CreateAsync(p);

var products = await contentStorage.QueryAsync<Product>(x => x
                                                                .Published(false)
                                                                .Asset(x => x.IsActive, null)
                                                                .Slug(x => x.Slug, "product-a")
                                                                .String(x => x.Title, "123", StringQueryType.Contains));

var customers = await contentStorage.QueryAsync<Customer>(x => x
                                                                .Published(false)
                                                                .Top(100)
                                                                .String(x => x.Lastname, "aaa", StringQueryType.StartsWith));

////update permissions to all roles
//IIdentityService identityService = app.Services.GetRequiredService<IIdentityService>();
//var roles = await identityService.GetRolesAsync();

//foreach (var role in roles)
//{
//    role.Permissions = PermissionManager.Default.GetAll().Select(x => x.Name).ToList();

//    await identityService.UpdateRoleAsync(role);
//}

//demo tasks
IBackgroundTaskManager taskManager = app.Services.GetRequiredService<IBackgroundTaskManager>();
taskManager.Start("Test", static async ctx => await Task.Delay(TimeSpan.FromSeconds(60), ctx.CancellationToken));

for (int i = 0; i < 10; i++)
{
    taskManager.Start($"Import {i}", static async ctx =>
    {
        Random random = new Random();
        int counter = 0;

        while (ctx.CancellationToken.IsCancellationRequested == false)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            await ctx.UpdateStatusAsync($"test {counter}", counter, ctx.Task.ProgressMaxValue);

            counter += random.Next(1, 8);

            if (ctx.Task.ProgressValue >= ctx.Task.ProgressMaxValue)
            {
                break;
            }
        }
    });
}

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseDragonFly(x => x
                        .MapImageWizard()
                        .MapApiKey()
                        .MapIdentity()
                        .MapRestApi());
app.UseDragonFlyManager();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.Run();
