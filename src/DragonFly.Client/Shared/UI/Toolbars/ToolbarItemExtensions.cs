﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Client.Base;
using System.Collections.Generic;

namespace DragonFly.Client;

public static class ToolbarItemExtensions
{
    //toolbarItems.Add(new ToolbarItem("Publish", Color.Success, () => PublishAsync()));
    //    toolbarItems.Add(new ToolbarItem("Unpublish", Color.Dark, () => UnpublishAsync()));
    //    toolbarItems.AddRefreshButton(this);
    //toolbarItems.Add(new ToolbarItem("Save", Color.Success, () => SaveAsync()));
    //    toolbarItems.Add(new ToolbarItem("Delete", Color.Danger, () => DeleteAsync()));
    public static void AddRefreshButton(this IList<ToolbarItem> items, StartComponentBase component)
    {
        items.Add(new ToolbarItem("Refresh", BSColor.Primary, () => component.RefreshAsync()));
    }

    public static void AddSaveButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
        where T : IEntity
    {
        items.Add(new ToolbarItem("Save", BSColor.Primary, () => component.SaveAsync()));
    }

    public static void AddCreateButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
        where T : IEntity
    {
        items.Add(new ToolbarItem("Create", BSColor.Primary, () => component.SaveAsync()));
    }

    public static void AddUpdateButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
        where T : IEntity
    {
        items.Add(new ToolbarItem("Update", BSColor.Primary, () => component.SaveAsync()));
    }

    public static void AddDeleteButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
       where T : IEntity
    {
        items.Add(new ToolbarItem("Delete", BSColor.Danger, () => component.DeleteAsync()));
    }

    //public static void AddPublishButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
    //   where T : ContentBase
    //{
    //    items.Add(new ToolbarItem("Publish", BlazorStrap.Color.Success, () => component.PublishAsync()));
    //}

    //public static void AddUnpublishButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
    //   where T : ContentBase
    //{
    //    items.Add(new ToolbarItem("Unpublish", BlazorStrap.Color.Dark, () => component.UnpublishAsync()));
    //}
}
