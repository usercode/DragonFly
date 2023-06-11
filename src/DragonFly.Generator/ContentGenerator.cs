// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace DragonFly.Generator;

[Generator(LanguageNames.CSharp)]
public class ContentGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            ctx.AddSource("additionalFiles2.g.cs",
                """
                namespace Generated
                {
                    public class AdditionalTextList
                    {
                        public static void PrintTexts2()
                        {
                            System.Console.WriteLine(""Additional Texts were: ");
                        }
                    }
                }");
                """
            );
        });
    }
}
