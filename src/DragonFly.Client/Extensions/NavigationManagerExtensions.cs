// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Extensions;

public static class NavigationManagerExtensions
{
    public static void NavigateToAssets(this NavigationManager manager)
    {
        manager.NavigateTo("asset");
    }

    public static void NavigateToAsset(this NavigationManager manager, Asset asset)
    {
        manager.NavigateTo($"asset/{asset.Id}");
    }

    public static void NavigateToCreateAsset(this NavigationManager manager, long? folderId = null)
    {
        manager.NavigateTo($"asset/create/{folderId}");
    }

    public static void NavigateToContent(this NavigationManager manager, ContentItem content)
    {
        manager.NavigateTo($"content/{content.Schema.Name}/{content.Id}");
    }
}
