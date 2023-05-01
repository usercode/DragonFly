// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace DragonFly.AspNetCore.Builders;

class DragonFlyApplicationBuilder : IDragonFlyApplicationBuilder
{
    public DragonFlyApplicationBuilder(IApplicationBuilder builder)
    {
        Builder = builder;
    }

    public IApplicationBuilder Builder { get; }

    IServiceProvider IApplicationBuilder.ApplicationServices { get => Builder.ApplicationServices; set => Builder.ApplicationServices = value; }

    IFeatureCollection IApplicationBuilder.ServerFeatures => Builder.ServerFeatures;

    IDictionary<string, object?> IApplicationBuilder.Properties => Builder.Properties;

    RequestDelegate IApplicationBuilder.Build() => Builder.Build();

    IApplicationBuilder IApplicationBuilder.New() => Builder.New();

    IApplicationBuilder IApplicationBuilder.Use(Func<RequestDelegate, RequestDelegate> middleware) => Builder.Use(middleware);
}
