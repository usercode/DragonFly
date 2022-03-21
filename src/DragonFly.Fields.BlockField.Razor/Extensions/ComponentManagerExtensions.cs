using DragonFly.Razor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor;

public static class ComponentManagerExtensions
{
    public static void RegisterBlock<TBlockComponent>(this ComponentManager component)
        where TBlockComponent : IBlockComponent
    {
        Type? elementType = typeof(TBlockComponent).GetProperty(nameof(IBlockComponent.Block)).PropertyType;

        component.Register(elementType, typeof(TBlockComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, Block element)
    {
        Type componentType = componentManager.GetComponentType(element.GetType());

        return builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(0, nameof(IBlockComponent.Block), element);
            builder.CloseComponent();
        };
    }
}
