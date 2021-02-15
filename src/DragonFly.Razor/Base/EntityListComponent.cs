using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.Client.Pages.ContentItems;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Base
{
    public abstract class EntityListComponent<T> : StartComponentBase
        where T : ContentBase
    {
        public EntityListComponent()
        {
            ListMode = EntityListMode.Default;
        }

        [Parameter]
        public EntityListMode ListMode { get; set; }

        [Parameter]
        public string EntityType { get; set; }

        [Inject]
        public ClientContentService ContentService { get; set; }

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

        private string _searchPattern;

        /// <summary>
        /// SearchPattern
        /// </summary>
        [Parameter]
        public string SearchPattern 
        {
            get => _searchPattern;
            set { _searchPattern = value; RefreshAsync();  }
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
}
