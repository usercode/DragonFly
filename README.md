# DragonFly
Headless CMS based on ASP.NET Core and Blazor

[![Docker](https://img.shields.io/docker/pulls/usercode/dragonfly)](https://hub.docker.com/r/usercode/dragonfly)

| Package                       | Release | 
|-------------------------------|-----------------|
| DragonFly.Core                   | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore             | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.AspNetCore.API         | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.API.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore.API/) |
| DragonFly.AspNetCore.API.Client  | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.API.Client.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore.API.Client/) |
| DragonFly.Razor                  | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Razor.svg)](https://www.nuget.org/packages/DragonFly.Razor/) |
| DragonFly.Storage.MongoDB        | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Storage.MongoDB.svg)](https://www.nuget.org/packages/DragonFly.Storage.MongoDB/) |


## DragonFly.Core

How to create new content schema and content item
```csharp
IContentStorage contentStorage = ...;//use MongoStorage or ClientContentService (http client)

//create brand schema
ContentSchema schemaBrand = new ContentSchema("Brand")
                                    .AddString("Name")
                                    .AddSlug("Slug")
                                    .AddTextArea("Description");

//Define schema for product
ContentSchema schemaProduct = new ContentSchema("Product")
                                    .AddReference("Brand")
                                    .AddString("Name", options => options.IsRequired = true)
                                    .AddSlug("Slug")
                                    .AddBool("IsAvailable", options => options.DefaultValue = true)
                                    .AddFloat("Price")
                                    .AddTextArea("Description", options => options.MaxLength = 255)
                                    .AddArray("Attributes", options => options
                                                                        .AddString("Name")
                                                                        .AddString("Value"));

await contentStorage.CreateAsync(schemaProduct);

//create product by schema
ContentItem contentProduct = schemaProduct
                            .CreateContentItem()
                            .SetReference("Brand", new ContentItem(Guid.Parse(""), schemaBrand))
                            .SetString("Name", "ProductA")
                            .SetBool("IsAvailable", true)
                            .SetFloat("Price", 9.99)
                            .SetTextArea("Description", "...")
                            .AddArrayFieldItem("Attributes", schemaProduct, item => item
                                                                    .SetString("Name", "Size")
                                                                    .SetString("Value", "M"));

await contentStorage.CreateAsync(contentProduct);

```

### DragonFly.AspNetCore	

```csharp
 public void ConfigureServices(IServiceCollection services)
 {
      services.Configure<DragonFlyOptions>(Configuration.GetSection("General"));
      services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDB"));

      //DragonFly
      services.AddDragonFly()
                  .AddRestApi()
                  .AddGraphQLApi()
                  .AddMongoDbStorage();
 }
```

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDragonFlyApi api)
{
    api.Init();

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
```

### DragonFly.Razor (Blazor Client App)

```csharp
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<DragonFly.App.Client.App>("app");

    //Register DragonFly components
    builder.AddDragonFlyClient();

    WebAssemblyHost build = builder.Build();

    build.UseDragonFlyClient();

    await build.RunAsync();
}
```
