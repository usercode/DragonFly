using DragonFly;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Storage;
using DragonFlyTemplate.Models;

namespace DragonFlyTemplate.Pages;

public class ProjectsPage : BasePageModel
{
    public ProjectsPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;
    }

    public IContentStorage ContentStorage { get; }

    public QueryResult<ContentItem> Result { get; private set; }

    public async Task OnGetAsync()
    {
        var e = PermissionState.IsEnabled;

        Result = await ContentStorage.QueryAsync(new ContentItemQuery());


    }
}
