// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DragonFly.Generator;

public static class SyntaxExtensions
{
    public static string GetString(this ExpressionSyntax expression)
    {
        if (expression is LiteralExpressionSyntax literal)
        {
            return literal.Token.Text;
        }
        else if (expression is InvocationExpressionSyntax invocation) //nameof(..)
        {
            if (invocation.ArgumentList.Arguments[0].Expression is IdentifierNameSyntax identifier)
            {
                return identifier.Identifier.ValueText;
            }
        }
        else if (expression is TypeOfExpressionSyntax typeOfExpressionSyntax)
        {
            return typeOfExpressionSyntax.Type.ToString();
        }
        else
        {
            return expression.ToString();
        }

        return string.Empty;
    }

    public static string[] GetFirstAttributeParameters(this TypeDeclarationSyntax typeSyntax, string attributeName)
    {
        return typeSyntax.AttributeLists
                                                  .SelectMany(x => x.Attributes)
                                                  .Where(x => x.Name.ToString() == attributeName)
                                                  .Select(x => x.ArgumentList?.Arguments[0].Expression.GetString())
                                                  .Where(x => x != null)
                                                  .Cast<string>()
                                                  .ToArray();
    }
}
