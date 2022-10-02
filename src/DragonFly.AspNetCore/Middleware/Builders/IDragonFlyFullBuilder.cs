// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Middleware;

public interface IDragonFlyFullBuilder
{
    void Endpoints(Action<IDragonFlyEndpointRouteBuilder> endpoints);

    void PreAuthBuilder(Action<IDragonFlyApplicationBuilder> builder);

    void Builder(Action<IDragonFlyApplicationBuilder> builder);
}
