// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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

    public static SourceBuilder AddNamespace(this SourceBuilder builder, string name, Action<SourceBuilder> body)
    {
        builder.AppendLine($"namespace {name}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddClass(this SourceBuilder builder, Modifier modifier, string name, Action<SourceBuilder> body, bool isPartial = false, bool isStatic = false, bool isSealed = false, IEnumerable<string>? baseTypes = null)
    {
        builder.AppendTabs();
        builder.Append($"{modifier} ");

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
        builder.AppendBlock(body);
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddMethod(this SourceBuilder builder, string name, TypeElement returnType, Modifier modifier, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} {returnType} {name}{parameterList}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddLambdaMethod(this SourceBuilder builder, Modifier modifier, TypeElement returnType, string name, ParameterList parameterList, string body)
    {
        builder.AppendLine($"{modifier} {returnType} {name}{parameterList} => {body};");

        return builder;
    }

    public static SourceBuilder AddVirtualMethod(this SourceBuilder builder, string name, TypeElement returnType, Modifier modifier, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} virtual {returnType} {name}{parameterList}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddStaticMethod(this SourceBuilder builder, string name, TypeElement returnType, Modifier modifier, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} static {returnType} {name}{parameterList}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddProperty(this SourceBuilder builder, string type, string name, Modifier modifier, bool isPartial = false, string? variable = null)
    {
        builder.AppendTabs();
        builder.Append($"{modifier} ");

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

    public static SourceBuilder AddField(this SourceBuilder builder, Modifier modifier, string type, string name, bool isReadOnly = false)
    {
        builder.AppendTabs();
        builder.Append($"{modifier} ");

        if (isReadOnly)
        {
            builder.Append("readonly ");
        }

        builder.Append($"{type} {name};");
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddConstructor(this SourceBuilder builder, Modifier modifier, string name, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} {name}{parameterList}");
        builder.AppendBlock(body);

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

    //public static SourceBuilder AddContentIdProperty(this SourceBuilder builder)
    //{
    //    builder.AppendLine("public Guid Id => _contentItem.Id;");

    //    return builder;
    //}

    public static SourceBuilder AddContentItemProperty(this SourceBuilder builder)
    {
        builder.AppendLine("public ContentItem GetContentItem() => _contentItem;");

        return builder;
    }

    public static SourceBuilder AddlambdaProperty(this SourceBuilder builder, Modifier modifier, TypeElement type, string name, string value)
    {
        builder.AppendLine($"{modifier} {type} {name} => {value};");

        return builder;
    }

    public static SourceBuilder AddContentMetadataCreateSchema(this SourceBuilder builder, string className, IEnumerable<ContentItemProperty> properties)
    {
        builder.AppendLine($"public ContentSchema CreateSchema()");
        builder.AppendBlock(x =>
        {
            x.AppendLine($"ContentSchema schema = new ContentSchema(\"{className}\");");
            x.AppendLine();

            foreach (ContentItemProperty property in properties)
            {
                string? parameters = null;

                if (string.IsNullOrEmpty(property.AttributeParameters) == false)
                {
                    parameters = $" {{ {property.AttributeParameters} }}";
                }

                x.AppendLine($"new {property.AttributeName}Attribute(){parameters}.AddToSchema(schema, \"{property.PropertyName}\");");
            }
            x.AppendLine();

            x.AppendLine($"return schema;");
        });

        return builder;
    }

    public static SourceBuilder AddExtensionMethod(this SourceBuilder builder, Modifier modifier, string name, string returnType, Parameter thisParamter, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} static {returnType} {name}(this {thisParamter})");
        builder.AppendBlock(body);
        builder.AppendLine();

        return builder;
    }
}
