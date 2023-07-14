using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DragonFly.Generator;

[Generator]
public class ProxyGenerator : IIncrementalGenerator
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
            if (ar.ClassSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == "Proxy") == false)
            {
                return;
            }

            INamedTypeSymbol? classSymbol = ar.GeneratorContext.SemanticModel.GetDeclaredSymbol(ar.ClassSyntax);

            if (classSymbol == null)
            {
                return;
            }

            INamedTypeSymbol? baseClassSymbol = classSymbol.BaseType;

            if (baseClassSymbol == null)
            {
                return;
            }           

            string ns = classSymbol.ContainingNamespace.ToDisplayString();
            string className = classSymbol.Name;

            SourceBuilder builder = new SourceBuilder();
            builder.AppendNullableDirective();
            builder.AddNamespace(ns, x =>
            {
                IEnumerable<INamedTypeSymbol> baseTypes = classSymbol.BaseType != null
                                                                             ? GetBaseTypes(classSymbol.BaseType)
                                                                             : Array.Empty<INamedTypeSymbol>();
                
                x.AddClass(Modifier.From(classSymbol.DeclaredAccessibility), className, x =>
                {
                    IMethodSymbol constructor = baseClassSymbol.Constructors.OrderByDescending(x => x.Parameters.Length).First();

                    //constructor
                    string parametersDef = string.Join(", ", constructor.Parameters.Select(x => $"{x.Type.ToDisplayString()} {x.Name}"));
                    string parameters = string.Join(", ", constructor.Parameters.Select(x => $"{x.Name}"));

                    builder.AppendLine($"public {className}({parametersDef})");
                    builder.AppendLine($"    : base({parameters})");
                    builder.AppendBlock(x =>
                    {
                        x.AppendLine($"_invocationTarget = new {baseClassSymbol.ToDisplayString()}({parameters});");
                    });                    

                    //InvocationTarget
                    builder.AddField(Modifier.Private, baseClassSymbol.ToDisplayString(), "_invocationTarget");

                    builder.AddMethod("SetInvocationTarget", TypeElement.Void, Modifier.Private, ParameterList.New().Add(baseClassSymbol.ToDisplayString(), "obj"), x =>
                    {
                        x.AppendLine($"_invocationTarget = obj;");
                    });

                    if (FindMethodWithAttribute(ar.ClassSyntax, "Intercept", out string? methodCaller, out bool isTask, out string[] ignoredProperties, out string[] ignoredMethods))
                    {
                        string? methodCallerAsyncExtensions = null;

                        if (isTask)
                        {
                            methodCallerAsyncExtensions = ".GetAwaiter().GetResult()";
                        }

                        // Process each base type and its members
                        foreach (INamedTypeSymbol baseType in baseTypes)
                        {
                            IEnumerable<ISymbol> members = baseType.GetMembers();

                            // Process each member of the base type
                            foreach (ISymbol member in members)
                            {
                                if (member.IsVirtual)
                                {
                                    //override properties
                                    if (member is IPropertySymbol propertySymbol && ignoredProperties.Contains(propertySymbol.Name) == false)
                                    {
                                        builder.AppendLine($"public override {propertySymbol.Type.ToDisplayString()} {propertySymbol.Name}");
                                        builder.AppendBlock(x =>
                                        {
                                            if (propertySymbol.GetMethod != null)
                                            {
                                                builder.AppendLine($"get");
                                                builder.AppendBlock(x =>
                                                {
                                                    x.AppendLine($"{methodCaller}(\"{propertySymbol.GetMethod.Name}\"){methodCallerAsyncExtensions};");
                                                    x.AppendLine($"return _invocationTarget.{propertySymbol.Name};");
                                                });

                                            }
                                            if (propertySymbol.SetMethod != null)
                                            {
                                                builder.AppendLine($"set");
                                                builder.AppendBlock(x =>
                                                {
                                                    x.AppendLine($"{methodCaller}(\"{propertySymbol.SetMethod.Name}\"){methodCallerAsyncExtensions};");
                                                    x.AppendLine($"_invocationTarget.{propertySymbol.Name} = value;");
                                                });
                                            }
                                        });
                                    }

                                    //override methods
                                    if (member is IMethodSymbol methodSymbol
                                                                        && ignoredMethods.Contains(methodSymbol.Name) == false
                                                                        && methodSymbol.MethodKind != MethodKind.PropertyGet
                                                                        && methodSymbol.MethodKind != MethodKind.PropertySet)
                                    {
                                        string parameterDefinition = $"{string.Join(", ", methodSymbol.Parameters.Select(x => $"{x.Type.ToDisplayString()} {x.Name}"))}";

                                        builder.AppendTabs();
                                        builder.Append($"public override {methodSymbol.ReturnType.OriginalDefinition.ToDisplayString()} {methodSymbol.Name}({parameterDefinition})");
                                        builder.AppendLineBreak();
                                        builder.AppendBlock(x =>
                                        {
                                            builder.AppendLine($"{methodCaller}(\"{methodSymbol.Name}\"){methodCallerAsyncExtensions};");

                                            builder.AppendTabs();

                                            if (methodSymbol.ReturnsVoid == false)
                                            {
                                                builder.Append($"return ");
                                            }

                                            builder.Append($"_invocationTarget.{methodSymbol.Name}({string.Join(", ", methodSymbol.Parameters.Select(x => x.Name))});");

                                            builder.AppendLineBreak();
                                        });
                                    }
                                }
                            }
                        }
                    }
                },
                isPartial: true,
                isSealed: true,
                baseTypes: new[] { "DragonFly.Generator.IProxyObject" });
            }, useFileScope: true);

            ctx.AddSource($"{ns}.{className}.g.cs", builder.ToString());
        });
    }

    private IEnumerable<INamedTypeSymbol> GetBaseTypes(INamedTypeSymbol? typeSymbol)
    {
        // Return all base types of the given type
        while (typeSymbol != null)
        {
            yield return typeSymbol;
            typeSymbol = typeSymbol.BaseType;
        }
    }

    private bool FindMethodWithAttribute(ClassDeclarationSyntax classDeclaration, string attributeName, out string? method, out bool isTask, out string[] ignoredProperties, out string[] ignoredMethods)
    {
        foreach (MemberDeclarationSyntax memberSyntax in classDeclaration.Members)
        {
            if (memberSyntax is MethodDeclarationSyntax methodDeclaration)
            {
                if (memberSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == attributeName))
                {
                    ignoredProperties = memberSyntax.AttributeLists
                                                    .SelectMany(x => x.Attributes)
                                                    .Where(x => x.Name.ToString() == "IgnoreProperty")
                                                    .Select(x => x.ArgumentList.Arguments[0].Expression.GetString())
                                                    .ToArray();

                    ignoredMethods = memberSyntax.AttributeLists
                                                    .SelectMany(x => x.Attributes)
                                                    .Where(x => x.Name.ToString() == "IgnoreMethod")
                                                    .Select(x => x.ArgumentList.Arguments[0].Expression.GetString())
                                                    .ToArray();

                    method = methodDeclaration.Identifier.Text.ToString();

                    //async method?
                    if (methodDeclaration.ReturnType is IdentifierNameSyntax nameSyntax)
                    {
                        isTask = nameSyntax.Identifier.Text == nameof(Task) || nameSyntax.Identifier.Text == nameof(ValueTask);
                    }
                    else
                    {
                        isTask = false;
                    }

                    return true;
                }
            }
        }

        method = null;
        ignoredProperties = Array.Empty<string>();
        ignoredMethods = Array.Empty<string>();
        isTask = false;

        return false;
    }
}
