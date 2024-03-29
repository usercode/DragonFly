﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Base;
using DragonFly.Query;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class ContentStructureDetailTreeBase : EntityDetailComponent<ContentStructure>
{
    public ContentStructureDetailTreeBase()
    {
        Entity = new ContentStructure();
        Nodes = new List<ContentNode>();
    }


    [Inject]
    public IStructureStorage ContentService { get; set; }

    /// <summary>
    /// Nodes
    /// </summary>
    public IList<ContentNode> Nodes { get; set; }

    [Inject]
    public IStructureStorage Storage { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if(IsNewEntity)
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
        await base.RefreshActionAsync();

        if (IsNewEntity)
        {
            Entity = new ContentStructure();
        }
        else
        {
            Entity = await ContentService.GetStructureAsync(EntityId);

            var nodesResult = await ContentService.QueryAsync(new NodesQuery() { Structure = Entity.Id, ParentId = null });

            Nodes = nodesResult.Items;
        }
    }

    protected override async Task CreateActionAsync()
    {
        await ContentService.CreateAsync(Entity);

        NavigationManager.NavigateTo($"structure/{Entity.Id}");
    }

    protected override async Task UpdateActionAsync()
    {
        await ContentService.UpdateAsync(Entity);
    }

    public async Task AddContentAsync()
    {
        ContentNode contentNode = new ContentNode();
        contentNode.Structure = Entity.Id;

        await Storage.CreateAsync(contentNode);

        Nodes.Add(contentNode);
    }
}
