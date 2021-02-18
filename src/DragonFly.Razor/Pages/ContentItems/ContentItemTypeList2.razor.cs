using DragonFly.Client.Base;
using DragonFly.Razor.Pages.ContentItems;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.Client.Pages.ContentItems
{
    public class ContentItemTypeListBase2 : ContentSchemaListBase
    {
        public ContentItemTypeListBase2()
        {

        }
        [Parameter]
        public ContentItem SelectedContentItem { get; set; }
       
        protected ContentItemList ContentItemList { get; set; }

        protected override async Task RefreshActionAsync()
        {
            SearchResult = await ContentService.GetContentSchemas();
        }

        public async Task RefreshContentItemsAsync(ContentSchema schema)
        {
            SelectedItem = schema;

            await ContentItemList.RefreshAsync(schema.Name);
        }

        public void SetResult(ContentItem contentItem)
        {
            SelectedContentItem = contentItem;

            Closed?.Invoke(this, true);
        }
    }
}
