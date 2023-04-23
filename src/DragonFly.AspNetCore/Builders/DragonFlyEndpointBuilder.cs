// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Builders;

class DragonFlyEndpointBuilder : IDragonFlyEndpointBuilder
{
    public DragonFlyEndpointBuilder(IEndpointRouteBuilder endpointRouteBuilder)
    {
        EndpointRouteBuilder = endpointRouteBuilder;
    }

    private IEndpointRouteBuilder EndpointRouteBuilder { get; }

    public IServiceProvider ServiceProvider => EndpointRouteBuilder.ServiceProvider;

    public ICollection<EndpointDataSource> DataSources => EndpointRouteBuilder.DataSources;

    public IApplicationBuilder CreateApplicationBuilder() => EndpointRouteBuilder.CreateApplicationBuilder();
}
