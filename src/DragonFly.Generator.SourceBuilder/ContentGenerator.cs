﻿using System.Collections.Immutable;
using DragonFly.Generator;
using DragonFly.Generator.Source;
using DragonFly.Generator.SourceBuilder;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DragonFly;

[Generator]
public class ContentGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classProvider = context.SyntaxProvider
                                   .CreateSyntaxProvider((node, _) =>
                                   {
                                       return node is ClassDeclarationSyntax syntax && syntax.AttributeLists.Count > 0;
                                   },
                                   (ctx, _) =>
                                   {
                                       return new { GeneratorContext = ctx, ClassSyntax = (ClassDeclarationSyntax)ctx.Node };
                                   });

        var r = classProvider.Collect();

        context.RegisterSourceOutput(r, (ctx, ar) =>
        {
            List<ClassItem> models = new List<ClassItem>();

            foreach (var item in ar)
            {
                var namespaceDeclaration = item.ClassSyntax.Parent as BaseNamespaceDeclarationSyntax;

                if (namespaceDeclaration == null)
                {
                    return;
                }

                if (item.ClassSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == "ContentItem") == false)
                {
                    return;
                }

                string ns = namespaceDeclaration.Name.ToString();
                string className = item.ClassSyntax.Identifier.Text;
                string classFields = $"{className}Fields";
                string classQuery = $"ContentQuery<{className}>";

                SourceBuilder builder = new SourceBuilder();

                List<ContentItemProperty> properties = new List<ContentItemProperty>();

                List<string> usings = new List<string>()
                {
                    "DragonFly",
                    "DragonFly.Generator"
                };

                //properties of contentitems
                foreach (var field in item.ClassSyntax.Members.OfType<FieldDeclarationSyntax>().Where(x => x.AttributeLists.Count > 0))
                {
                    string attributeName = field.AttributeLists[0].Attributes[0].Name.ToString();
                    string attributeParameters = field.AttributeLists[0].Attributes[0].ArgumentList?.Arguments.ToString() ?? string.Empty;

                    TypeInfo propertyTypeSymbol = item.GeneratorContext.SemanticModel.GetTypeInfo(field.Declaration.Type);

                    string fieldName = field.Declaration.Variables[0].Identifier.Text;
                    string propertyName = fieldName.TrimStart('_').FirstCharToUpper();
                    string propertyType = field.Declaration.Type.ToString();
                    bool isSingleValue = propertyTypeSymbol.Type?.IsValueType == true || propertyTypeSymbol.Type.Name == "String";
                    string propertyTypeNamespace = propertyTypeSymbol.Type.ContainingNamespace.ToString();

                    properties.Add(new ContentItemProperty()
                    {
                        AttributeName = attributeName,
                        AttributeParameters = attributeParameters,
                        PropertyName = propertyName,
                        PropertyType = propertyType,
                        IsSingleValue = isSingleValue
                    });

                    usings.Add(propertyTypeNamespace);
                }

                models.Add(new ClassItem(ns, className, new string[0]));

                builder.AddUsings(usings.ToArray());
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

                        x.AddContentMetadataCreateSchema(className, properties);
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
                        x.AddExtensionMethod(Modifier.Public, $"To{className}", className, new Parameter("contentItem", "ContentItem"), x =>
                        {
                            x.AppendLine($"return new {className}(contentItem);");
                        });

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

                        x.AppendLine($"public static {classQuery} Integer(this {classQuery} query, Func<{classFields}, string> field, int? value, int? minValue = null, int? maxValue = null)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.Integer(field({classFields}.Default), value, minValue, maxValue);");
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

                        x.AppendLine($"public static {classQuery} String(this {classQuery} query, Func<{classFields}, string> field, string pattern, StringQueryType type = StringQueryType.Equals)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine($"return query.String(field({classFields}.Default), pattern, type);");
                        });

                    },
                    isStatic: true);
                }, useFileScope: true);

                ctx.AddSource($"{ns}.{className}.g.cs", builder.ToString());
            }

            {
                SourceBuilder builder = new SourceBuilder();

                builder.AddUsings("System");
                builder.AddNamespace("DragonFly", x =>
                {
                    x.AddClass(Modifier.Public, "ContentExtensions", x =>
                    {
                        x.AppendLine("public static IContentModel ToModel(this ContentItem content)");
                        x.AppendBlock(x =>
                        {
                            x.AppendLine("return content.Schema.Name switch");
                            x.AppendBlock(x =>
                            {
                                foreach (ClassItem model in models.OrderBy(x => x.Name))
                                {
                                    x.AppendLine($"\"{model.Name}\" => new {model.Namespace}.{model.Name}(content),");
                                }
                                x.AppendLine("_ => throw new Exception(\"Unknown model.\")");
                            }, command: true);
                            
                        });
                    }, isStatic: true);
                }, useFileScope: true);

                ctx.AddSource("DragonFly.ContentExtensions.g.cs", builder.ToString());
            }
        });
    }
}