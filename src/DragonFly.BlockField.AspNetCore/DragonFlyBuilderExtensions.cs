using DragonFly.Builders;
using DragonFly.BlockField;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton(BlockFieldManager.Default);

        builder.Init(api =>
        {
            api.ContentField().Add<BlockField.BlockField>();
        });

        return builder;
    }
}
