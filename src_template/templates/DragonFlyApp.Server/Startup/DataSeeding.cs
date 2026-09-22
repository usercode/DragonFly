// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using Microsoft.Extensions.FileProviders;
using DragonFlyTemplate.Models;

namespace DragonFlyTemplate.Startup;

/// <summary>
/// DataSeeding
/// </summary>
public class DataSeeding
{
    public DataSeeding(
        IContentStorage contentStorage,
        IAssetStorage assetStorage,
        ISlugService slugService)
    {
        ContentStorage = contentStorage;
        AssetStorage = assetStorage;
        SlugService = slugService;
    }

    /// <summary>
    /// DataStorage
    /// </summary>
    private IContentStorage ContentStorage { get; }

    /// <summary>
    /// AssetStorage
    /// </summary>
    private IAssetStorage AssetStorage { get; }

    /// <summary>
    /// SlugService
    /// </summary>
    private ISlugService SlugService { get; }

    public async Task StartAsync()
    {
        if (await ContentStorage.FirstOrDefaultAsync<StandardPageModel>() != null)
        {
            return;
        }

        StandardPageModel startPage = new StandardPageModel();
        startPage.IsStartPage = true;
        startPage.Title = "Welcome To DragonFly CMS";
        startPage.Slug = "start";

        Document document = new Document();
        document.Blocks.Add(new HeadingBlock(HeadingType.H1, "Welcome To DragonFly CMS"));
        document.Blocks.Add(new ProgressBlock(ColorType.Danger, 75));
        document.Blocks.Add(new HeadingBlock(HeadingType.H2, "Introduction"));
        document.Blocks.Add(new HtmlBlock("<p><i>DragonFly</i> is an open source CMS based on <b>ASP.NET</b> and Blazor. </p>"));
        document.Blocks.Add(new ColumnBlock(
                                 new Column(new AlertBlock(ColorType.Success, new HtmlBlock("You can find the manager here: <b>/manager</b>"))),
                                 new Column(new AlertBlock(ColorType.Warning, new HtmlBlock("This is a warning."))),
                                 new Column(new AlertBlock(ColorType.Danger, new HtmlBlock("It's dangerous to use another CMS. ;-)")))));
        document.Blocks.Add(new ColumnBlock(
                                new Column(
                                       new CodeBlock(CodeType.CSharp, """
                                                        [ContentItem]
                                                        public partial class StandardPageModel
                                                        {
                                                            [StringField(Required = true, ListField = true)]
                                                            public string _title;
                                    
                                                            [SlugField(Required = true, Index = true)]
                                                            public string _slug;
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public bool _noFollow;
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public bool _noIndex;
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public bool _isStartPage;
                                    
                                                            [BoolField(Required = true, Index = true)]
                                                            public bool _isFooterPage;
                                    
                                                            [BlockField]
                                                            public BlockField _mainContent;
                                                        } 
                                                        """)),
                                       new Column(new OpenGraphBlock("https://github.com/usercode/DragonFly"))));

        document.Blocks.Add(new HeadingBlock(HeadingType.H2, "Great youtube videos about .NET, C# and Blazor"));
        document.Blocks.Add(new ColumnBlock(
                                    new Column(new YouTubeBlock("QVkxusemLoo")),
                                    new Column(new YouTubeBlock("Z8SL0Vv30j8")),
                                    new Column(new YouTubeBlock("v8UWYwAhKZA"))));

        await startPage.MainContent.SetDocumentAsync(document);

        await ContentStorage.CreateAsync(startPage);
        await ContentStorage.PublishAsync(startPage);
    }

    public async Task<Asset> CreateAssetAsync(IFileInfo filename)
    {
        if (filename.Exists == false)
        {
            throw new Exception($"Asset not found: {filename}");
        }

        Asset asset = new Asset();
        asset.Name = filename.Name;
        asset.Slug = SlugService.Transform(asset.Name);

        await AssetStorage.CreateAsync(asset);

        using Stream stream = filename.CreateReadStream();

        await AssetStorage.UploadAsync(asset.Id, MimeTypes.Jpeg, stream);

        return asset;
    }
}
