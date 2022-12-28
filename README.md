# DragonFly
Headless CMS based on ASP.NET Core and Blazor

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Docker](https://img.shields.io/docker/pulls/usercode/dragonfly)](https://hub.docker.com/r/usercode/dragonfly)

| Package                     | Release | 
|-----------------------------|-----------------|
| DragonFly.Core              | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore        | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.API               | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.API.svg)](https://www.nuget.org/packages/DragonFly.API/) |
| DragonFly.Client            | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Client.svg)](https://www.nuget.org/packages/DragonFly.Client/) |
| DragonFly.BlockField        | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.BlockField.svg)](https://www.nuget.org/packages/DragonFly.BlockField/) |
| DragonFly.Proxy             | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Proxy.svg)](https://www.nuget.org/packages/DragonFly.Proxy/) |
| DragonFly.ImageWizard       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.ImageWizard.svg)](https://www.nuget.org/packages/DragonFly.ImageWizard/) |
| DragonFly.MongoDB           | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.MongoDB.svg)](https://www.nuget.org/packages/DragonFly.MongoDB/) |
| DragonFly.Identity       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Identity.svg)](https://www.nuget.org/packages/DragonFly.Identity/) |
| DragonFly.ApiKeys       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.ApiKeys.svg)](https://www.nuget.org/packages/DragonFly.ApiKeys/) |
| DragonFly.Permissions       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Permissions.svg)](https://www.nuget.org/packages/DragonFly.Permissions/) |

## Getting started

### Prerequisites

* [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
* Visual Studio 2022
* MongoDB instance

### Create a new project from templates

To use the project templates you first need to download and install them from NuGet. 

~~~ bash
dotnet new install DragonFly.Templates
~~~

After this you can create the project with:

~~~ bash
dotnet new DragonFly
~~~

If you have a remote MongoDB instance, you need to add some appsettings:

~~~ json
"MongoDB": {
    "Hostname": "localhost",
    "Database": "DragonFly_App",
    "Port": 27017,
    "Username": "admin",
    "Password": "Password123"
  },
~~~

## DragonFly

![grafik](https://user-images.githubusercontent.com/2958488/208325922-9e55b4d0-9e08-4e0a-96c1-855fe1361584.png)

### Supported fields
- StringField
- FloatField
- BoolField
- AssetField
- ReferenceField
- ComponentField
- ArrayField
- DateField
- IntegerField
- ColorField
- GeoLocationField
- SlugField
- XmlField
- HtmlField
- BlockField (HeadingBlock, TextBlock, HtmlBlock, CodeBlock, ContainerBlock, ColumnBlock, AssetBlock, ReferenceBlock,..)

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
                            .CreateContent()
                            .SetReference("Brand", new ContentItem(Guid.Parse(""), schemaBrand))
                            .SetString("Name", "ProductA")
                            .SetBool("IsAvailable", true)
                            .SetFloat("Price", 9.99)
                            .SetTextArea("Description", "...")
                            .AddArrayItem("Attributes", schemaProduct, item => item
                                                                    .SetString("Name", "Size")
                                                                    .SetString("Value", "M"));

await contentStorage.CreateAsync(contentProduct);

```

### DragonFly.AspNetCore	

```csharp
builder.Services.Configure<DragonFlyOptions>(Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDB"));

//add DragonFly services
builder.Services.AddDragonFly()
                  .AddImageWizard()
                  .AddRestApi()
                  .AddMongoDbStorage()
                  .AddMongoDbIdentity()
                  .AddBlockField()
                  .AddApiKeys()
                  .AddPermissions();

var app = builder.Build();

//init DragonFly
await app.InitDragonFly();

if (env.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseWebAssemblyDebugging();
}

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
app.UseRouting();
app.Run();
```

### DragonFly.Client (Blazor)

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFly.App.Client.App>("app");

//Register DragonFly components
builder.AddDragonFly()
          .AddRestApi()
          .AddBlockField()
          .AddIdentity()
          .AddApiKeys();

WebAssemblyHost host = builder.Build();

await host.InitDragonFly();
await host.RunAsync();
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
      - MongoDB__InitialUsername=admin
      - MongoDB__InitialPassword=Password123
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
