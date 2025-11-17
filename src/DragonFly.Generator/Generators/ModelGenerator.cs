using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DragonFly.Generator;

[Generator]
public class ModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
                                   .ForAttributeWithMetadataName("DragonFly.Generator.ContentItemAttribute",
                                        predicate: static (node, _) =>  node is ClassDeclarationSyntax syntax,
                                        transform: static (ctx, _) => ctx)
                                   .Collect();

        context.RegisterSourceOutput(classDeclarations, (ctx, ar) =>
        {
            List<ClassItem> models = new List<ClassItem>();

            foreach (var item in ar)
            {
                ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)item.TargetNode;

                INamedTypeSymbol? classSymbol = item.SemanticModel.GetDeclaredSymbol(classSyntax);

                if (classSymbol == null)
                {
                    continue;
                }

                string ns = classSymbol.ContainingNamespace.ToDisplayString();
                string className = classSymbol.Name;
                string classFields = $"{className}Fields";
                string classQuery = $"ContentQuery<{className}>";

                SourceBuilder builder = new SourceBuilder();
                builder.AppendPreprocessorDirectives();

                List<ContentItemProperty> properties = new List<ContentItemProperty>();

                string? contentAttributeParameters = classSyntax.AttributeLists[0].Attributes[0].ArgumentList?.Arguments.ToString();

                //properties of contentitems
                foreach (var field in classSyntax.Members.OfType<PropertyDeclarationSyntax>().Where(x => x.AttributeLists.Count > 0))
                {
                    TypeInfo attributeTypeInfo = item.SemanticModel.GetTypeInfo(field.AttributeLists[0].Attributes[0]);

                    if (attributeTypeInfo.Type == null)
                    {
                        continue;
                    }

                    TypeInfo propertyTypeInfo = item.SemanticModel.GetTypeInfo(field.Type);

                    if (propertyTypeInfo.Type == null)
                    {
                        continue;
                    }

                    string attributeParameters = field.AttributeLists[0].Attributes[0].ArgumentList?.Arguments.ToString() ?? string.Empty;

                    string propertyName = field.Identifier.ValueText;

                    properties.Add(new ContentItemProperty()
                    {
                        AttributeParameters = attributeParameters,
                        PropertyName = propertyName,
                        AttributeTypeSymbol = attributeTypeInfo.Type,
                        PropertyTypeSymbol = propertyTypeInfo.Type
                    });
                }

                models.Add(new ClassItem(ns, className));

                builder.AddUsings("System", "DragonFly");
                builder.AddNamespace(ns, x =>
                {
                    x.AddClass(Modifier.Public, className, x =>
                    {
                        x.AddConstructor(Modifier.Public, className, ParameterList.New(), x =>
                        {
                            x.AppendLine("_contentItem = Schema.CreateContent();");
                        });
                        x.AddConstructor(Modifier.Public, className, ParameterList.New().Add("ContentItem", "contentItem"), x =>
                        {
                            x.AppendLine($"if (contentItem.Schema.Name != \"{className}\")");
                            x.AppendBlock(x =>
                            {
                                x.AppendLine($"throw new Exception(\"The content item isn't compatible with the model '{className}'.\");");
                            });
                            x.AppendLine("_contentItem = contentItem;");
                        });
                        x.AddField(Modifier.Private, "ContentItem", "_contentItem", isReadOnly: true);
                        x.AddLambdaProperty(Modifier.Public, TypeElement.Guid, "Id", "_contentItem.Id");
                        x.AddLambdaMethod(Modifier.Public, TypeElement.Get("ContentItem"), "GetContentItem", ParameterList.New(), "_contentItem");

                        foreach (ContentItemProperty property in properties)
                        {
                            x.AddContentProperty(property);
                        }

                        //Equals
                        x.AppendLine("public override bool Equals(object? obj)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"if (obj is {className} other)");
                            x.AppendBlock(x =>
                            {
                                x.AppendLine("return Id == other.Id;");
                            });
                            x.AppendLine();
                            x.AppendLine("return false;");
                        });

                        //GetHashCode
                        x.AppendLine("public override int GetHashCode()");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return HashCode.Combine(typeof({className}), Id);");
                        });

                        x.AddGetProperty(Modifier.Public, TypeElement.Get("ContentSchema"), "Schema", "CreateSchema()", isStatic: true);
                        x.AddGetProperty(Modifier.Public, TypeElement.Get(classFields), "Fields", $"{classFields}.Default", isStatic: true);

                        x.AddLambdaMethod(Modifier.Public, TypeElement.Get(className), "Create", ParameterList.New().Add("ContentItem", "contentItem"), $"new {className}(contentItem)", isStatic: true);

                        x.AppendLine($"static IContentModel IContentModel.Create(ContentItem contentItem) => Create(contentItem);");

                        x.AddContentMetadataCreateSchema(className, contentAttributeParameters, properties);
                    },
                    isPartial: true,
                    isSealed: true,
                    baseTypes: new[] { "IContentModel" });

                    x.AddClass(Modifier.Public, classFields, x =>
                    {
                        x.AddConstructor(Modifier.Private, classFields, ParameterList.New(), x => { });

                        x.AddGetProperty(Modifier.Public, TypeElement.Get(classFields), "Default", $"new {className}Fields()", isStatic: true);

                        foreach (ContentItemProperty property in properties)
                        {
                            x.AddLambdaProperty(Modifier.Public, TypeElement.String, property.PropertyName, $"\"{property.PropertyName}\"");
                        }
                    },
                    isSealed: true);

                    x.AddClass(Modifier.Public, $"{className}Extensions", x =>
                    {
                        x.AppendLine($"public static {classQuery} OrderBy(this {classQuery} query, Func<{classFields}, string> field, bool asc = true)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"query.OrderFields.Add(new FieldOrder(field({classFields}.Default), asc));");
                            x.AppendLine("return query;");
                        });

                        x.AppendLine($"public static {classQuery} Asset(this {classQuery} query, Func<{classFields}, string> field, Guid? id)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Asset(field({classFields}.Default), id);");
                        });

                        x.AppendLine($"public static {classQuery} Bool(this {classQuery} query, Func<{classFields}, string> field, bool? value)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Bool(field({classFields}.Default), value);");
                        });

                        x.AppendLine($"public static {classQuery} Integer(this {classQuery} query, Func<{classFields}, string> field, long? value, NumberQueryType type = NumberQueryType.Equal)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Integer(field({classFields}.Default), value, type);");
                        });

                        x.AppendLine($"public static {classQuery} Float(this {classQuery} query, Func<{classFields}, string> field, double? value, NumberQueryType type = NumberQueryType.Equal)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Float(field({classFields}.Default), value, type);");
                        });

                        x.AppendLine($"public static {classQuery} Reference(this {classQuery} query, Func<{classFields}, string> field, Guid? id)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Reference(field({classFields}.Default), id);");
                        });

                        x.AppendLine($"public static {classQuery} Slug(this {classQuery} query, Func<{classFields}, string> field, string value)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Slug(field({classFields}.Default), value);");
                        });

                        x.AppendLine($"public static {classQuery} String(this {classQuery} query, Func<{classFields}, string> field, string pattern, StringQueryType type = StringQueryType.Equal)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.String(field({classFields}.Default), pattern, type);");
                        });

                        x.AppendLine($"public static {classQuery} Geolocation(this {classQuery} query, Func<{classFields}, string> field, double longitude, double latitude, double? minDistance = null, double? maxDistance = null)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Geolocation(field({classFields}.Default), longitude, latitude, minDistance, maxDistance);");
                        });
                    },
                    isStatic: true);
                }, useFileScope: true);

                ctx.AddSource($"{ns}.{className}.g.cs", builder.ToString());
            }

            {
                if (models.Count > 0)
                {
                    models = models.OrderBy(x => x.Name).ToList();

                    SourceBuilder builder = new SourceBuilder();

                    builder.AddUsings("System");
                    builder.AddNamespace("DragonFly", x =>
                    {
                        x.AddClass(Modifier.Public, "ContentExtensions", x =>
                        {
                            x.AppendLine("/// <summary>");
                            x.AppendLine("/// Decorates automatically the content item based on the schema.");
                            x.AppendLine("/// <br /><br />");
                            x.AppendLine("/// Available models:<br />");
                            foreach (ClassItem model in models)
                            {
                                x.AppendLine($"/// <see cref=\"{model}\"/><br />");
                            }
                            x.AppendLine("/// </summary>");                           
                            x.AppendLine("public static IContentModel ToModel(this ContentItem content)");
                            x.AppendBlock(x =>
                            {
                                x.AppendLine("return content.Schema.Name switch");
                                x.AppendBlock(x =>
                                {
                                    foreach (ClassItem model in models)
                                    {
                                        x.AppendLine($"\"{model.Name}\" => new {model.Namespace}.{model.Name}(content),");
                                    }
                                    x.AppendLine("_ => throw new Exception($\"Unknown model for '{content.Schema.Name}'.\")");
                                }, command: true);

                            });
                        }, isStatic: true);
                    }, useFileScope: true);

                    ctx.AddSource("DragonFly.ContentExtensions.g.cs", builder.ToString());
                }
            }
        });
    }
}
