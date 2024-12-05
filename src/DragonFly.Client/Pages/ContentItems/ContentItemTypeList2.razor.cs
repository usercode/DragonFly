// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems;

public partial class ContentItemTypeList2
{
    [Parameter]
    public ContentItem SelectedContentItem { get; set; }

    [Inject]
    public ISchemaStorage ContentService { get; set; }

    protected ContentItemList ContentItemList { get; set; }

    protected override async Task RefreshActionAsync()
    {
        SearchResult = await ContentService.QuerySchemasAsync();
    }

    public async Task RefreshContentItemsAsync(ContentSchema schema)
    {
        SelectedItem = schema;

        if (ContentItemList != null)
        {
            await ContentItemList.RefreshAsync(schema.Name);
        }

        StateHasChanged();
    }

    public void SetResult(ContentItem contentItem)
    {
        SelectedContentItem = contentItem;

        Closed?.Invoke(this, true);
    }
}
