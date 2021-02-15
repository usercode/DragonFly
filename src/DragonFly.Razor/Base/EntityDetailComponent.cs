using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Base
{
    public class EntityDetailComponent<T> : StartComponentBase, IEntityDetailComponent<T>
        where T : ContentBase
    {
        public EntityDetailComponent()
        {
        }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ClientContentService ContentService { get; set; }

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
            if(IsNewEntity)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();

                await RefreshAsync();
            }
        }

        public async Task CreateAsync()
        {
            
        }

        public async Task UpdateAsync()
        {

        }

        public async Task SaveAsync()
        {
            await SaveActionAsync();
        }

        public async Task DeleteAsync()
        {
            await DeleteActionAsync();
        }

      
       
    }
}
