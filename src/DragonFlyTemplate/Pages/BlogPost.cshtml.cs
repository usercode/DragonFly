using DragonFly.Query;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;
using Microsoft.AspNetCore.Mvc;
using DragonFly;

namespace DragonFlyTemplate.Pages;

public class BlogPostPage : BasePageModel
{
    public BlogPostPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;
    }

    public IContentStorage ContentStorage { get; }

    public BlogPostModel Result { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var query = new ContentItemQuery() { Top = 1, Skip = 0, Published = true }
                        .AddStringQuery(nameof(BlogPostModel.Slug), slug);

        QueryResult<BlogPostModel> result = await ContentStorage.QueryAsync<BlogPostModel>(query);

        if (result.Items.Count == 0)
        {
            return NotFound();
        }

        Result = result.Items[0];

        return Page();
    }
}
