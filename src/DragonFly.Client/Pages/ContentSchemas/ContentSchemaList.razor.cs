// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentSchemas;

public partial class ContentSchemaList
{
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
