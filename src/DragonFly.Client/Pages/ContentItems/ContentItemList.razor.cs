﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems;

public partial class ContentItemList
{
    [Parameter]
    public string SchemaName { get; set; }

    [Parameter]
    public Asset UsedAsset { get; set; }

    [Inject]
    public FieldManager ContentFieldManager { get; set; }


    [Inject]
    public IContentStorage ContentService { get; set; }


    [Inject]
    public ISchemaStorage SchemaService { get; set; }

    /// <summary>
    /// Schema
    /// </summary>
    public ContentSchema Schema { get; set; }

    /// <summary>
    /// FieldOrder
    /// </summary>
    public IList<FieldOrder> OrderFields { get; private set; } = new List<FieldOrder>();

    /// <summary>
    /// QueryFields
    /// </summary>
    public IList<FieldQuery> QueryFields { get; private set; } = new List<FieldQuery>();

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        toolbarItems.Add(new ToolbarItem("Create", BSColor.Primary, async () => Navigation.NavigateTo($"content/{SchemaName}/create")));
        toolbarItems.Add(new ToolbarItem("Publish all", BSColor.Primary, PublishQueryAsync));
        toolbarItems.Add(new ToolbarItem("Unpublish all", BSColor.Danger, UnpublishQueryAsync));
        toolbarItems.AddRefreshButton(this);
    }

    protected override async Task RefreshActionAsync()
    {
        if (SchemaName != null)
        {
            Schema = await SchemaService.GetSchemaAsync(SchemaName);

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

            ContentQuery quey = CreateQuery();

            SearchResult = await ContentService.QueryAsync(quey);
        }
    }

    private ContentQuery CreateQuery()
    {
        ContentQuery query = new()
        {
            Pattern = SearchPattern,
            Skip = Page * PageSize,
            Take = PageSize,
            Published = false
        };

        foreach (FieldOrder f in OrderFields)
        {
            query.OrderBy(f.Name, f.Asc);
        }

        foreach (FieldQuery fieldQuery in QueryFields.Where(x => x.IsEmpty() == false))
        {
            query.Fields.Add(fieldQuery);
        }

        query.Schema = Schema.Name;
        query.Reference = UsedAsset?.ToReference();
        
        return query;
    }

    public async Task PublishQueryAsync()
    {
        ContentQuery query = CreateQuery();

        await ContentService.PublishQueryAsync(query);
    }

    public async Task UnpublishQueryAsync()
    {
        ContentQuery query = CreateQuery();

        await ContentService.UnpublishQueryAsync(query);
    }

    public async Task Search(string pattern)
    {

    }

    public async Task RefreshAsync(string schema)
    {
        SchemaName = schema;

        await RefreshAsync();
    }

    protected override string GetNavigationPath(ContentItem entity)
    {
        return $"content/{entity.Schema.Name}/{entity.Id}";
    }
}
