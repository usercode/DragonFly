# DragonFly
Headless CMS based on ASP.NET Core and Blazor

| Package                       | Release | 
|-------------------------------|-----------------|
| DragonFly.Core                | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore          | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.Razor               | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Razor.svg)](https://www.nuget.org/packages/DragonFly.Razor/) |


## Overview

How to create new content schema and content item
```csharp
//Define schema for entities
ContentSchema schemaProduct = new ContentSchema();
schemaProduct.Name = "Product";
schemaProduct.AddField<ReferenceField>("Brand");
schemaProduct.AddField<StringField>("Name");
schemaProduct.AddField<BoolField>("IsAvailable");
schemaProduct.AddField<FloatField>("Price");
schemaProduct.AddField<TextAreaField>("Description");

ArrayFieldOptions attributeOptions = new ArrayFieldOptions();
attributeOptions.AddField<StringField>("Name");
attributeOptions.AddField<StringField>("Value");

schemProduct.AddField<ArrayField>("Attributes", options: attributeOptions);

ISchemaStorage schemaStorage = //use MongoStorage or ClientContentService (http client)

await schemaStorage.CreateAsync(schemaProduct);

//create Product entity by schema
ContentItem contentProduct = schemaProduct.CreateItem();
contentProduct.GetField<ReferenceField>("Brand").ContentItem = new ContentItem(Guid.Parse(""), schemaBrand);
contentProduct.GetField<StringField>("Name").Value = "ProductA";
contentProduct.GetField<BoolField>("IsAvailable").Value = true;
contentProduct.GetField<FloatField>("Price").Value = 9.99;
contentProduct.GetField<TextAreaField>("Description").Value = "...";

IContentStorage contentStorage = //...

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
