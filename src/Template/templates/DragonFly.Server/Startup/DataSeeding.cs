// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Storage;
using Microsoft.Extensions.FileProviders;
using DragonFly.Proxy;
using DragonFlyTemplate.Models;
using DragonFly.BlockField;
using DragonFly.Query;

namespace DragonFlyTemplate.Startup;

/// <summary>
/// DataSeeding
/// </summary>
public class DataSeeding
{
    public DataSeeding(
        IDataStorage dataStorage,
        IDragonFlyApi api)
    {
        DataStorage = dataStorage;
        Api = api;
    }

    /// <summary>
    /// DataStorage
    /// </summary>
    private IDataStorage DataStorage { get; }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    public async Task StartAsync()
    {
        StandardPageModel r = await DataStorage.FirstOrDefaultAsync<StandardPageModel>();

        if (r != null)
        {
            return;
        }

        StandardPageModel startPage = Api.Proxy().Create<StandardPageModel>();
        startPage.IsStartPage = true;
        startPage.Title = "Welcome To DragonFly CMS";
        startPage.Slug = "start";

        Document document = new Document();
        document.Blocks.Add(new HeadingBlock(HeadingType.H1, "Welcome To DragonFly CMS"));
        document.Blocks.Add(new ProgressBlock(ColorType.Danger, 75));
        document.Blocks.Add(new HeadingBlock(HeadingType.H2, "Introduction"));
        document.Blocks.Add(new HtmlBlock("<p><i>DragonFLy</i> is an open source CMS based on <b>ASP.NET</b> and Blazor. </p>"));
        document.Blocks.Add(new ColumnBlock(
                                 new Column(new AlertBlock(ColorType.Success, new HtmlBlock("You can find the manager here: <b>/manager</b>"))),
                                 new Column(new AlertBlock(ColorType.Warning, new HtmlBlock("This is a warning."))),
                                 new Column(new AlertBlock(ColorType.Danger, new HtmlBlock("It's dangerous to use another CMS. ;-)")))));
        document.Blocks.Add(new ColumnBlock(
                                new Column(
                                       new CodeBlock(CodeType.CSharp, """
                                                        [ContentItem("StandardPage")]
                                                        public class StandardPageModel : EntityPageModel
                                                        {
                                                            [StringField(Required = true, ListField = true)]
                                                            public virtual string Title { get; set; }
                                    
                                                            [SlugField(Required = true, Index = true)]
                                                            public virtual string Slug { get; set; }
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public virtual bool NoFollow { get; set; }
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public virtual bool NoIndex { get; set; }
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public virtual bool IsStartPage { get; set; }
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public virtual bool IsFooterPage { get; set; }
                                    
                                                            [BlockField]
                                                            public virtual BlockField MainContent { get; set; }
                                                        } 
                                                        """)),
                                        new Column(new OpenGraphBlock("https://github.com/usercode/DragonFly"))));

        document.Blocks.Add(new HeadingBlock(HeadingType.H2, "Great youtube videos about .NET, C# and Blazor"));
        document.Blocks.Add(new ColumnBlock(
                                    new Column(new YouTubeBlock("QVkxusemLoo")),
                                    new Column(new YouTubeBlock("Z8SL0Vv30j8")),
                                    new Column(new YouTubeBlock("v8UWYwAhKZA"))));

        await startPage.MainContent.SetDocumentAsync(document);

        await DataStorage.CreateAsync(startPage);
        await DataStorage.PublishAsync(startPage);
    }

    public async Task<Asset> CreateAssetAsync(IFileInfo filename)
    {
        if (filename.Exists == false)
        {
            throw new Exception();
        }

        Asset asset = new Asset();
        asset.Name = filename.Name;
        asset.Slug = Slugify.ToSlug(asset.Name);

        await DataStorage.CreateAsync(asset);

        using Stream stream = filename.CreateReadStream();

        await DataStorage.UploadAsync(asset.Id, MimeTypes.Jpeg, stream);

        return asset;
    }
}
