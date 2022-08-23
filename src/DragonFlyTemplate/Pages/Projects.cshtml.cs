using DragonFly;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;

namespace DragonFlyTemplate.Pages;

public class ProjectsPage : BasePageModel
{
    public ProjectsPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Projekte";
    }

    public IContentStorage ContentStorage { get; }

    public QueryResult<ProjectModel> Result { get; private set; }

    public async Task OnGetAsync()
    {
        Result = await ContentStorage.QueryAsync<ProjectModel>(new ContentItemQuery() {  Top = 100, Skip = 0});


    }
}
