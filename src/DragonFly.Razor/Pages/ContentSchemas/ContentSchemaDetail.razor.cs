using DragonFly.Client.Base;
using DragonFly.Content.ContentParts;
using DragonFly.ContentTypes;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages
{
    public class ContentSchemaDetailBase : EntityDetailComponent<ContentSchema>
    {
        public ContentSchemaDetailBase()
        {
            Entity = new ContentSchema();
        }

        public override bool IsNewEntity => EntityType == null;



        //[Parameter]
        //public bool ShowAddFieldModal { get; set; }


        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddSaveButton(this);
            toolbarItems.AddDeleteButton(this);
        }

        protected override async Task RefreshActionAsync()
        {
            await base.RefreshActionAsync();

            if (IsNewEntity)
            {
                Entity = new ContentSchema();
            }
            else
            {
                Entity = await ContentService.GetContentSchemaAsync(EntityType);
            }
        }

        protected override async Task SaveActionAsync()
        {
            if(IsNewEntity)
            {
                await ContentService.CreateSchemaAsync(Entity);
            }
            else
            {
                await ContentService.UpdateSchemaAsync(Entity);
            }            
        }
    }
}
