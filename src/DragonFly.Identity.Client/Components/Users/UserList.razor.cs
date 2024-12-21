// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using static Microsoft.FluentUI.AspNetCore.Components.Icons.Regular.Size16;

namespace DragonFly.Identity.Client.Components.Users;

public class UserListBase : EntityListComponent<IdentityUser>
{
    [Inject]
    public IIdentityService UserStore { get; set; }

    public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        toolbarItems.Add(new ToolbarItem("Create", true, new ArrowClockwise(), async () => Navigation.NavigateTo("settings/identity/user/create")));
        toolbarItems.AddRefreshButton(this);
    }

    protected override string GetNavigationPath(IdentityUser entity)
    {
        return $"settings/identity/user/{entity.Id}";
    }

    protected override async Task RefreshActionAsync()
    {
        Users = (await UserStore.GetUsersAsync()).ToList();
    }
}
