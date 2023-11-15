// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Threading.Tasks;
using DragonFly;
using DragonFly.API;
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
builder.Services.AddOptions<DragonFlyOptions>().BindConfiguration("General");
builder.Services.AddOptions<MongoDbOptions>().BindConfiguration("MongoDB");

//ImageWizard options
builder.Services.AddOptions<ImageWizardOptions>().BindConfiguration("ImageWizard");
builder.Services.AddOptions<FileCacheOptions>().BindConfiguration("AssetCache");

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
                    .AddModels(x => x
                                    .Add<Product2>()
                                    .Add<Customer>()
                                    )
                    ;

var app = builder.Build();

//init DragonFly
await app.InitDragonFlyAsync();

IContentStorage contentStorage = app.Services.GetRequiredService<IContentStorage>();

Product2 p = new Product2();
p.Title = "chair";
p.IsActive = true;
p.Slug.Value = "product-a";
//p.CustomerA = new ReferenceField();
//p.CustomerC = new Customer();
//p.CustomerB = Customer.Schema.CreateContent();
p.Location.Latitude = 10;
p.Location.Longitude = -9;
p.Quantity = 10;
//p.Quantity2 = 1.1;

ContentItem ci = p.GetContentItem();

ci.ToModel<Product2>();
ci.ToModel();

await contentStorage.CreateAsync(p);

var products = await contentStorage.QueryAsync<Product2>(x => x
                                                                .Published(false)
                                                                .Asset(x => x.Image, null)
                                                                .Slug(x => x.Slug, "product-a")
                                                                .Slug(Product2.Fields.Slug, "product-a")
                                                                .Integer(x => x.Quantity, 10, NumberQueryType.Equal)
                                                                .Float(x => x.Quantity2, 1.1)
                                                                .String(x => x.Title, "123", StringQueryType.Equal));

var customers = await contentStorage.QueryAsync<Customer>(x => x
                                                                .Published(false)
                                                                .Take(100)
                                                                .String(x => x.Lastname, "aaa", StringQueryType.StartWith));

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
            await Task.Delay(TimeSpan.FromSeconds(1), ctx.CancellationToken);

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
app.Run();
