// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFlyTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using DragonFly;
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
        Result = await ContentStorage.FirstOrDefaultAsync<BlogPostModel>(x => x.Slug(x => x.Slug, slug));

        if (Result == null)
        {
            return NotFound();
        }

        Document = await Result.MainContent.GetDocumentAsync();

        PageTitle = Result.Title;

        return Page();
    }
}
