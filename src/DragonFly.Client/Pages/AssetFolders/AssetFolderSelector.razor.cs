// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class AssetFolderSelectorBase : StartComponentBase
{
    public AssetFolderSelectorBase()
    {
        SelectedFolders = new Stack<AssetFolder>();
    }

    [Inject]
    public IAssetFolderStorage AssetFolderStore { get; set; }

    /// <summary>
    /// Folders
    /// </summary>
    public IEnumerable<AssetFolder> Folders { get; set; } = Enumerable.Empty<AssetFolder>();

    public Stack<AssetFolder> SelectedFolders { get; set; }

    [Parameter]
    public Action<AssetFolder> FolderSelected { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
       // toolbarItems.Add(new ToolbarItem("Create", BlazorStrap.BSColor.Danger, async () => Navigation.NavigateTo($"asset/create/{SelectedFolder?.Id}")));
    }

    protected override async Task RefreshActionAsync()
    {
        Guid? parentId = null;
        if (SelectedFolders.TryPeek(out var r))
        {
            parentId = r.Id;
        }

        Folders = await AssetFolderStore.QueryAsync(new AssetFolderQuery() { Parent = parentId });
    }

    protected async Task LoadSubFolderAsync(AssetFolder folder)
    {
        SelectedFolders.Push(folder);

        await RefreshAsync();
    }
}
