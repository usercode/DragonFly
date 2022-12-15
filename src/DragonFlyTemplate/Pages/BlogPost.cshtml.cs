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
        Result = await ContentStorage.FirstOrDefaultAsync<BlogPostModel>(x => x.Published(true).AddSlugQuery(nameof(BlogPostModel.Slug), slug));

        if (Result == null)
        {
            return NotFound();
        }

        Document = await Result.MainContent.GetDocumentAsync();

        PageTitle = Result.Title;

        return Page();
    }
}
