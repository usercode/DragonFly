// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DragonFly.Client;
using DragonFly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<DragonFlyApp.Client.App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDragonFly()
            .AddRestClient()
            .AddIdentity()
            .AddApiKeys()
            .Init(x =>
            {
                //Use TinyMCE editor for html field
                //x.Field().Add<HtmlField>().WithTinyMceView();
            });

WebAssemblyHost host = builder.Build();

await host.InitDragonFlyAsync();
await host.RunAsync();
