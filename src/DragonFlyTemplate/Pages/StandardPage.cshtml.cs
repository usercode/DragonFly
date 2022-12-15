// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Proxy;
using DragonFly.Query;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using Microsoft.AspNetCore.Mvc;

namespace DragonFlyTemplate.Pages;

public class StandardPage : BasePageModel
{
    public StandardPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;
    }

    public IContentStorage ContentStorage { get; }

    public StandardPageModel Result { get; set; }

    public Document Document { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
        {
            Result = await ContentStorage.FirstOrDefaultAsync<StandardPageModel>(x => x.Published(true).AddBoolQuery(nameof(StandardPageModel.IsStartPage), true));
        }
        else
        {
            Result = await ContentStorage.FirstOrDefaultAsync<StandardPageModel>(x => x.Published(true).AddSlugQuery(nameof(StandardPageModel.Slug), slug));
        }        

        if (Result == null)
        {
            return NotFound();
        }

        PageTitle = Result.Title.Value;

        Document = await Result.MainContent.GetDocumentAsync();

        return Page();
    }
}
