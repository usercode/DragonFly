// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Identity.Client.Components.Roles;

public partial class RoleListBase : EntityListComponent<IdentityRole>
{
    public RoleListBase()
    {
        Roles = new List<IdentityRole>();
    }

    [Inject]
    public IIdentityService UserStore { get; set; }

    public IList<IdentityRole> Roles { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        toolbarItems.Add(new ToolbarItem("Create", true, new Icons.Regular.Size16.New(), async () => Navigation.NavigateTo("settings/identity/role/create")));
        toolbarItems.AddRefreshButton(this);
    }

    protected override string GetNavigationPath(IdentityRole entity)
    {
        return $"settings/identity/role/{entity.Id}";
    }

    protected override async Task RefreshActionAsync()
    {
        Roles = (await UserStore.GetRolesAsync()).ToList();
    }

}
