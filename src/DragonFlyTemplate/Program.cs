using DragonFly;
using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.Identity.AspNetCore.MongoDB;
using DragonFly.ImageWizard;
using DragonFly.MongoDB.Options;
using DragonFlyTemplate.Models;
using ImageWizard;
using ImageWizard.Caches;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DragonFlyOptions>(builder.Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

//ImageWizard
builder.Services.Configure<ImageWizardOptions>(builder.Configuration.GetSection("ImageWizard"));
builder.Services.Configure<FileCacheOptions>(builder.Configuration.GetSection("AssetCache"));

//DragonFly
builder.Services.AddDragonFly()
                    .AddImageWizard()
                    .AddRestApi()
                    .AddMongoDbStorage()
                    .AddMongoDbIdentity()
                    .AddBlockField()
                    .AddSchemaBuilder(options => 
                    {
                        options.AddType<StandardPageModel>();
                        options.AddType<BlogEntryModel>();    
                    })
                    .AddPermissions()
                    ;

builder.Services.AddRazorPages();

var app = builder.Build();

//Init DragonFly CMS
IDragonFlyApi api = app.Services.GetRequiredService<IDragonFlyApi>();
await api.InitAsync();

IHostEnvironment env = app.Services.GetRequiredService<IHostEnvironment>();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDragonFly(x =>
{
    x.MapImageWizard(requireAuthentication: false);
    x.MapIdentity();
    x.MapRestApi();
    x.MapPermission();
});
app.UseDragonFlyManager();
app.MapRazorPages();

app.Run();
