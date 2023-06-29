// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.ApiKeys;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.ApiKeys.Client.Components;

public class ApiKeyListBase : EntityListComponent<ApiKey>
{
    public ApiKeyListBase()
    {
        Items = new List<ApiKey>();
    }

    [Inject]
    public IApiKeyService ApiKeyService { get; set; }

    public IEnumerable<ApiKey> Items { get; set; }    

    protected override string GetNavigationPath(ApiKey entity)
    {
        return $"settings/apikey/{entity.Id}";
    }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        toolbarItems.Add(new ToolbarItem("Create", BSColor.Success, async () => Navigation.NavigateTo("settings/apikey/create")));
        toolbarItems.AddRefreshButton(this);
    }

    protected override async Task RefreshActionAsync()
    {
        Items = await ApiKeyService.GetAllApiKeys();
    }
}
