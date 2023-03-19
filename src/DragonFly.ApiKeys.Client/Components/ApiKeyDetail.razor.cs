﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using DragonFly.Client.Base;
using DragonFly.Client.Helpers;
using DragonFly.Permissions;
using DragonFly.Permissions.Client;
using DragonFly.Razor.Base;
using DragonFLy.ApiKeys;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.ApiKeys.Client.Components;

public class ApiKeyDetailBase : EntityDetailComponent<ApiKey>
{
    public ApiKeyDetailBase()
    {
        Permissions = new List<SelectableElementTree<PermissionItem>>();
    }

    [Inject]
    public IApiKeyService ApiKeyService { get; set; }

    [Inject]
    public IPermissionService PermissionService { get; set; }

    public IEnumerable<SelectableElementTree<PermissionItem>> Permissions { get; set; }

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
            Entity = new ApiKey();
        }
        else
        {
            Entity = await ApiKeyService.GetApiKey(EntityId);
        }

        IEnumerable<PermissionItem> permissions = await PermissionService.GetPermissionsAsync();

        Permissions = permissions
                                .ToSelectableStructure(x => Entity.Permissions.Any(p => p == x.Name))
                                .ToList();
    }

    protected override async Task CreateActionAsync()
    {
        await base.CreateActionAsync();

        await ApiKeyService.CreateApiKey(Entity);
    }

    protected override async Task UpdateActionAsync()
    {
        await base.UpdateActionAsync();

        await ApiKeyService.UpdateApiKey(Entity);
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        base.OnSaving(args);

        Entity.Permissions = Permissions
                                    .ToFlatList()
                                    .Select(x => x.Name)
                                    .ToList();
    }
}
