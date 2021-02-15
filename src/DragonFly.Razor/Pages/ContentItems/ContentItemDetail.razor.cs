using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Client.Core.Contents.ContentItems;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Parts.Base;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Data.Content;
using DragonFly.Models;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems
{
    public class ContentItemDetailBase : EntityDetailComponent<ContentItem>
    {
        public ContentItemDetailBase()
        {
        }

        [Inject]
        public IEnumerable<IContentItemAction> ContentItemActions { get; set; }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            toolbarItems.Add(new ToolbarItem("Publish", Color.Success, () => PublishAsync()));
            toolbarItems.Add(new ToolbarItem("Unpublish", Color.Dark, () => UnpublishAsync()));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddSaveButton(this);
            toolbarItems.AddDeleteButton(this);

            if (ContentItemActions != null)
            {
                ContentItemActions.Foreach(x => toolbarItems.Add(new ToolbarItem(x.Name, Color.Dark, () => x.Execute(this))));
            }
        }

        protected override async Task RefreshActionAsync()
        {
            await base.RefreshActionAsync();

            Schema = await ContentService.GetContentSchemaAsync(EntityType);

            if (IsNewEntity)
            {
                Entity = Schema.CreateItem();
            }
            else
            {
                Entity = await ContentService.GetContentItemAsync(EntityType, EntityId);
            }
        }

        protected override async Task SaveActionAsync()
        {
            if (IsNewEntity)
            {
                await ContentService.CreateAsync(Entity);

                NavigationManager.NavigateTo($"content/{EntityType}/{Entity.Id}");
            }
            else
            {
                await ContentService.UpdateAsync(Entity);
            }
        }

        public async Task PublishAsync()
        {
            await SaveAsync();

            await ContentService.PublishAsync(Entity.Schema.Name, Entity.Id);
        }

        public async Task UnpublishAsync()
        {
            await ContentService.UnpublishAsync(Entity.Schema.Name, Entity.Id);
        }       
    }
}
