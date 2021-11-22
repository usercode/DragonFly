using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Content.Queries;
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
            QueryFields = new List<FieldQuery>();
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
        public IList<FieldQuery> QueryFields { get; private set; }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            toolbarItems.Add(new ToolbarItem("Create", Color.Success, async () => Navigation.NavigateTo($"content/{EntityType}/create")));
            toolbarItems.Add(new ToolbarItem("Publish all", Color.Success, async () => await PublishQueryAsync()));
            toolbarItems.AddRefreshButton(this);            
        }

        protected override async Task RefreshActionAsync()
        {
            if (EntityType != null)
            {
                Schema = await ContentService.GetSchemaAsync(EntityType);

                if (OrderFields.Any() == false)
                {
                    OrderFields = Schema.OrderFields.ToList();
                }

                if (QueryFields.Any() == false)
                {
                    foreach (var field in Schema.QueryFields
                                                    .Select(x => Schema.Fields.First(f => f.Key == x))
                                                    .ToList())
                    {
                        FieldQuery q = ContentFieldManager.CreateQuery(field.Value.FieldType);

                        if (q == null)
                        {
                            continue;
                        }

                        q.FieldName = field.Key;

                        QueryFields.Add(q);
                    }
                }

                ContentItemQuery quey = CreateQuery();

                SearchResult = await ContentService.QueryAsync(quey);
            }
        }

        private ContentItemQuery CreateQuery()
        {
            ContentItemQuery query = new()
            {
                SearchPattern = SearchPattern,
                Skip = Page * PageSize,
                Top = PageSize
            };

            foreach (FieldOrder f in OrderFields)
            {
                query.AddFieldOrder(f.Name, f.Asc);
            }

            foreach (FieldQuery fieldQuery in QueryFields.Where(x => x.IsEmpty() == false))
            {
                query.Fields.Add(fieldQuery);
            }

            query.Schema = Schema.Name;

            return query;
        }

        public async Task PublishQueryAsync()
        {
            ContentItemQuery query = CreateQuery();

            await ContentService.PublishQueryAsync(query);
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
