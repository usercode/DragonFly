using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Client.Core.Contents.ContentItems;
using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Core;
using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Models;
using DragonFly.Razor.Base;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems;

public class ContentItemDetailBase : EntityDetailComponent<ContentItem>
{
    public ContentItemDetailBase()
    {
        ValidationContext = new ValidationContext();
    }

    [Inject]
    public IEnumerable<IContentItemAction> ContentItemActions { get; set; }

    public ValidationContext ValidationContext { get; set; }

    [Parameter]
    public Guid? CloneFromEntityId { get; set; }

    public bool IsFieldValid(string field)
    {
        return ValidationContext.Errors.All(x => x.Field != field);
    }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if(IsNewEntity)
        {
            toolbarItems.AddCreateButton(this);
        }
        else
        {
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Success, () => PublishAsync()));
            toolbarItems.Add(new ToolbarItem("Unpublish", BSColor.Dark, () => UnpublishAsync()));
            toolbarItems.Add(new ToolbarItem("Clone", BSColor.Light, ()=> CloneAsync()));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddSaveButton(this);
            toolbarItems.AddDeleteButton(this);
        }

        if (ContentItemActions != null)
        {
            ContentItemActions.Foreach(x => toolbarItems.Add(new ToolbarItem(x.Name, BSColor.Dark, () => x.Execute(this))));
        }
    }

    protected override async Task RefreshActionAsync()
    {
        await base.RefreshActionAsync();

        ValidationContext = new ValidationContext();

        if (IsNewEntity)
        {
            Schema = await ContentService.GetSchemaAsync(EntityType);

            Entity = Schema.CreateContentItem();

            if(CloneFromEntityId != null)
            {
                ContentItem original = await ContentService.GetContentAsync(EntityType, CloneFromEntityId.Value);

                Entity.Fields = original.Fields;
            }
        }
        else
        {
            Entity = await ContentService.GetContentAsync(EntityType, EntityId);

            StateHasChanged();
        }
    }

    public async Task CloneAsync()
    {
        NavigationManager.NavigateTo($"content/{EntityType}/create/{Entity.Id}");
    }

    protected override async Task CreateActionAsync()
    {
        await ContentService.CreateAsync(Entity);

        NavigationManager.NavigateTo($"content/{EntityType}/{Entity.Id}");
    }

    protected override async Task UpdateActionAsync()
    {
        await ContentService.UpdateAsync(Entity);
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        ValidationContext = Entity.Validate();

        if (ValidationContext.Errors.Any())
        {
            StateHasChanged();

            args.CanSave = false;
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
