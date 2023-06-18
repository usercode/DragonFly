// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Generator.SourceBuilder;

static class SourceBuilderExtensions
{
    public static SourceBuilder AddUsings(this SourceBuilder builder, params string[] usings)
    {
        foreach (string usingName in usings)
        {
            builder.AppendLine($"using {usingName};");
        }

        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddNamespace(this SourceBuilder builder, string name, Action<SourceBuilder> inner)
    {
        builder.AppendLine($"namespace {name}");
        builder.AppendBlock(inner);

        return builder;
    }

    public static SourceBuilder AddClass(this SourceBuilder builder, Modifier modifier, string name, Action<SourceBuilder> inner, bool isPartial = false, bool isStatic = false, bool isSealed = false, IEnumerable<string>? baseTypes = null)
    {
        builder.AppendTabs();
        builder.Append($"{modifier.Name} ");

        if (isStatic)
        {
            builder.Append("static ");
        }

        if (isSealed)
        {
            builder.Append("sealed ");
        }

        if (isPartial)
        {
            builder.Append($"partial ");
        }

        builder.Append($"class {name}");

        if (baseTypes != null)
        {
            builder.Append($" : {string.Join(", ", baseTypes)}");            
        }

        builder.AppendLine();
        builder.AppendBlock(inner);
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddContentConstructor(this SourceBuilder builder, Modifier modifier, string name)
    {
        builder.AppendLine($"{modifier} {name}(ContentItem contentItem)");
        builder.AppendBlock(x => x.AppendLine("_contentItem = contentItem;"));
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddContentProperty(this SourceBuilder builder, ContentItemProperty property)
    {
        builder.AppendTabs();

        builder.Append($"public {property.PropertyType} {property.PropertyName}");
        builder.AppendLine();

        string type = property.PropertyType;

        if (property.IsSingleValue)
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get => _contentItem.GetSingleValue<{type}>(\"{property.PropertyName}\");");
                x.AppendLine($"set => _contentItem.SetSingleValue<{type}>(\"{property.PropertyName}\", value);");
            });
        }
        else
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get => _contentItem.GetField<{type}>(\"{property.PropertyName}\");");
                x.AppendLine($"set => _contentItem.SetField(\"{property.PropertyName}\", value);");
            });
        }
        
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddContentIdProperty(this SourceBuilder builder)
    {
        builder.AppendLine("public Guid Id => _contentItem.Id;");

        return builder;
    }

    public static SourceBuilder AddContentItemProperty(this SourceBuilder builder)
    {
        builder.AppendLine("public ContentItem GetContentItem() => _contentItem;");

        return builder;
    }

    public static SourceBuilder AddContentMetadataProperty(this SourceBuilder builder, string type, string name, string value)
    {
        builder.AppendLine($"public {type} {name} => \"{value}\";");

        return builder;
    }

    public static SourceBuilder AddContentMetadataCreate(this SourceBuilder builder, string className)
    {
        builder.AppendLine($"IContentModel IContentMetadata.Create(ContentItem contentItem) => Create(contentItem);");

        return builder;
    }

    public static SourceBuilder AddContentMetadata2Create(this SourceBuilder builder, string className)
    {
        builder.AppendLine($"public {className} Create(ContentItem contentItem) => new {className}(contentItem);");

        return builder;
    }

    public static SourceBuilder AddStaticContentMetadataProperty(this SourceBuilder builder, string className)
    {
        builder.AppendLine($"static IContentMetadata IContentModel.Metadata => Metadata;");

        return builder;
    }

    public static SourceBuilder AddStaticContentMetadata2Property(this SourceBuilder builder, string className)
    {
        builder.AppendLine($"public static {className}Metadata Metadata => new {className}Metadata();");

        return builder;
    }

    public static SourceBuilder AddContentMetadataModelNameProperty(this SourceBuilder builder, string className)
    {
        builder.AppendLine($"public string ModelName => \"{className}\";");

        return builder;
    }

    public static SourceBuilder AddProperty(this SourceBuilder builder, string type, string name, bool isPublic = true, bool isPartial = false, string? variable = null)
    {
        builder.AppendTabs();

        if (isPublic)
        {
            builder.Append("public ");
        }
        else
        {
            builder.Append("private ");
        }

        if (isPartial)
        {
            builder.Append("partial ");
        }

        builder.Append($"{type} {name}");
        builder.AppendLine();

        if (variable == null)
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get;");
                x.AppendLine($"set;");
            });
        }
        else
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get => {variable};");
                x.AppendLine($"set => {variable} = value;");
            });
        }

        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddReadOnlyField(this SourceBuilder builder, Modifier modifier, string type, string name)
    {
        builder.AppendLine($"{modifier} readonly {type} {name};");
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddExtensionToModel(this SourceBuilder builder, string type)
    {
        builder.AppendLine($"public static {type} To{type}(this ContentItem contentItem) => {type}.Metadata.Create(contentItem);");
        builder.AppendLine();

        return builder;
    }
}
