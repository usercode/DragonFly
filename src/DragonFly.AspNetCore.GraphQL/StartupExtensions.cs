using DragonFly.AspNetCore.GraphQL;
using DragonFly.AspNet.Middleware;
using DragonFly.Core.Builders;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.GraphQL
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddGraphQLApi(this IDragonFlyBuilder builder)
        {

            return builder;
        }

        public static IDragonFlyApplicationBuilder UseGraphQLApi(this IDragonFlyApplicationBuilder builder)
        {
            builder.Map("/graphql", x =>
            {
                x.UseRouting();
                x.UseAuthentication();
                x.UseAuthorization();

                //x.UseGraphQL<>();
                x.UseGraphQLPlayground(new GraphQLPlaygroundOptions
                {
                    Path = "/ui/playground"                    
                });

                x.UseEndpoints(endpoints =>
                {
                    
                });
            });

            return builder;
        }
    }
}
