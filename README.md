# DragonFly » Headless ASP.NET Core CMS + Blazor

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

| Package                     | Release | 
|-----------------------------|-----------------|
| DragonFly.Core              | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Core.svg)](https://www.nuget.org/packages/DragonFly.Core/) |
| DragonFly.AspNetCore        | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.AspNetCore.svg)](https://www.nuget.org/packages/DragonFly.AspNetCore/) |
| DragonFly.API               | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.API.svg)](https://www.nuget.org/packages/DragonFly.API/) |
| DragonFly.Client            | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Client.svg)](https://www.nuget.org/packages/DragonFly.Client/) |
| DragonFly.Generator             | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Generator.svg)](https://www.nuget.org/packages/DragonFly.Generator/) |
| DragonFly.ImageWizard       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.ImageWizard.svg)](https://www.nuget.org/packages/DragonFly.ImageWizard/) |
| DragonFly.MongoDB           | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.MongoDB.svg)](https://www.nuget.org/packages/DragonFly.MongoDB/) |
| DragonFly.Identity       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.Identity.svg)](https://www.nuget.org/packages/DragonFly.Identity/) |
| DragonFly.ApiKeys       | [![NuGet](https://img.shields.io/nuget/vpre/DragonFly.ApiKeys.svg)](https://www.nuget.org/packages/DragonFly.ApiKeys/) |

## Features

![Bildschirmfoto am 2024-12-30 um 19 35 07](https://github.com/user-attachments/assets/22c5a91b-fd5c-4c54-ba03-5d76b1e1feac)


* Define schema with custom fields
* Create, update, delete and publish/unpublish content items based on schema
  * Create new Fields with FieldGenerator
* Create typed content items with ModelGenerator
* BlockField for structured page content
  * TextBlock, HtmlBlock, ColumnBlock, GridBlock, AssetBlock, YouTubeBlock, CodeBlock,..
* Asset management
  * Folder tree
  * Asset metadata like ImageMetadata and PdfMetadata
    * Use IAssetProcessing after asset upload
  * Create thumbnails for image and pdf files with ImageWizard
* REST API
* Permissions for content, schema, asset, asset folder, webhooks
  * Dynamic permissions for every schema
* Background tasks
* WebHooks
* MongoDB storage
  * Create proxies with ProxyGenerator
* Admin interface with Blazor

## Getting started
### Prerequisites

* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Visual Studio 2022
* MongoDB instance

### Create a new project from template

To use the project template you need first to download and install it from NuGet. 

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
    "Username": "",
    "Password": ""
  },
~~~

### Use source generators to avoid reflection
- FieldGenerator
- ModelGenerator
- ProxyGenerator

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
- GeolocationField
- SlugField
- XmlField
- HtmlField
- BlockField (HeadingBlock, TextBlock, HtmlBlock, CodeBlock, ContainerBlock, ColumnBlock, AssetBlock, ReferenceBlock, GridBlock,..)

### How to create new content schema and content item
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

### Create typed content

DragonFly.Generator

```csharp
[ContentItem]
public partial class BlogPost
{
    [DateField(Required = true)]
    private DateTime? _date;

    [StringField(Required = true, Searchable = true, ListField = true, MinLength = 8, MaxLength = 512)]
    private string? _title;

    [TextField]
    private string? _description;

    [SlugField(Required = true, Index = true)]
    private string? _slug;

    [AssetField(ListField = true, ShowPreview = true)]
    private AssetField _image;

    [BlockField]
    private BlockField _mainContent;
}
```
#### Register typed content

```csharp
builder.Services.AddDragonFly()
                    .AddModels(x => x.Add<BlogPost>());                    
```

#### Use queries for typed content
```csharp
//get all items
var result = await ContentStorage.QueryAsync<BlogPostModel>(x => x
                                    .Published(true)
                                    .Top(10)
                                    .Slug(x => x.Slug, "my-product")
                                    .Integer(x => x.Quantity, 10, NumberQueryType.Equal)
                                    .String(x => x.Title, "Test", StringQueryType.Equal)
);
```

### BackgroundTask

For long running jobs you can use the BackgroundTaskManager.

```csharp
IBackgroundTaskManager taskManager = app.Services.GetRequiredService<IBackgroundTaskManager>();
taskManager.Start("Test", async ctx => await Task.Delay(TimeSpan.FromSeconds(60), ctx.CancellationToken));
```

### DragonFly.AspNetCore	

```csharp
builder.Services.Configure<DragonFlyOptions>(Configuration.GetSection("General"));
builder.Services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDB"));

//add DragonFly services
builder.Services.AddDragonFly(x => x
                      .AddImageWizard()
                      .AddRestApi()
                      .AddMongoDbStorage()
                      .AddMongoDbIdentity()
                      .AddApiKeys());

var app = builder.Build();

//init DragonFly
await app.InitDragonFlyAsync();

if (env.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseWebAssemblyDebugging();
}

app.UseDragonFly();
app.UseDragonFlyManager();
app.UseRouting();
app.Run();
```

### DragonFly.Client (Blazor)

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFly.App.Client.App>("app");

//Register DragonFly components
builder.AddDragonFlyClient(x => x
                        .AddRestClient()
                        .AddIdentity()
                        .AddApiKeys());

WebAssemblyHost host = builder.Build();

await host.InitDragonFlyAsync();
await host.RunAsync();
```
