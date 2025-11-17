using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DragonFly.Generator;

[Generator]
public class FieldGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classProvider = context.SyntaxProvider
                                   .ForAttributeWithMetadataName("DragonFly.Generator.FieldAttribute",
                                        predicate: static (node, _) => node is ClassDeclarationSyntax syntax,
                                        transform: static (ctx, _) => ctx);
        
        context.RegisterSourceOutput(classProvider, (ctx, ar) =>
        {
            ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)ar.TargetNode;
           
            INamedTypeSymbol? classSymbol = ar.SemanticModel.GetDeclaredSymbol(classSyntax);

            if (classSymbol == null)
            {
                return;
            }

            string ns = classSymbol.ContainingNamespace.ToDisplayString();
            string className = classSymbol.Name;

            List<string> baseTypes = new List<string>();

            if (classSymbol.BaseType?.ToDisplayString() == "object")
            {
                baseTypes.Add("ContentField");
            }

            baseTypes.Add("IContentFieldFactory");

            string? optionsParameter = classSyntax.GetFirstAttributeParameters("FieldOptions").FirstOrDefault();
            string optionsFactory = "null";
            string optionsType = "null";

            if (optionsParameter != null)
            {
                optionsFactory = $"() => new {optionsParameter}()";
                optionsType = $"typeof({optionsParameter})";
            }

            string? queryParameter = classSyntax.GetFirstAttributeParameters("FieldQuery").FirstOrDefault();
            string queryFactory = "null";
            string queryType = "null";

            if (queryParameter != null)
            {
                queryFactory = $"() => new {queryParameter}()";
                queryType = $"typeof({queryParameter})";
            }

            SourceBuilder builder = new SourceBuilder();
            builder.AppendPreprocessorDirectives();
            builder.AddUsings("DragonFly");
            builder.AddNamespace(ns, x =>
            {                
                x.AddClass(Modifier.From(classSymbol.DeclaredAccessibility), className, x =>
                {
                    x.AppendLine("public static FieldFactory Factory { get; } = CreateFactory();");

                    x.AppendLine("private static FieldFactory CreateFactory()");
                    x.AppendBlock(x =>
                    {
                        x.AppendLine($"return new FieldFactory(\"{className}\", typeof({className}), {optionsType}, {queryType}, () => new {className}(), {optionsFactory}, {queryFactory});");
                    });
                    
                },
                isPartial: true,
                isSealed: true,
                baseTypes: baseTypes);
            }, useFileScope: true);

            ctx.AddSource($"{ns}.{className}.g.cs", builder.ToString());
        });
    }
}
