// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.CodeAnalysis;

namespace DragonFly.Generator;

public static class SourceBuilderExtensions
{
    public static SourceBuilder AppendPreprocessorDirectives(this SourceBuilder builder)
    {
        builder.AppendLine("#nullable enable");
        builder.AppendLine("#pragma warning disable CS8618");
        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddUsings(this SourceBuilder builder, params string[] usings)
    {
        foreach (string usingName in usings
                                        .Distinct())
        {
            builder.AppendLine($"using {usingName};");
        }

        builder.AppendLine();

        return builder;
    }

    public static SourceBuilder AddNamespace(this SourceBuilder builder, string name, Action<SourceBuilder> body, bool useFileScope = false)
    {
        if (useFileScope)
        {
            builder.AppendLine($"namespace {name};");
            builder.AppendLineBreak();

            body(builder);
        }
        else
        {
            builder.AppendLine($"namespace {name}");
            builder.AppendBlock(body);
        }

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

        builder.AppendLineBreak();
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddMethod(this SourceBuilder builder, string name, TypeElement returnType, Modifier modifier, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} {returnType} {name}{parameterList}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddLambdaMethod(this SourceBuilder builder, Modifier modifier, TypeElement returnType, string name, ParameterList parameterList, string body, bool isStatic = false)
    {
        builder.AppendTabs();
        builder.Append($"{modifier} ");

        if (isStatic)
        {
            builder.Append("static ");
        }

        builder.Append($"{returnType} {name}{parameterList} => {body};");
        builder.AppendLineBreak();

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
        builder.AppendLineBreak();

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
        builder.AppendLineBreak();

        return builder;
    }

    public static SourceBuilder AddConstructor(this SourceBuilder builder, Modifier modifier, string name, ParameterList parameterList, Action<SourceBuilder> body)
    {
        builder.AppendLine($"{modifier} {name}{parameterList}");
        builder.AppendBlock(body);

        return builder;
    }

    public static SourceBuilder AddContentProperty(this SourceBuilder builder, ContentItemProperty property)
    {
        builder.AppendTabs();

        string propertyType = property.PropertyTypeSymbol.ToDisplayString();
        string propertyTypeMaybeNull = property.PropertyTypeSymbol.ToDisplayString(NullableFlowState.MaybeNull);

        bool IsSingleValueType = property.PropertyTypeSymbol.IsValueType == true || property.PropertyTypeSymbol.Name == "String";
        bool isDirectReferenceField = property.AttributeTypeSymbol.Name == "ReferenceFieldAttribute" && property.PropertyTypeSymbol.Name != "ReferenceField";
        bool isDirectAssetField = property.AttributeTypeSymbol.Name == "AssetFieldAttribute" && property.PropertyTypeSymbol.Name != "AssetField";

        builder.Append($"public {(IsSingleValueType || isDirectReferenceField || isDirectAssetField ? propertyTypeMaybeNull : propertyType)} {property.PropertyName}");
        builder.AppendLineBreak();

        //primitive type or string?
        if (IsSingleValueType)
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get => _contentItem.GetSingleValue<{propertyType}>(\"{property.PropertyName}\");");
                x.AppendLine($"set => _contentItem.SetSingleValue<{propertyType}>(\"{property.PropertyName}\", value);");
            });
        }
        //ReferenceField
        else if (isDirectReferenceField)
        {
            if (property.PropertyTypeSymbol.Name == "ContentItem")
            {
                builder.AppendBlock(x =>
                {
                    x.AppendLine($"get");
                    x.AppendBlock(x =>
                    {
                        x.AppendLine($"ReferenceField field = _contentItem.GetField<ReferenceField>(\"{property.PropertyName}\");");                        
                        x.AppendLine($"return field.ContentItem;");
                    });

                    x.AppendLine($"set");
                    x.AppendBlock(x =>
                    {
                        x.AppendLine($"ReferenceField field = _contentItem.GetField<ReferenceField>(\"{property.PropertyName}\");");
                        x.AppendLine($"field.ContentItem = value;");
                    });
                });
            }
            else
            {
                builder.AppendBlock(x =>
                {
                    x.AppendLine($"get");
                    x.AppendBlock(x =>
                    {
                        x.AppendLine($"ReferenceField field = _contentItem.GetField<ReferenceField>(\"{property.PropertyName}\");");
                        x.AppendLine("if (field.ContentItem == null)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine("return null;");
                        });
                        x.AppendLine($"return {propertyType}.Create(field.ContentItem);");
                    });

                    x.AppendLine($"set");
                    x.AppendBlock(x =>
                    {
                        x.AppendLine($"ReferenceField field = _contentItem.GetField<ReferenceField>(\"{property.PropertyName}\");");
                        x.AppendLine($"field.ContentItem = value?.GetContentItem();");
                    });
                });
            }
        }
        else if (isDirectAssetField)
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get");
                x.AppendBlock(x =>
                {
                    x.AppendLine($"AssetField field = _contentItem.GetField<AssetField>(\"{property.PropertyName}\");");               
                    x.AppendLine($"return field.Asset;");
                });

                x.AppendLine($"set");
                x.AppendBlock(x =>
                {
                    x.AppendLine($"AssetField field = _contentItem.GetField<AssetField>(\"{property.PropertyName}\");");
                    x.AppendLine($"field.Asset = value;");
                });
            });
        }
        //ContentField
        else
        {
            builder.AppendBlock(x =>
            {
                x.AppendLine($"get => _contentItem.GetField<{propertyType}>(\"{property.PropertyName}\");");
                x.AppendLine($"set => _contentItem.SetField(\"{property.PropertyName}\", value);");
            });
        }

        return builder;
    }

    public static SourceBuilder AddLambdaProperty(this SourceBuilder builder, Modifier modifier, TypeElement type, string name, string? value = null, bool isStatic = false)
    {
        builder.AppendTabs();

        builder.Append($"{modifier} ");

        if (isStatic)
        {
            builder.Append("static ");
        }

        if (string.IsNullOrEmpty(value) == false)
        {
            builder.Append($"{type} {name} => {value};");
        }

        builder.AppendLineBreak();

        return builder;
    }

    public static SourceBuilder AddGetProperty(this SourceBuilder builder, Modifier modifier, TypeElement type, string name, string? value = null, bool isStatic = false)
    {
        builder.AppendTabs();
        builder.Append($"{modifier} ");

        if (isStatic)
        {
            builder.Append("static ");
        }

        builder.Append($"{type} {name} {{ get; }}");

        if (string.IsNullOrEmpty(value) == false)
        {
            builder.Append($" = {value};");
        }

        builder.AppendLineBreak();

        return builder;
    }

    public static SourceBuilder AddContentMetadataCreateSchema(this SourceBuilder builder, string className, string? contentAttributeParameters, IEnumerable<ContentItemProperty> properties)
    {
        builder.AppendLine($"private static ContentSchema CreateSchema()");
        builder.AppendBlock(x =>
        {
            x.AppendLine($"ContentSchema schema = new ContentSchema(\"{className}\");");
            x.AppendLine();

            if (contentAttributeParameters != null)
            {
                contentAttributeParameters = $" {{ {contentAttributeParameters} }}";
            }

            x.AppendLine($"new DragonFly.Generator.ContentItemAttribute(){contentAttributeParameters}.ApplyToSchema(schema);");
            
            x.AppendLine();

            foreach (ContentItemProperty property in properties)
            {
                string? parameters = null;

                if (string.IsNullOrEmpty(property.AttributeParameters) == false)
                {
                    parameters = $" {{ {property.AttributeParameters} }}";
                }

                x.AppendLine($"new {property.AttributeTypeSymbol.ToDisplayString()}(){parameters}.ApplyToSchema(schema, \"{property.PropertyName}\");");
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

        return builder;
    }
}
