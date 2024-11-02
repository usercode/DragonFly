// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DragonFly.Razor.Extensions;
using DragonFly.Client;

namespace DragonFly.Client.Pages.Assets;

public class AssetDetailBase : EntityDetailComponent<Asset>
{
    public AssetDetailBase()
    {

    }

    [Inject]
    public IAssetStorage AssetStore { get; set; }

    [Inject]
    public IAssetFolderStorage AssetFolderStore { get; set; }


    private IBrowserFile SelectedFile { get; set; }

    [Parameter]
    public Guid? FolderId { get; set; }


    [Inject]
    public IAssetStorage AssetService { get; set; }

    [Inject]
    public IContentStorage ContentService { get; set; }

    public async Task PublishAsync()
    {
        await SaveAsync();

        await AssetService.PublishAsync(Entity);
    }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if (IsNewEntity)
        {
            toolbarItems.AddCreateButton(this);
        }
        else
        {
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Success, () => PublishAsync()));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddUpdateButton(this);
            toolbarItems.AddDeleteButton(this);
            toolbarItems.Add(new ToolbarItem("Refresh metadata", BSColor.Danger, () => ApplyMetadata()));
        }            
    }

    protected override async Task RefreshActionAsync()
    {
        await base.RefreshActionAsync();

        if (IsNewEntity)
        {
            Entity = new Asset();

            if (FolderId != null)
            {
                Entity.Folder = await AssetFolderStore.GetAssetFolderAsync(FolderId.Value);
            }
        }
        else
        {
            Entity = await AssetService.GetAssetAsync(EntityId);
        }
    }

    protected override async Task CreateActionAsync()
    {
        await AssetService.CreateAsync(Entity);

        NavigationManager.NavigateToAsset(Entity);
    }

    protected override async Task UpdateActionAsync()
    {
        await AssetService.UpdateAsync(Entity);
    }

    protected override async Task DeleteActionAsync()
    {
        await AssetStore.DeleteAsync(Entity);

        NavigationManager.NavigateToAssets();
    }

    protected async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        SelectedFile = e.File;

        using (Stream stream = SelectedFile.OpenReadStream(long.MaxValue))
        {
            await AssetService.UploadAsync(Entity, SelectedFile.ContentType, stream);
        }

        await RefreshAsync();
    }

    public virtual async Task ApplyMetadata()
    {
        await AssetService.ApplyMetadataAsync(Entity);

        await RefreshAsync();
    }
}
