// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFly.Client.Base;

public class StartComponentBase : ComponentBase
{
    public StartComponentBase()
    {
        RebuildToolbar();
    }

    private bool _init = false;

    public async Task InitAsync()
    {
        if (_init)
        {
            return;
        }

        _init = true;

        await InitActionAsync();
    }

    protected virtual Task InitActionAsync()
    {
        return Task.CompletedTask;
    }

    public async Task RefreshAsync()
    {
        //blocks recursive refreshing
        //if (IsRefreshing == true)
        //{
        //    return;
        //}

        IsRefreshing = true;

        try
        {
            await RefreshActionAsync();

            OnRefreshed();

            StateHasChanged();
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

    protected virtual Task RefreshActionAsync()
    {
        return Task.CompletedTask;
    }

    protected override async Task OnParametersSetAsync()
    {        
        await RefreshAsync();
    }

    //public override Task SetParametersAsync(ParameterView parameters)
    //{
    //    Console.WriteLine("SetParametersAsync: " + this.GetType().Name);
    //    Console.WriteLine("Parameters: " + string.Join(", ", parameters.ToDictionary().Keys));

    //    return base.SetParametersAsync(parameters);
    //}

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
        await InitAsync();
    }

    public async Task NavigateToExternalUrl(string url)
    {
        await JsRuntime.InvokeAsync<object>("open", url, "_blank");
    }
}
