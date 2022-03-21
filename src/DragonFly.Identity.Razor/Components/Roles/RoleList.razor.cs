using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components.Roles;

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

        toolbarItems.Add(new ToolbarItem("Create", BSColor.Success, async () => Navigation.NavigateTo("settings/identity/role/create")));
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
