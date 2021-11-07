using DragonFly.AspNet.Middleware;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Middleware.Builders
{
    public class DragonFlyFullBuilder : IDragonFlyFullBuilder
    {
        public DragonFlyFullBuilder()
        {
            EndpointList = new List<Action<IDragonFlyEndpointRouteBuilder>>();
            PreAuthBuilders = new List<Action<IDragonFlyApplicationBuilder>>();
            PostAuthBuilders = new List<Action<IDragonFlyApplicationBuilder>>();
        }

        public IList<Action<IDragonFlyEndpointRouteBuilder>> EndpointList { get; }
        public IList<Action<IDragonFlyApplicationBuilder>> PreAuthBuilders { get; }
        public IList<Action<IDragonFlyApplicationBuilder>> PostAuthBuilders { get; }

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
            PostAuthBuilders.Add(builder);
        }
    }
}
