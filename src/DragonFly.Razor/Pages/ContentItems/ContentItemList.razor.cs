using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Core.ContentItems.Queries;
using DragonFly.Models;
using DragonFly.Razor.Services;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems
{
    public class ContentItemListBase : EntityListComponent<ContentItem>
    {
        public ContentItemListBase()
        {
            OrderFields = new List<FieldOrder>();
            QueryFields = new List<FieldQueryBase>();
        }

        [Inject]
        public ContentFieldManager ContentFieldManager { get; set; }

        /// <summary>
        /// Schema
        /// </summary>
        public ContentSchema Schema { get; set; }

        /// <summary>
        /// FieldOrder
        /// </summary>
        public IList<FieldOrder> OrderFields { get; private set; }

        /// <summary>
        /// QueryFields
        /// </summary>
        public IList<FieldQueryBase> QueryFields { get; private set; }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            toolbarItems.Add(new ToolbarItem("Create", Color.Success, async () => Navigation.NavigateTo($"content/{EntityType}/create")));
            toolbarItems.AddRefreshButton(this);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();


        }

        protected override async Task RefreshActionAsync()
        {
            if (EntityType != null)
            {
                Schema = await ContentService.GetContentSchemaAsync(EntityType);

                if (OrderFields.Any() == false)
                {
                    OrderFields = Schema.OrderFields.ToList();
                }

                if (QueryFields.Any() == false)
                {
                    foreach (var field in Schema.Fields.Where(x => x.Value.Options.IsSearchable))
                    {
                        FieldQueryBase q = ContentFieldManager.CreateQuery(field.Value.FieldType);
                        q.FieldName = field.Key;

                        QueryFields.Add(q);
                    }
                }
                
                var queryParameters = new QueryParameters()
                {
                   SearchPattern = SearchPattern,
                   Skip = CurrentPageIndex * PageSize,
                   Top = PageSize
                };

                foreach (FieldOrder f in OrderFields)
                {
                    queryParameters.AddFieldOrder(f.Name, f.Asc);
                }

                foreach (FieldQueryBase query in QueryFields)
                {
                    queryParameters.Fields2.Add(query);
                }

                SearchResult = await ContentService.QueryAsync(Schema.Name, queryParameters);
            }
        }

        public async Task Search(string pattern)
        {

        }

        public async Task RefreshAsync(string schema)
        {
            EntityType = schema;

            await RefreshAsync();
        }

        protected override string GetNavigationPath(ContentItem entity)
        {
            return $"content/{entity.Schema.Name}/{entity.Id}";
        }
    }
}
