using DragonFly;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;

namespace DragonFlyTemplate.Pages;

public class BlogPage : BasePageModel
{
    public BlogPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Blog";
    }

    public IContentStorage ContentStorage { get; }

    public QueryResult<BlogEntryModel> Result { get; private set; }

    public async Task OnGetAsync()
    {
        Result = await ContentStorage.QueryAsync<BlogEntryModel>(new ContentItemQuery() {  Top = 100, Skip = 0});


    }
}
