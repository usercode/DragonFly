// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Client.Core.Contents.ContentItems;
using DragonFly.Validations;
using DragonFly.Razor.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Razor.Extensions;

namespace DragonFly.Client.Pages.ContentItems;

public class ContentItemDetailBase : EntityDetailComponent<ContentItem>
{
    public ContentItemDetailBase()
    {
    }

    [Inject]
    public IEnumerable<IContentItemAction> ContentItemActions { get; set; }


    [Inject]
    public IContentStorage ContentService { get; set; }


    [Inject]
    public ISchemaStorage SchemaService { get; set; }

    [Parameter]
    public Guid? CloneFromEntityId { get; set; }

    public bool IsFieldValid(string field)
    {
        return Entity.ValidationContext.Errors.All(x => x.Field != field);
    }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        base.BuildToolbarItems(toolbarItems);

        if (IsNewEntity)
        {
            toolbarItems.AddCreateButton(this);
        }
        else
        {
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Success, () => PublishAsync()));
            toolbarItems.Add(new ToolbarItem("Unpublish", BSColor.Dark, () => UnpublishAsync()));
            toolbarItems.Add(new ToolbarItem("Clone", BSColor.Light, () => CloneAsync()));
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

        if (IsNewEntity)
        {
            Schema = await SchemaService.GetSchemaAsync(EntityType);

            Entity = Schema.CreateContent();

            if (CloneFromEntityId != null)
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

        NavigationManager.NavigateToContent(Entity);
    }

    protected override async Task UpdateActionAsync()
    {
        await ContentService.UpdateAsync(Entity);
    }

    protected override async Task DeleteActionAsync()
    {
        await ContentService.DeleteAsync(Entity);

        NavigationManager.NavigateTo($"content/{EntityType}");
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        if (Entity.Validate() == ValidationState.Invalid)
        {
            StateHasChanged();

            args.CanSave = false;
        }
    }

    public async Task PublishAsync()
    {
        await SaveAsync();

        await ContentService.PublishAsync(Entity);
    }

    public async Task UnpublishAsync()
    {
        await ContentService.UnpublishAsync(Entity);
    }
}
