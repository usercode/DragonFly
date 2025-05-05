// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Razor.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorStrap.V5;
using DragonFly.Razor.Extensions;

namespace DragonFly.Client.Pages.ContentItems;

public partial class ContentItemDetail
{

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

    public IList<ContentItem> SelectedVersions { get; private set; } = [];

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
        var item = await ContentVersionStorage.GetContentByVersionAsync(EntityType, id);

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
            toolbarItems.Add(new ToolbarItem("Publish", BSColor.Primary, PublishAsync));
            toolbarItems.Add(new ToolbarItem("Unpublish", BSColor.Primary, UnpublishAsync));
            toolbarItems.Add(new ToolbarItem("Clone", BSColor.Primary, CloneAsync));
            toolbarItems.Add(new ToolbarItem("Versions", BSColor.Primary, ToggleVersions));
            toolbarItems.AddRefreshButton(this);
            toolbarItems.AddSaveButton(this);
            toolbarItems.AddDeleteButton(this);

            if (string.IsNullOrEmpty(Entity.Schema.Preview.Pattern) == false)
            {
                toolbarItems.Add(new ToolbarItem("Preview", BSColor.Primary, PreviewAsync));
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

            Console.WriteLine(v);

            if (v.IsSucceeded)
            {
                Versions = v.Value.Items;
            }
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
                    Notifications.Add(new NotificationItem(NotificationType.Success, $"Content was published on {Entity.PublishedAt.Value}"));
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
        await ContentService.DeleteAsync(EntityType, EntityId);

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

        await ContentService.PublishAsync(EntityType, EntityId);

        await RefreshAsync();
    }

    public async Task UnpublishAsync()
    {
        await ContentService.UnpublishAsync(EntityType, EntityId);
    }

    private void RemoveSelectedVersion(ContentItem contentItem)
    {
        SelectedVersions.Remove(contentItem);

        StateHasChanged();
    }
}
