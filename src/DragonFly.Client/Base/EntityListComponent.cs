﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Base;

public abstract class EntityListComponent<T> : StartComponentBase
    where T : IEntity
{
    public EntityListComponent()
    {
        ListMode = EntityListMode.Default;
        PageSize = 25;
    }

    [Parameter]
    public EntityListMode ListMode { get; set; }   

    [Inject]
    public IDataStorage ContentService { get; set; }

    [Parameter]
    public Action<T> ItemSelected { get; set; }

    [Parameter]
    public EventHandler<bool> Closed { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    public T SelectedItem { get; set; }

    /// <summary>
    /// SearchResult
    /// </summary>
    public QueryResult<T> SearchResult { get; set; }

    private int _page;

    [Parameter]
    [SupplyParameterFromQuery(Name = "page")]
    public int Page
    {
        get => _page;
        set
        {
            if (_page != value)
            {
                _page = value;

                Navigation.NavigateTo(Navigation.GetUriWithQueryParameter("page", _page));
            }
        }
    }

    public int PageSize { get; set; }

    public int CountPages => (int)Math.Ceiling((double)SearchResult.TotalCount / PageSize);

    private string _searchPattern;

    /// <summary>
    /// SearchPattern
    /// </summary>
    [Parameter]
    [SupplyParameterFromQuery(Name = "pattern")]
    public string SearchPattern 
    {
        get => _searchPattern;
        set 
        {
            if (_searchPattern != value)
            {
                _searchPattern = value;
            }
        }
    }

    public async Task NavigateAsync()
    {
        if (ListMode == EntityListMode.Default)
        {
            Navigation.NavigateTo(Navigation.GetUriWithQueryParameters(new Dictionary<string, object>() { ["pattern"] = SearchPattern, ["page"] = Page }));
        }
        else
        {
            await RefreshAsync();
        }
    }

    protected abstract string GetNavigationPath(T entity);

    public virtual void OpenItem(T entity)
    {
        if (ListMode == EntityListMode.Single)
        {
            SelectedItem = entity;

            ItemSelected(entity);
        }
        else
        {
            Navigation.NavigateTo(GetNavigationPath(entity));
        }
    }
}