// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DragonFly.Client;

public static class DragonFlyRenderMode
{
    /// <summary>
    /// Current
    /// </summary>
    public static IComponentRenderMode Current { get; set; } = new InteractiveServerRenderMode(prerender: false);
}
