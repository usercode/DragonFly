// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using DragonFly.Razor.Extensions;
using DragonFly.Core.Modules.Assets.Actions;

namespace DragonFly.Client.Pages.Assets;

public partial class AssetDetail
{
    [Inject]
    public IAssetStorage AssetStore { get; set; } = default!;

    [Inject]
    public IAssetFolderStorage AssetFolderStore { get; set; } = default!;

    private IBrowserFile SelectedFile { get; set; }

    [Parameter]
    public Guid? FolderId { get; set; }

    [Inject]
    public IAssetStorage AssetService { get; set; } = default!;

    [Inject]
    public IContentStorage ContentService { get; set; } = default!;

    public IList<ActionItem> Actions { get; set; } = [];

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
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Primary, () => PublishAsync()));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddUpdateButton(this);
            toolbarItems.AddDeleteButton(this);
            toolbarItems.Add(new ToolbarItem("Refresh metadata", BSColor.Primary, () => ApplyMetadata()));

            foreach (ActionItem action in Actions)
            {
                toolbarItems.Add(new ToolbarItem(action.Name, BSColor.Primary, async () =>
                {
                    await AssetStore.ApplyActionAsync(Entity.Id, action.Name);

                    await RefreshAsync();
                }));
            }
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

            var result = await AssetStore.GetActionsAsync(Entity.Id);

            Actions = result.Value;
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
            await AssetService.UploadAsync(Entity.Id, SelectedFile.ContentType, stream);
        }

        await RefreshAsync();
    }

    public virtual async Task ApplyMetadata()
    {
        await AssetService.ApplyMetadataAsync(Entity);

        await RefreshAsync();
    }
}
