// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
using DragonFly.Razor.Base;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Identity.Client.Components.Roles;

public class RoleDetailBase : EntityDetailComponent<IdentityRole>
{
    public RoleDetailBase()
    {
        Permissions = new List<SelectableElement<Permission>>();
    }

    [Inject]
    public IIdentityService UserStore { get; set; }

    [Inject]
    public HttpClient Client { get; set; }

    [Inject]
    public IPermissionService PermissionService { get; set; }

    public IEnumerable<SelectableElement<Permission>> Permissions { get; set; }

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
        if (IsNewEntity)
        {
            Entity = new IdentityRole();
        }
        else
        {
            Entity = await UserStore.GetRoleAsync(EntityId);
        }

        IEnumerable<Permission> permissions = await PermissionService.GetPermissionsAsync();

        Permissions = permissions
                                .ToSelectableElement(x => Entity.Permissions.Any(p => p == x.Name))
                                .ToList();
    }

    protected override async Task CreateActionAsync()
    {
        await base.CreateActionAsync();

        await UserStore.CreateRoleAsync(Entity);
    }

    protected override async Task UpdateActionAsync()
    {
        await base.UpdateActionAsync();

        await UserStore.UpdateRoleAsync(Entity);
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        base.OnSaving(args);

        Entity.Permissions = Permissions
                                   .Where(x=> x.IsSelected)
                                   .Select(x => x.Element.Name)
                                   .ToList();
    }
}
