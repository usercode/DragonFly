﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class AssetListBase : EntityListComponent<Asset>
{
    public AssetListBase()
    {
    }

    [Inject]
    public IAssetFolderStorage AssetFolderStore { get; set; }


    [Inject]
    public IAssetStorage AssetService { get; set; }

    public AssetFolder SelectedFolder { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.Add(new ToolbarItem("Create", true, new Icons.Regular.Size16.New(), async () => Navigation.NavigateTo($"asset/create/{SelectedFolder?.Id}")));
        toolbarItems.Add(new ToolbarItem("Apply metadata", false, new Icons.Regular.Size16.ImageEdit(), RefreshAllMetadataAsync));
    }

    protected virtual AssetQuery CreateQuery()
    {
        return new AssetQuery() { Pattern = SearchPattern, Folder = SelectedFolder?.Id };
    }

    protected override async Task RefreshActionAsync()
    {
        AssetFolderQuery query = new AssetFolderQuery();

        if (SelectedFolder == null)
        {
            query.RootOnly = true;
        }
        else
        {
            query.Parent = SelectedFolder.Id;
        }

        SearchResult = await AssetService.QueryAsync(CreateQuery());
    }

    protected async Task OpenFolder(AssetFolder folder)
    {
        SelectedFolder = folder;

        await RefreshAsync();
    }

    private async Task RefreshAllMetadataAsync()
    {
        await AssetService.ApplyMetadataAsync(CreateQuery());
    }

    protected override string GetNavigationPath(Asset entity)
    {
        return $"asset/{entity.Id}";
    }
}
