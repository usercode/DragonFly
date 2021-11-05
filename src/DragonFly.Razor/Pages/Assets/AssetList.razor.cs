using DragonFly.Client.Base;
using DragonFly.Client.Pages.ContentItems;
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
using DragonFly.Storage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages
{
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
}
