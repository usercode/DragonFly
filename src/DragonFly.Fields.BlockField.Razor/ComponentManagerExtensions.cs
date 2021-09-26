using DragonFly.Razor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor
{
    public static class ComponentManagerExtensions
    {
        public static void RegisterElement<TElementComponent>(this ComponentManager component)
            where TElementComponent : IElementComponent
        {
            Type? elementType = typeof(TElementComponent).GetProperty(nameof(IElementComponent.Element)).PropertyType;

            component.Register(elementType, typeof(TElementComponent));
        }

        public static RenderFragment CreateComponent(this ComponentManager componentManager, Element element)
        {
            Type componentType = componentManager.GetComponentType(element.GetType());

            return builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(0, nameof(IElementComponent.Element), element);
                builder.CloseComponent();
            };
        }
    }
}
