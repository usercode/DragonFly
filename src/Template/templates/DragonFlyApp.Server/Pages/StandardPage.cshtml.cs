// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.BlockField;
using DragonFly.Query;
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

    public Document MainContent { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
        {
            Result = await ContentStorage.FirstOrDefaultAsync<StandardPageModel>(x => x.AddBoolQuery(x => x.IsStartPage, true));
        }
        else
        {
            Result = await ContentStorage.FirstOrDefaultAsync<StandardPageModel>(x => x.AddSlugQuery(x => x.Slug, slug));
        }        

        if (Result == null)
        {
            return NotFound();
        }

        PageTitle = Result.Title;

        MainContent = await Result.MainContent.GetDocumentAsync();

        return Page();
    }
}
