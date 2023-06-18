using System.Xml;
using DragonFly;
using DragonFly.Generator;
using DragonFly.Generator.SourceBuilder;
using Microsoft.CodeAnalysis;
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
        //context.RegisterPostInitializationOutput(x => x.AddSource("DragonFly.ContentAttribute.g.cs", SourceGenerationHelper.Attribute));
    }

    private static void Generate(SourceProductionContext ctx, GeneratorSyntaxContext g)
    {
        var namespaceDeclaration = g.Node.Parent as BaseNamespaceDeclarationSyntax;

        if (namespaceDeclaration == null)
        {
            return;
        }

        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)g.Node;

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
            string fieldName = field.Declaration.Variables[0].Identifier.Text;
            string propertyName = fieldName.TrimStart('_').FirstCharToUpper();
            string propertyType = field.Declaration.Type.ToString();
            string contentFieldType = propertyType;
            bool isSingleValue = false;

            var propertyTypeSymbol = g.SemanticModel.GetTypeInfo(field.Declaration.Type);
            if (propertyTypeSymbol.Type.IsValueType || propertyTypeSymbol.Type.Name == "String")
            {
                isSingleValue = true;
            }

            properties.Add(new ContentItemProperty() { PropertyName = propertyName, FieldName = fieldName, PropertyType = propertyType, IsSingleValue = isSingleValue });
        }

        builder.AddUsings("DragonFly");
        builder.AddNamespace(ns, x =>
        {
            x.AddClass(Modifier.Public, className, x => 
            {
                x.AddStaticContentMetadataProperty(className);
                x.AddStaticContentMetadata2Property(className);

                x.AddContentConstructor(Modifier.Public, className);
                x.AddReadOnlyField(Modifier.Private, "ContentItem", "_contentItem");
                x.AddContentIdProperty();
                x.AddContentItemProperty();
                
                foreach (ContentItemProperty property in properties)
                {
                    x.AddContentProperty(property);
                }
            },
            isPartial : true, 
            isSealed : true,
            baseTypes : new[] { "IContentModel" });

            x.AddClass(Modifier.Public, $"{className}Metadata", x =>
            {
                x.AddContentMetadataModelNameProperty(className);

                foreach (ContentItemProperty property in properties)
                {
                    x.AddContentMetadataProperty("string", property.PropertyName, property.PropertyName);
                }

                x.AddContentMetadataCreate(className);
                x.AddContentMetadata2Create(className);

            },
            isSealed : true,
            baseTypes: new[] { "IContentMetadata" });

            x.AddClass(Modifier.Public, $"{className}Extensions", x =>
            {
                x.AddExtensionToModel(className);

            }, 
            isStatic: true);
        });
        ctx.AddSource($"{ns}.{className}.cs", builder.ToString());
    }
}

