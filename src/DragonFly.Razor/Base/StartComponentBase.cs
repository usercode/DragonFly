using DragonFly.Models;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Base
{
    public class StartComponentBase : ComponentBase
    {
        public StartComponentBase()
        {
            RebuildToolbar();
        }

        protected bool _init = false;
        public Task RefreshAsync()
        {
            return RefreshAsync(true);
        }
         
        public async Task RefreshAsync(bool stateHasChanged)
        {
            IsRefreshing = true;

            try
            {
                await RefreshActionAsync();

                OnRefreshed();

                if (stateHasChanged)
                {
                    await InvokeAsync(StateHasChanged);
                }
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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Debug.WriteLine("SetParametersAsync");           

            foreach (var k in parameters)
            {
                Debug.WriteLine(k.Name + " " + k.Value);
            }

            await base.SetParametersAsync(parameters);
        }

        protected override async Task OnParametersSetAsync()
        {
            await RefreshAsync(false);
        }

        protected virtual void OnRefreshed()
        {
            RebuildToolbar();
        }

        protected void RebuildToolbar()
        {
            List<ToolbarItem> list = new List<ToolbarItem>();
            BuildToolbarItems(list);
            ToolbarItems = list;
        }

        protected virtual void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {

        }

        protected override async Task OnInitializedAsync()
        {
            //await InvokeAsync(RefreshAsync);
        }

        public async Task NavigateToExternalUrl(string url)
        {
            await JsRuntime.InvokeAsync<object>("open", url, "_blank");
        }
    }
}
