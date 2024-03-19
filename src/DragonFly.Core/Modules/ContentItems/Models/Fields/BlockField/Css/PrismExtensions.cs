// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class PrismExtensions
{
    public static string? ToPrismClass(this CodeType type)
    {
        return ((CodeType?)type).ToPrismClass();
    }

    public static string? ToPrismClass(this CodeType? type)
    {
        string? language = type switch
        {
            CodeType.CSharp => "csharp",
            CodeType.Bash => "bash",
            CodeType.CSS => "css",
            CodeType.CSV => "csv",
            CodeType.Java => "java",
            CodeType.JavaScript => "javascript",
            CodeType.JSON => "json",
            CodeType.HTML => "markup",
            CodeType.HTTP => "http",
            CodeType.HLSL => "hlsl",
            CodeType.GLSL => "glsl",
            CodeType.GraphQL => "graphql",
            CodeType.Ini => "ini",
            CodeType.Markdown => "markdown",
            CodeType.PHP => "php",
            CodeType.Rust => "rust",
            CodeType.Regex => "regex",
            CodeType.XML => "markup",
            CodeType.SQL => "sql",
            CodeType.SVG => "svg",
            CodeType.TypeScript => "typescript",
            CodeType.Docker => "docker",
            CodeType.Yaml => "yaml",
            CodeType.WebAssembly => "wasm",
            _ => null
        };

        if (language == null)
        {
            return null;
        }

        return $"language-{language}";
    }
}
