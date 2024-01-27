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
                                   .CreateSyntaxProvider((node, _) =>
                                   {
                                       return node is ClassDeclarationSyntax syntax && syntax.AttributeLists.Count > 0;
                                   },
                                   (ctx, _) =>
                                   {
                                       return new { GeneratorContext = ctx, ClassSyntax = (ClassDeclarationSyntax)ctx.Node };
                                   });

        var r = classProvider;
        
        context.RegisterSourceOutput(r, (ctx, ar) =>
        {
            if (ar.ClassSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == "Field") == false)
            {
                return;
            }

            INamedTypeSymbol? classSymbol = ar.GeneratorContext.SemanticModel.GetDeclaredSymbol(ar.ClassSyntax);

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

            string? optionsParameter = ar.ClassSyntax.GetFirstAttributeParameters("FieldOptions").FirstOrDefault();
            string optionsFactory = "null";
            string optionsType = "null";

            if (optionsParameter != null)
            {
                optionsFactory = $"() => new {optionsParameter}()";
                optionsType = $"typeof({optionsParameter})";
            }

            string? queryParameter = ar.ClassSyntax.GetFirstAttributeParameters("FieldQuery").FirstOrDefault();
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
