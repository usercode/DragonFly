# DragonFly
Headless CMS based on ASP.NET Core and Blazor

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Docker](https://img.shields.io/docker/pulls/usercode/dragonfly)](https://hub.docker.com/r/usercode/dragonfly)

| Package                     | Release | 
|-----------------------------|-----------------|
| DragonFly.Core              | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore        | [![NuGet](https://img.shields.io/nuget/v/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.API               | [![NuGet](https://img.shields.io/nuget/v/DragonFly.API.svg)](https://www.nuget.org/packages/DragonFly.API/) |
| DragonFly.API.Client        | [![NuGet](https://img.shields.io/nuget/v/DragonFly.API.Client.svg)](https://www.nuget.org/packages/DragonFly.API.Client/) |
| DragonFly.Razor             | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Razor.svg)](https://www.nuget.org/packages/DragonFly.Razor/) |
| DragonFly.BlockField        | [![NuGet](https://img.shields.io/nuget/v/DragonFly.BlockField.svg)](https://www.nuget.org/packages/DragonFly.BlockField/) |
| DragonFly.Proxy             | [![NuGet](https://img.shields.io/nuget/v/DragonFly.Proxy.svg)](https://www.nuget.org/packages/DragonFly.Proxy/) |
| DragonFly.ImageWizard       | [![NuGet](https://img.shields.io/nuget/v/DragonFly.ImageWizard.svg)](https://www.nuget.org/packages/DragonFly.ImageWizard/) |
| DragonFly.MongoDB           | [![NuGet](https://img.shields.io/nuget/v/DragonFly.MongoDB.svg)](https://www.nuget.org/packages/DragonFly.MongoDB/) |


## DragonFly.Core

### Schema

![localhost_44383_manager_schema_a87bd85d-b243-46eb-9aa7-7f1f35456ddb(My) (3)](https://user-images.githubusercontent.com/2958488/135279414-34be752d-9443-46c7-9a05-f0383ce73783.png)

### Content

![localhost_44383_manager_schema_a87bd85d-b243-46eb-9aa7-7f1f35456ddb(My) (4)](https://user-images.githubusercontent.com/2958488/135279635-68e3234f-ede7-4611-a71e-4d145b6a8080.png)

### Supported fields
- StringField
- FloatField
- BoolField
- AssetField
- ReferenceField
- EmbedField
- ArrayField
- DateField
- IntegerField
- SlugField
- XmlField
- XHtmlField
- HtmlField

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
                  .AddImageWizard()
                  .AddRestApi()
                  .AddGraphQLApi()
                  .AddMongoDbStorage()
                  .AddMongoDbIdentity()
                  .AddSchemaBuilder()
                  .AddBlockField()
                  .AddApiKeys()
                  .AddPermissions()
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
                        x.MapImageWizard();
                        x.MapApiKey();
                        x.MapIdentity();
                        x.MapRestApi();
                        x.MapPermission();
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
## Docker
```yaml
version: "2"

services:
  app:
    image: usercode/dragonfly
    container_name: catalog
    restart: always
    networks:
      - default
    volumes:
      - catalog_cache:/cache
    environment: 
      - MongoDB__Database=DragonFly_App
      - MongoDB__Hostname=catalog_db      
      - MongoDB__Username=root
      - MongoDB__Password=YOUR_PASSWORD
      - AssetCache__Folder=/cache
      - ImageWizard__Key=YOUR_IMAGEWIZARD_KEY
    depends_on:
      - db
  
  db:
    image: mongo:4.4
    container_name: catalog_db
    restart: always
    networks:
      - default
    environment:
      - MONGO_INITDB_ROOT_USERNAME=root
      - MONGO_INITDB_ROOT_PASSWORD=YOUR_PASSWORD      
    volumes:
      - catalog_db:/data/db
      - catalog_configdb:/data/configdb

volumes:
  catalog_db:
    external: true
  catalog_configdb:
    external: true
  catalog_cache:
    external: true
```
