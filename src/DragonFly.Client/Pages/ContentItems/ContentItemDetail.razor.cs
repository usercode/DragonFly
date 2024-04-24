// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Razor.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Razor.Extensions;
using Microsoft.JSInterop;
using BlazorStrap.V5;

namespace DragonFly.Client.Pages.ContentItems;

public class ContentItemDetailBase : EntityDetailComponent<ContentItem>
{
    public ContentItemDetailBase()
    {
    }

    [Inject]
    public IEnumerable<IContentAction> ContentActions { get; set; }

    [Inject]
    public IContentStorage ContentService { get; set; }

    [Inject]
    public IContentVersionStorage ContentVersionStorage { get; set; }

    [Inject]
    public ISchemaStorage SchemaService { get; set; }

    [Inject]
    public ComponentManager ComponentManager { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public Guid? CloneFromEntityId { get; set; }

    public IEnumerable<ContentVersionEntry> Versions { get; set; }

    public BSOffCanvas _versionsModal;

    public IList<ContentItem> SelectedVersions { get; private set; } = new List<ContentItem>();

    public bool IsFieldValid(string field)
    {
        return Entity.ValidationState.Errors.All(x => x.Field != field);
    }

    protected BSColor GetFieldColor(string field)
    {
        bool valid = IsFieldValid(field);

        if (valid)
        {
            return BSColor.Default;
        }
        else
        {
            return BSColor.Danger;
        }
    }

    protected async Task ToggleVersions()
    {
        await _versionsModal.ToggleAsync();
    }

    protected async Task AddContentVersionsAsync(Guid id)
    {
        ContentItem item = await ContentVersionStorage.GetContentByVersionAsync(EntityType, id);

        SelectedVersions.Add(item);

        //reorder
        SelectedVersions = SelectedVersions.OrderByDescending(x => x.ModifiedAt).ToList();

        await ToggleVersions();
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
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Success, PublishAsync));
            toolbarItems.Add(new ToolbarItem("Unpublish", BSColor.Dark, UnpublishAsync));
            toolbarItems.Add(new ToolbarItem("Clone", BSColor.Dark, CloneAsync));
            toolbarItems.Add(new ToolbarItem("Versions", BSColor.Dark, ToggleVersions));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddSaveButton(this);
            toolbarItems.AddDeleteButton(this);

            if (string.IsNullOrEmpty(Entity.Schema.Preview.Pattern) == false)
            {
                toolbarItems.Add(new ToolbarItem("Preview", BSColor.Success, PreviewAsync));
            }
        }

        if (ContentActions != null)
        {
            ContentActions.Foreach(x => toolbarItems.Add(new ToolbarItem(x.Name, BSColor.Dark, () => x.Execute(this))));
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

            var v = await ContentVersionStorage.GetContentVersionsAsync(EntityType, EntityId);

            Versions = v.Value;
        }
    }

    protected override void OnGuiStateChanged()
    {
        base.OnGuiStateChanged();        

        if (Entity != null)
        {
            if (Entity.IsNew() == false)
            {
                if (Entity.PublishedAt == null)
                {
                    Notifications.Add(new NotificationItem(NotificationType.Warning, "Content is not published"));
                }
                else
                {
                    Notifications.Add(new NotificationItem(NotificationType.Success, $"Content has been published at {Entity.PublishedAt.Value}"));
                }
            }

            foreach (ValidationError error in Entity.ValidationState.Errors)
            {
                Notifications.Add(new NotificationItem(NotificationType.Error, error.Message));
            }
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

    protected virtual async Task PreviewAsync()
    {
        string url = Entity.GetPreviewUrl();

        Console.WriteLine("Preview: " + url);

        await JSRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    protected override void OnSaving(SavingEventArgs args)
    {
        if (Entity.Validate() == ValidationResult.Invalid)
        {
            OnGuiStateChanged();

            StateHasChanged();

            args.CanSave = false;
        }
    }

    public async Task PublishAsync()
    {
        await SaveAsync();

        await ContentService.PublishAsync(Entity);

        await RefreshAsync();
    }

    public async Task UnpublishAsync()
    {
        await ContentService.UnpublishAsync(Entity);
    }
}
