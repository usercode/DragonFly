// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;

namespace DragonFly.AspNetCore.Middleware.Builders;

public class DragonFlyFullBuilder : IDragonFlyFullBuilder
{
    public DragonFlyFullBuilder()
    {
        EndpointList = new List<Action<IDragonFlyEndpointRouteBuilder>>();
        PreAuthBuilders = new List<Action<IDragonFlyApplicationBuilder>>();
        Builders = new List<Action<IDragonFlyApplicationBuilder>>();
    }

    public IList<Action<IDragonFlyEndpointRouteBuilder>> EndpointList { get; }
    public IList<Action<IDragonFlyApplicationBuilder>> PreAuthBuilders { get; }
    public IList<Action<IDragonFlyApplicationBuilder>> Builders { get; }

    public void Endpoints(Action<IDragonFlyEndpointRouteBuilder> endpoints)
    {
        EndpointList.Add(endpoints);
    }

    public void PreAuthBuilder(Action<IDragonFlyApplicationBuilder> builder)
    {
        PreAuthBuilders.Add(builder);
    }

    public void Builder(Action<IDragonFlyApplicationBuilder> builder)
    {
        Builders.Add(builder);
    }
}
