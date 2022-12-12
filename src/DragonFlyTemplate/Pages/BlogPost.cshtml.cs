// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using DragonFly;
using DragonFly.Proxy;
using DragonFly.BlockField;

namespace DragonFlyTemplate.Pages;

public class BlogPostPage : BasePageModel
{
    public BlogPostPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;
    }

    public IContentStorage ContentStorage { get; }

    public BlogPostModel Result { get; private set; }

    public Document Document { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        QueryResult<BlogPostModel> result = await ContentStorage.QueryAsync<BlogPostModel>(new ContentItemQuery() { Top = 1, Skip = 0, Published = true }
                                                                                            .AddSlugQuery(nameof(BlogPostModel.Slug), slug));

        if (result.Items.Count == 0)
        {
            return NotFound();
        }

        Result = result.Items[0];

        Document = await Result.MainContent.GetDocumentAsync();

        PageTitle = Result.Title;

        return Page();
    }
}
