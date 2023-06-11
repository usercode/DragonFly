// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DragonFly.Proxy.Tests;

public class GeneratorTest
{
    [Fact]
    public void Test()
    {
        CSharpCompilation compilation = CSharpCompilation.Create(assemblyName: "DragonFly.Generator");
        ContentGenerator generator = new ContentGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        
    }
}
