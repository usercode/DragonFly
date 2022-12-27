// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;

namespace DragonFly.AspNetCore.Middleware;

public interface IDragonFlyMiddlewareBuilder
{
    void Endpoints(Action<IDragonFlyEndpointBuilder> endpoints);

    void PreAuthBuilder(Action<IDragonFlyApplicationBuilder> builder);

    void Builder(Action<IDragonFlyApplicationBuilder> builder);
}
