// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;

namespace DragonFly.AspNetCore.Middleware;

public interface IDragonFlyFullBuilder
{
    void Endpoints(Action<IDragonFlyEndpointRouteBuilder> endpoints);

    void PreAuthBuilder(Action<IDragonFlyApplicationBuilder> builder);

    void Builder(Action<IDragonFlyApplicationBuilder> builder);
}
