// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Middleware.Builders;

class DragonFlyEndpointRouteBuilder : IDragonFlyEndpointRouteBuilder
{
    public DragonFlyEndpointRouteBuilder(IEndpointRouteBuilder endpointRouteBuilder)
    {
        EndpointRouteBuilder = endpointRouteBuilder;
    }

    private IEndpointRouteBuilder EndpointRouteBuilder { get; }

    public IServiceProvider ServiceProvider => EndpointRouteBuilder.ServiceProvider;

    public ICollection<EndpointDataSource> DataSources => EndpointRouteBuilder.DataSources;

    public IApplicationBuilder CreateApplicationBuilder() => EndpointRouteBuilder.CreateApplicationBuilder();
}
