// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Maps the DragonFly manager. (Blazor WebAssembly)
    /// </summary>
    public static IApplicationBuilder UseDragonFlyManager(this IApplicationBuilder builder)
    {
        builder.Map("/manager",
                                x =>
                                {
                                    x.UseBlazorFrameworkFiles();
                                    x.UseStaticFiles();
                                    x.UseRouting();
                                    x.UseEndpoints(endpoints => endpoints.MapFallbackToFile("index.html"));
                                }
            );

        return builder;
    }
}
