// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.WebHooks;

public partial class WebHookList
{
    /// <summary>
    /// WebHookStore
    /// </summary>
    [Inject]
    private IWebHookStorage WebHookStore { get; set; }

    protected override string GetNavigationPath(WebHook entity)
    {
        return $"webhook/{entity.Id}";
    }

    protected override async Task RefreshActionAsync()
    {
      
        SearchResult = await WebHookStore.QueryAsync(new WebHookQuery());
    }
}
