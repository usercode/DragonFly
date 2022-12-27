// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Client.Base;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class AssetListBase : EntityListComponent<Asset>
{
    public AssetListBase()
    {
        Folders = new List<AssetFolder>();
    }

    [Inject]
    public IAssetFolderStorage AssetFolderStore { get; set; }

    /// <summary>
    /// Folders
    /// </summary>
    public IEnumerable<AssetFolder> Folders { get; set; }

    public AssetFolder SelectedFolder { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.Add(new ToolbarItem("Create", BlazorStrap.BSColor.Danger, async () => Navigation.NavigateTo($"asset/create/{SelectedFolder?.Id}")));
    }

    protected override async Task RefreshActionAsync()
    {
        AssetFolderQuery query = new AssetFolderQuery();

        if(SelectedFolder == null)
        {
            query.RootOnly = true;
        }
        else
        {
            query.Parent = SelectedFolder.Id;
        }

        Folders = await AssetFolderStore.GetAssetFoldersAsync(query);

        SearchResult = await ContentService.GetAssetsAsync(new AssetQuery() { Pattern = SearchPattern, Folder = SelectedFolder?.Id });
    }

    protected async Task OpenFolder(AssetFolder folder)
    {
        SelectedFolder = folder;

        await RefreshAsync();
    }

    protected override string GetNavigationPath(Asset entity)
    {
        return $"asset/{entity.Id}";
    }
}
