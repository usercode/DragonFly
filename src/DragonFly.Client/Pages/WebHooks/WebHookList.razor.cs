// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems;

public class WebHookListBase : EntityListComponent<WebHook>
{
    public WebHookListBase()
    {
    }

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
