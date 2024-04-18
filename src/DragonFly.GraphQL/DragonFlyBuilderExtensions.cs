// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.AspNetCore.GraphQL;
using DragonFly.GraphQL.ObjectTypes;
using Microsoft.AspNetCore.Builder;

namespace DragonFly.AspNetCore;

public static class StartupExtensions
{
    public static IDragonFlyBuilder AddGraphQLApi(this IDragonFlyBuilder builder)
    {

        return builder;
    }

    //public static IDragonFlyApplicationBuilder UseGraphQLApi(this IDragonFlyApplicationBuilder builder)
    //{
    //    builder.Map("/graphql", x =>
    //    {
    //        x.UseRouting();
    //        x.UseAuthentication();
    //        x.UseAuthorization();

    //        x.UseGraphQL<DragonFlySchema>();
    //        x.UseGraphQLPlayground();

    //        x.UseEndpoints(endpoints =>
    //        {
                
    //        });
    //    });

    //    return builder;
    //}
}
