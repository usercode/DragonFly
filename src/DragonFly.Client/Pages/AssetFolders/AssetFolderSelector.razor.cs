// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client.Pages.AssetFolders;

public partial class AssetFolderSelector : IDialogContentComponent
{
    [Parameter]
    public AssetFolder RootFolder { get; set; }

    [Parameter]
    public Action<AssetFolder> FolderClicked { get; set; }

    public IList<AssetFolderNode> Folders { get; set; } = new List<AssetFolderNode>();

    public string NewFolderName { get; set; }

    public AssetFolderNode FolderToDelete { get; set; }

    [Parameter]
    public AssetFolderSelector Content { get; set; } = default!;

    [CascadingParameter]
    public FluentDialog? Dialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await RefreshAsync();
    }

    protected async Task RefreshAsync()
    {
        Folders = (await Storage.QueryAsync(new AssetFolderQuery() { Parent = RootFolder?.Id }))
                            .Value
                            .Items
                            .Select(x => new AssetFolderNode(x))
                            .ToList();
    }

    protected async Task DeleteAsync(AssetFolderNode node)
    {
        //await DeletionModal.ShowAsync();

        FolderToDelete = node;
    }

    protected async Task DeleteActionAsync()
    {
        //await DeletionModal.HideAsync();

        await Storage.DeleteAsync(FolderToDelete.Folder);

        Folders.Remove(FolderToDelete);
    }

    protected async Task CreateNewFolderAsync()
    {
        if (string.IsNullOrEmpty(NewFolderName))
        {
            return;
        }

        AssetFolder folder = new AssetFolder();
        folder.Name = NewFolderName;
        folder.Parent = RootFolder;

        await Storage.CreateAsync(folder);

        NewFolderName = string.Empty;

        await RefreshAsync();
    }
}
