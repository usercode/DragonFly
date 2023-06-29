// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using DragonFly.Query;
using Microsoft.AspNetCore.Components;
using System;

namespace DragonFly;

/// <summary>
/// ComponentManagerExtensions
/// </summary>
public static class ComponentManagerExtensions
{
    public static void RegisterField<TFieldComponent>(this ComponentManager componentManager)
        where TFieldComponent : IFieldComponent
    {
        Type fieldType = typeof(TFieldComponent).GetProperty(nameof(IFieldComponent.Field)).PropertyType;

        componentManager.Add(fieldType, typeof(TFieldComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, ContentField contentField, FieldOptions? options)
    {
        Type componentType = componentManager.GetComponentType(contentField.GetType());

        return builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(0, nameof(IFieldComponent.Field), contentField);
            builder.AddAttribute(1, nameof(IFieldComponent.Options), options);
            builder.CloseComponent();
        };
    }

    public static void RegisterOptions<TFieldOptionsComponent>(this ComponentManager componentManager)
       where TFieldOptionsComponent : IFieldOptionsComponent
    {
        Type fieldOptionsType = typeof(TFieldOptionsComponent).GetProperty(nameof(IFieldOptionsComponent.Options)).PropertyType;

        componentManager.Add(fieldOptionsType, typeof(TFieldOptionsComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, FieldOptions options)
    {
        Type componentType = componentManager.GetComponentType(options.GetType());

        if (componentType != null)
        {
            return builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.AddAttribute(0, nameof(IFieldOptionsComponent.Options), options);
                builder.CloseComponent();
            };
        }
        else
        {
            return builder => { builder.OpenElement(0, "p"); builder.AddContent(0, $"The view for {options.GetType().Name} is not available."); builder.CloseElement(); };
        }            
    }

    public static void RegisterQuery<TQueryView>(this ComponentManager componentManager)
        where TQueryView : IFieldQueryComponent
    {
        Type queryType = typeof(TQueryView).GetProperty(nameof(IFieldQueryComponent.Query)).PropertyType;

        componentManager.Add(queryType, typeof(TQueryView));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, FieldQuery fieldQuery)
    {
        Type componentType = componentManager.GetComponentType(fieldQuery.GetType());

        return builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(0, nameof(IFieldQueryComponent.Query), fieldQuery);
            builder.CloseComponent();
        };
    }
}
