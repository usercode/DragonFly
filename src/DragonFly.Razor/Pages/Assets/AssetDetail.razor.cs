using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages
{
    public class AssetDetailBase : EntityDetailComponent<Asset>
    {
        public AssetDetailBase()
        {

        }

        [Inject]
        public IAssetStorage AssetStore { get; set; }

        private IBrowserFile SelectedFile { get; set; }

        [Parameter]
        public Guid? FolderId { get; set; }

        public async Task PublishAsync()
        {
            await SaveAsync();

            await ContentService.PublishAsync(Entity.Id);
        }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            if(IsNewEntity)
            {               
                toolbarItems.AddCreateButton(this);
            }
            else
            {
                toolbarItems.Add(new ToolbarItem("Publish", Color.Success, () => PublishAsync()));
                toolbarItems.AddRefreshButton(this);
                toolbarItems.AddUpdateButton(this);
                toolbarItems.AddDeleteButton(this);
            }           
            
        }

        protected override async Task RefreshActionAsync()
        {
            await base.RefreshActionAsync();

            if(IsNewEntity)
            {
                Entity = new Asset();

                if(FolderId != null)
                {
                    Entity.Folder = new AssetFolder(FolderId.Value);
                }
            }
            else
            {
                Entity = await ContentService.GetAssetAsync(EntityId);
            }
        }

        protected override async Task SaveActionAsync()
        {
            if(IsNewEntity)
            {
                await ContentService.CreateAsync(Entity);

                NavigationManager.NavigateTo($"asset/{Entity.Id}");
            }
            else
            {
                await ContentService.UpdateAsync(Entity);
            }

            await RefreshAsync();
        }

        protected override async Task DeleteActionAsync()
        {
            await AssetStore.DeleteAsybc(Entity.Id);

            NavigationManager.NavigateTo($"asset");
        }

        protected async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            SelectedFile = e.File;

            using (Stream stream = SelectedFile.OpenReadStream(long.MaxValue))
            {
                await ContentService.UploadAsync(Entity.Id, SelectedFile.ContentType, stream);
            }

            await RefreshAsync();
        }
    }
}
