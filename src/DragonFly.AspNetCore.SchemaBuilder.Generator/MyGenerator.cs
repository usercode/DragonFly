using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.SchemaBuilder.Generator
{
    [Generator]
    public class MyGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
                using System;
                namespace HelloWorldGenerated
                {
                    public static class HelloWorld
                    {
                        public static void SayHello() 
                        {
                            Console.WriteLine(""Hello from generated code!"");
                            Console.WriteLine(""The following syntax trees existed in the compilation that created this program:"");
                ");

                            // using the context, get a list of syntax trees in the users compilation
                            var syntaxTrees = context.Compilation.SyntaxTrees;

                            // add the filepath of each tree to the class we're building
                            foreach (SyntaxTree tree in syntaxTrees)
                            {
                                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - {tree.FilePath}"");");
                            }

                            // finish creating the source to inject
                            sourceBuilder.Append(@"
                        }
                    }
                }");

            context.AddSource("generatedSource.cs", sourceBuilder.ToString());
        }    

        public void Initialize(GeneratorInitializationContext context)
        {
            
        }
    }
}
