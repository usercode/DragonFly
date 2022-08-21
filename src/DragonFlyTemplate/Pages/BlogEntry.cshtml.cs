using DragonFly;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;
using Microsoft.AspNetCore.Mvc;

namespace DragonFlyTemplate.Pages;

public class BlogEntryPage : BasePageModel
{
    public BlogEntryPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;
    }

    public IContentStorage ContentStorage { get; }

    public BlogEntryModel Result { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var query = new ContentItemQuery() { Top = 1, Skip = 0 }
                        .AddStringQuery(nameof(BlogEntryModel.Slug), slug);

        var result = await ContentStorage.QueryAsync<BlogEntryModel>(query);

        if (result.Items.Count == 0)
        {
            return NotFound();
        }

        Result = result.Items[0];

        return Page();
    }
}
