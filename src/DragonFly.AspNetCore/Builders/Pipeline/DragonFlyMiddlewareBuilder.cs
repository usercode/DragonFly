// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Builders;

internal class DragonFlyMiddlewareBuilder : IDragonFlyMiddlewareBuilder
{
    public DragonFlyMiddlewareBuilder()
    {
        EndpointList = new List<Action<IDragonFlyEndpointBuilder>>();
        PreAuthBuilders = new List<Action<IDragonFlyApplicationBuilder>>();
        Builders = new List<Action<IDragonFlyApplicationBuilder>>();
    }

    public IList<Action<IDragonFlyEndpointBuilder>> EndpointList { get; }
    public IList<Action<IDragonFlyApplicationBuilder>> PreAuthBuilders { get; }
    public IList<Action<IDragonFlyApplicationBuilder>> Builders { get; }

    public void Endpoints(Action<IDragonFlyEndpointBuilder> endpoints)
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
