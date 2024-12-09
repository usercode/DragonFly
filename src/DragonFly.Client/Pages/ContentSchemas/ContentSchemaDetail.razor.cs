// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentSchemas;

public partial class ContentSchemaDetail
{
    [Inject]
    public ISchemaStorage ContentService { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if (IsNewEntity)
        {
            toolbarItems.AddCreateButton(this);
        }
        else
        {
            toolbarItems.AddUpdateButton(this);
            toolbarItems.AddRefreshButton(this);            
            toolbarItems.AddDeleteButton(this);
        }
    }

    protected override async Task RefreshActionAsync()
    {
        await base.RefreshActionAsync();

        if (IsNewEntity)
        {
            Entity = new ContentSchema(string.Empty);
        }
        else
        {
            Entity = await ContentService.GetSchemaAsync(EntityId);
        }
    }

    protected override async Task CreateActionAsync()
    {
        await ContentService.CreateAsync(Entity);

        NavigationManager.NavigateTo($"schema/{Entity.Id}");
    }

    protected override async Task UpdateActionAsync()
    {
        await ContentService.UpdateAsync(Entity);
    }

    protected override async Task DeleteActionAsync()
    {
        await ContentService.DeleteAsync(Entity);

        NavigationManager.NavigateTo($"schema");
    }
}
