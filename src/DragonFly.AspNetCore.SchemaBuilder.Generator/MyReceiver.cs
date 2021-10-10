using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFly.AspNetCore.SchemaBuilder.Generator
{
    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<IFieldSymbol> Fields { get; } = new List<IFieldSymbol>();

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        public void OnVisitSyntaxNode(SyntaxNode node)
        {
            // any field with at least one attribute is a candidate for property generation
            if (node is FieldDeclarationSyntax fieldDeclarationSyntax && fieldDeclarationSyntax.AttributeLists.Count > 0)
            {
                foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
                {
                    //// Get the symbol being declared by the field, and keep it if its annotated
                    //IFieldSymbol fieldSymbol = node.SemanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                    //if (fieldSymbol.GetAttributes().Any(ad => ad.AttributeClass.ToDisplayString() == "AutoNotify.AutoNotifyAttribute"))
                    //{
                    //    Fields.Add(fieldSymbol);
                    //}
                }
            }
        }
    }
}
