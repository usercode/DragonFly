// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;
using DragonFly.Proxy;
using DragonFly.Query;
using DragonFly.Storage;
using DragonFlyTemplate.Models;

namespace DragonFlyTemplate.Pages;

public class StartPage : BasePageModel
{
    public StartPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Start";
    }

    public IContentStorage ContentStorage { get; }

    public StandardPageModel Result { get; set; }

    public Document Document { get; private set; }

    public async Task OnGetAsync()
    {
        var result = await ContentStorage.QueryAsync<StandardPageModel>(new ContentItemQuery() { Top = 1 }
                                                                        .AddSlugQuery(nameof(StandardPageModel.Slug), "start"));

        if (result.Count == 0)
        {
            return;
        }

        Result = result.Items[0];

        Document = await Result.MainContent.GetDocumentAsync();
    }
}
