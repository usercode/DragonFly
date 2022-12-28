// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Razor.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DragonFly.Client.Base;

public class EntityDetailComponent<T> : StartComponentBase, IEntityDetailComponent<T>
    where T : IEntity
{
    public EntityDetailComponent()
    {
    }

    public Func<Task> Saving;
    public Func<Task> Saved;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IDataStorage ContentService { get; set; }

    public virtual bool IsNewEntity => EntityId == Guid.Empty;

    [Parameter]
    public Guid EntityId { get; set; }

    [Parameter]
    public string EntityType { get; set; }
    
    [Parameter]
    public T Entity { get; set; }

    public ContentSchema Schema { get; set; }

    [Parameter]
    public EventCallback<T> EntityChanged { get; set; }

    protected override async Task RefreshActionAsync()
    {

    }

    protected virtual async Task DeleteActionAsync()
    {
    }

    protected virtual async Task SaveActionAsync()
    {
        if (IsNewEntity)
        {
            await CreateAsync();
        }
        else
        {
            await UpdateAsync();
        }

        await RefreshAsync();
    }

    public async Task CreateAsync()
    {
        await CreateActionAsync();

        RebuildToolbar();
    }

    protected virtual async Task CreateActionAsync()
    {

    }

    public async Task UpdateAsync()
    {
        await UpdateActionAsync();

        RebuildToolbar();
    }

    protected virtual async Task UpdateActionAsync()
    {

    }

    public async Task SaveAsync()
    {
        SavingEventArgs args = new SavingEventArgs();

        OnSaving(args);

        if (args.CanSave == false)
        {
            return;
        }

        if (Saving != null)
        {
            await Saving();
        }

        await SaveActionAsync();

        OnSaved();

        if (Saved != null)
        {
            await Saved();
        }
    }

    protected virtual void OnSaving(SavingEventArgs args)
    {

    }

    protected virtual void OnSaved()
    {
        RebuildToolbar();
    }

    public async Task DeleteAsync()
    {
        await DeleteActionAsync();
    }

  
   
}
