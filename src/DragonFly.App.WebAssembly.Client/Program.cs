// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DragonFly.Client;
using DragonFly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFly.App.WebAssembly.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFlyClient()
            .AddRestClient()
            .AddIdentity()
            .AddApiKeys()
            .Init(x =>
            {
                x.Field().Add<HtmlField>().WithTinyMceView();
            });

WebAssemblyHost host = builder.Build();

await host.InitDragonFlyAsync();
await host.RunAsync();
