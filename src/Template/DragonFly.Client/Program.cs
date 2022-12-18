// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.Razor;
using DragonFly.BlockField.Razor;
using DragonFly.Client.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFlyTemplate.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFlyClient()
            .AddBlockField()
            .AddIdentity()
            ;

WebAssemblyHost build = builder.Build();

build.UseDragonFlyClient();

await build.RunAsync();
