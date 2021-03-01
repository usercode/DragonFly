using BlazorStrap;
using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Shared.UI.Toolbars
{
    public static class ToolbarItemExtensions
    {
        //toolbarItems.Add(new ToolbarItem("Publish", Color.Success, () => PublishAsync()));
        //    toolbarItems.Add(new ToolbarItem("Unpublish", Color.Dark, () => UnpublishAsync()));
        //    toolbarItems.AddRefreshButton(this);
        //toolbarItems.Add(new ToolbarItem("Save", Color.Success, () => SaveAsync()));
        //    toolbarItems.Add(new ToolbarItem("Delete", Color.Danger, () => DeleteAsync()));
        public static void AddRefreshButton(this IList<ToolbarItem> items, StartComponentBase component)
        {
            items.Add(new ToolbarItem("Refresh", Color.Dark, () => component.RefreshAsync()));
        }

        public static void AddSaveButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
            where T : ContentBase
        {
            items.Add(new ToolbarItem("Save", Color.Success, () => component.SaveAsync()));
        }

        public static void AddCreateButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
            where T : ContentBase
        {
            items.Add(new ToolbarItem("Create", Color.Success, () => component.CreateAsync()));
        }

        public static void AddUpdateButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
            where T : ContentBase
        {
            items.Add(new ToolbarItem("Update", Color.Success, () => component.UpdateAsync()));
        }

        public static void AddDeleteButton<T>(this IList<ToolbarItem> items, IEntityDetailComponent<T> component)
           where T : ContentBase
        {
            items.Add(new ToolbarItem("Delete", Color.Danger, () => component.DeleteAsync()));
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
}
