﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Query;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentItems.Query;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
using DragonFly.Razor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        componentManager.Register(fieldType, typeof(TFieldComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, IContentField contentField, ContentFieldOptions? options)
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

        componentManager.Register(fieldOptionsType, typeof(TFieldOptionsComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, ContentFieldOptions options)
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

    public static void RegisterAssetMetadata<TMetadataComponent>(this ComponentManager componentManager)
      where TMetadataComponent : IAssetMetadataComponent
    {
        Type metadataType = typeof(TMetadataComponent).GetProperty(nameof(IAssetMetadataComponent.Metadata)).PropertyType;

        componentManager.Register(metadataType, typeof(TMetadataComponent));
    }

    public static RenderFragment CreateComponent(this ComponentManager componentManager, AssetMetadata metadata)
    {
        Type componentType = componentManager.GetComponentType(metadata.GetType());

        return builder =>
        {
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(0, nameof(IAssetMetadataComponent.Metadata), metadata);
            builder.CloseComponent();
        };
    }

    public static void RegisterQuery<TQueryView>(this ComponentManager componentManager)
        where TQueryView : IFieldQueryComponent
    {
        Type queryType = typeof(TQueryView).GetProperty(nameof(IFieldQueryComponent.Query)).PropertyType;

        componentManager.Register(queryType, typeof(TQueryView));
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
