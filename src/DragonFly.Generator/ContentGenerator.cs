using System.Xml;
using DragonFly;
using DragonFly.Generator;
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
                                       return ctx;
                                   });
        
        context.RegisterSourceOutput(classProvider, Generate);
    }

    private static void Generate(SourceProductionContext ctx, GeneratorSyntaxContext g)
    {
        var namespaceDeclaration = g.Node.Parent as BaseNamespaceDeclarationSyntax;

        if (namespaceDeclaration == null)
        {
            return;
        }

        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)g.Node;

        //try
        //{
        //    var r22 = g.SemanticModel.GetConstantValue(classSyntax.AttributeLists[0].Attributes[0].ArgumentList?.Arguments[0].Expression);

        //    string fieldAttribute = classSyntax.AttributeLists[0].Attributes[0].Name.ToString();
        //    var arg = classSyntax.AttributeLists[0].Attributes[0].ArgumentList?.Arguments.ToString();
        //}
        //catch
        //{

        //}

        if (classSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == "ContentItem") == false)
        {
            return;
        }

        string ns = namespaceDeclaration.Name.ToString();
        string className = classSyntax.Identifier.Text;

        SourceBuilder builder = new SourceBuilder();

        List<ContentItemProperty> properties = new List<ContentItemProperty>();
        
        //properties of contentitems
        foreach (var field in classSyntax.Members.OfType<FieldDeclarationSyntax>().Where(x=> x.AttributeLists.Count > 0))
        {
            string attributeName = field.AttributeLists[0].Attributes[0].Name.ToString();
            string attributeParameters = field.AttributeLists[0].Attributes[0].ArgumentList?.Arguments.ToString() ?? string.Empty;

            string fieldName = field.Declaration.Variables[0].Identifier.Text;
            string propertyName = fieldName.TrimStart('_').FirstCharToUpper();
            string propertyType = field.Declaration.Type.ToString();
            bool isSingleValue = false;

            var propertyTypeSymbol = g.SemanticModel.GetTypeInfo(field.Declaration.Type);

            if (propertyTypeSymbol.Type?.IsValueType == true || propertyTypeSymbol.Type?.Name == "String")
            {
                isSingleValue = true;
            }

            properties.Add(new ContentItemProperty() 
            { 
                AttributeName = attributeName,
                AttributeParameters = attributeParameters,
                PropertyName = propertyName, 
                PropertyType = propertyType, 
                IsSingleValue = isSingleValue });
        }

        builder.AddUsings("DragonFly", "DragonFly.Generator");
        builder.AddNamespace(ns, x =>
        {
            x.AddClass(Modifier.Public, className, x => 
            {
                x.AppendLine($"static IContentMetadata IContentModel.Metadata => Metadata;");
                x.AppendLine($"public static {className}Metadata Metadata => new {className}Metadata();");

                x.AddConstructor(Modifier.Public, className, ParameterList.New.Add("ContentItem", "contentItem"), x =>
                {
                    x.AppendLine("_contentItem = contentItem;");
                });
                x.AddField(Modifier.Private, "ContentItem", "_contentItem", isReadOnly: true);
                x.AddlambdaProperty(Modifier.Public, TypeElement.Guid, "Id", "_contentItem.Id");
                x.AddLambdaMethod(Modifier.Public, TypeElement.Get("ContentItem"), "GetContentItem", ParameterList.New, "_contentItem");

                foreach (ContentItemProperty property in properties)
                {
                    x.AddContentProperty(property);
                }
            },
            isPartial: true,
            isSealed: true,
            baseTypes: new[] { "IContentModel" });

            x.AddClass(Modifier.Public, $"{className}Metadata", x =>
            {
                x.AddlambdaProperty(Modifier.Public, TypeElement.String, "ModelName", $"\"{className}\"");

                foreach (ContentItemProperty property in properties)
                {
                    x.AddlambdaProperty(Modifier.Public, TypeElement.String, property.PropertyName, $"\"{property.PropertyName}\"");
                }

                x.AddLambdaMethod(Modifier.Public, TypeElement.Get(className), "CreateModel", ParameterList.New.Add("ContentItem", "contentItem"), $"new {className}(contentItem)");

                x.AppendLine($"IContentModel IContentMetadata.CreateModel(ContentItem contentItem) => CreateModel(contentItem);");

                x.AddContentMetadataCreateSchema(className, properties);

            },
            isSealed: true,
            baseTypes: new[] { "IContentMetadata" });

            x.AddClass(Modifier.Public, $"{className}Extensions", x =>
            {
                x.AddExtensionMethod(Modifier.Public, $"To{className}", className, new Parameter("contentItem","ContentItem"), x =>
                {
                    x.AppendLine($"return {className}.Metadata.CreateModel(contentItem);");
                });

            }, 
            isStatic: true);
        });
        ctx.AddSource($"{ns}.{className}.cs", builder.ToString());
    }
}

