// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
        ContentItemQuery query = new ContentItemQuery() { Top = 1 };

        if (string.IsNullOrEmpty(slug))
        {
            query.AddBoolQuery(nameof(StandardPageModel.IsStartPage), true);
        }
        else
        {
            query.AddSlugQuery(nameof(StandardPageModel.Slug), slug);
        }

        var result = await ContentStorage.QueryAsync<StandardPageModel>(query);

        if (result.Count == 0)
        {
            return NotFound();
        }

        Result = result.Items[0];

        PageTitle = Result.Title.Value;

        Document = await Result.MainContent.GetDocumentAsync();

        return Page();
    }
}
