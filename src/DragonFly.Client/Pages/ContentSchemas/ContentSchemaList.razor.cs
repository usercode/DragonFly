// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class ContentSchemaListBase : EntityListComponent<ContentSchema>
{
    public ContentSchemaListBase()
    {

    }


    [Inject]
    public ISchemaStorage ContentService { get; set; }

    protected override string GetNavigationPath(ContentSchema entity)
    {
        return $"schema/{entity.Name}";
    }

    protected override async Task RefreshActionAsync()
    {
        SearchResult = await ContentService.QuerySchemasAsync();
    }
}
