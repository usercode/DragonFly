using DragonFly.Core.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyBuilderExtensions
    {
        public static void Init(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
        {
            builder.Services.AddTransient<IInitialize>(x => new InitItem(action));
        }

        public static void Init<T>(this IDragonFlyBuilder builder)
            where T : class, IInitialize
        {
            builder.Services.AddTransient<IInitialize, T>();
        }

        public static void PreInit<T>(this IDragonFlyBuilder builder)
            where T : class, IPreInitialize
        {
            builder.Services.AddTransient<IPreInitialize, T>();
        }

        public static void PreInit(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
        {
            builder.Services.AddTransient<IPreInitialize>(x => new InitItem(action));
        }

        public static void PostInit<T>(this IDragonFlyBuilder builder)
            where T : class, IPostInitialize
        {
            builder.Services.AddTransient<IPostInitialize, T>();
        }

        public static void PostInit(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
        {
            builder.Services.AddTransient<IPostInitialize>(x => new InitItem(action));
        }
    }
}
