using DragonFly.Models;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Base
{
    public class StartComponentBase : ComponentBase
    {
        public StartComponentBase()
        {
            ToolbarItems = new List<ToolbarItem>();

            BuildToolbarItems(ToolbarItems);
        }

        public async Task RefreshAsync()
        {
            IsRefreshing = true;

            try
            {
                await RefreshActionAsync();

                OnRefreshed();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public IList<ToolbarItem> ToolbarItems { get; private set; }

        public bool IsRefreshing { get; protected set; }

        protected virtual async Task RefreshActionAsync()
        {

        }

        protected override async Task OnParametersSetAsync()
        {
            //if(IsRefreshing == false)
            //{
            //    await RefreshAsync();
            //}
        }

        protected virtual void OnRefreshed()
        {
            List<ToolbarItem> list = new List<ToolbarItem>();
            BuildToolbarItems(list);
            ToolbarItems = list;

            StateHasChanged();
        }

        protected virtual void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await RefreshAsync();
        }

        public async Task NavigateToExternalUrl(string url)
        {
            await JsRuntime.InvokeAsync<object>("open", url, "_blank");
        }
    }
}
