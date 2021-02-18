# DragonFly
Headless CMS based on ASP.NET Core and Blazor

| Package                       | Release | 
|-------------------------------|-----------------|
| DragonFly.Core                   | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore             | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.AspNetCore.API         | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.API.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore.API/) |
| DragonFly.AspNetCore.API.Client  | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.API.Client.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore.API.Client/) |
| DragonFly.AspNetCore.ImageSharp  | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.ImageSharp.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore.ImageSharp/) |
| DragonFly.Razor                  | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Razor.svg)](https://www.nuget.org/packages/DragonFly.Razor/) |
| DragonFly.Storage.MongoDB        | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Storage.MongoDB.svg)](https://www.nuget.org/packages/DragonFly.Storage.MongoDB/) |


## DragonFly.Core

How to create new content schema and content item
```csharp
IContentStorage contentStorage = //use MongoStorage or ClientContentService (http client)

//define schema for brand
ContentSchema schemaBrand = new ContentSchema();
schemaBrand.Name = "Brand";
schemaBrand.AddString("Name");
schemaBrand.AddSlug("Slug");
schemaBrand.AddTextArea("Description");

await contentStorage.CreateAsync(schemaBrand);

//define schema for product
ContentSchema schemaProduct = new ContentSchema();
schemaProduct.Name = "Product";
schemaProduct.AddReference("Brand");
schemaProduct.AddString("Name", options => options.IsRequired = true);
schemaProduct.AddSlug("Slug");
schemaProduct.AddBool("IsAvailable", optios => optios.DefaultValue = true);
schemaProduct.AddFloat("Price");
schemaProduct.AddTextArea("Description", options => options.MaxLength = 255);
schemaProduct.AddArray("Attributes", options => options
                                                    .AddString("Name")
                                                    .AddString("Value"));

await contentStorage.CreateAsync(schemaProduct);

//create product by schema
ContentItem contentProduct = schemaProduct.CreateItem();
contentProduct.SetReference("Brand", new ContentItem(Guid.Parse(""), schemaBrand));
contentProduct.SetString("Name", "ProductA");
contentProduct.SetBool("IsAvailable", true);
contentProduct.SetFloat("Price", 9.99);
contentProduct.SetTextArea("Description", "...");

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
                  .AddImageSharpProcessing()
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
