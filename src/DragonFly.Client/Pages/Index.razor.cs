// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Client.Base;
using DragonFly.Query;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client.Pages;

public class IndexBase : StartComponentBase
{
    public IDictionary<ContentSchema, IList<ContentItem>> LatestContentItems { get; set; } = new Dictionary<ContentSchema, IList<ContentItem>>();

    public IList<ContentSchema> Schemas { get; set; }

    [Inject]
    public IContentStorage ContentStorage { get; set; }

    [Inject]
    public ISchemaStorage SchemaStorage { get; set; }

    protected override async Task RefreshActionAsync()
    {
        Schemas = (await SchemaStorage.QuerySchemasAsync()).Items;

        foreach (ContentSchema schema in Schemas)
        {
            QueryResult<ContentItem> result = await ContentStorage.QueryAsync(new ContentQuery(schema.Name)
                                                                                                .Top(10)
                                                                                                .OrderBy(nameof(ContentItem.ModifiedAt), false, false));

            LatestContentItems[schema] = result.Items;
        }
    }
}
