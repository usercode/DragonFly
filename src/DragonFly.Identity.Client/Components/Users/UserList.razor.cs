// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Identity.Client.Components.Users;

public class UserListBase : EntityListComponent<IdentityUser>
{
    public UserListBase()
    {
        Users = new List<IdentityUser>();
    }

    [Inject]
    public IIdentityService UserStore { get; set; }

    public IList<IdentityUser> Users { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        toolbarItems.Add(new ToolbarItem("Create", BSColor.Success, async () => Navigation.NavigateTo("settings/identity/user/create")));
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
