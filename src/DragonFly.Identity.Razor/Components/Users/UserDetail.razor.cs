using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Razor.Base;
using DragonFly.Razor.Helpers;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components.Users;

public class UserDetailBase : EntityDetailComponent<IdentityUser>
{
    public UserDetailBase()
    {
        Roles = new List<SelectableElement<IdentityRole>>();
    }

    [Inject]
    public IIdentityService UserStore { get; set; }

    public IEnumerable<SelectableElement<IdentityRole>> Roles { get; set; }

    public string NewPassword { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if (IsNewEntity)
        {
            toolbarItems.AddCreateButton(this);
        }
        else
        {
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddUpdateButton(this);
            toolbarItems.AddDeleteButton(this);
        }
    }

    protected override async Task RefreshActionAsync()
    {
        if (IsNewEntity == false)
        {
            Entity = await UserStore.GetUserAsync(EntityId);
        }
        else
        {
            Entity = new IdentityUser();
        }

        IEnumerable<IdentityRole> roles = await UserStore.GetRolesAsync();

        Roles = roles.Select(x => new SelectableElement<IdentityRole>(Entity.Roles.Any(r => r.Id == x.Id), x)).ToList();
    }

    protected override async Task CreateActionAsync()
    {
        await base.CreateActionAsync();

        await UserStore.CreateUserAsync(Entity, NewPassword);
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        base.OnSaving(args);

        Entity.Roles = Roles.Where(x => x.IsSelected).Select(x => x.Element).ToList();
    }

    protected override async Task UpdateActionAsync()
    {
        await UserStore.UpdateUserAsync(Entity);

        if (string.IsNullOrEmpty(NewPassword) == false)
        {
            await UserStore.ChangePasswordAsync(Entity.Id, NewPassword);
        }
    }
}
