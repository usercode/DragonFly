// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public static class PrismExtensions
{
    public static string? ToPrismCssClass(this CodeType type)
    {
        return ((CodeType?)type).ToPrismCssClass();
    }

    public static string? ToPrismCssClass(this CodeType? type)
    {
        string? language = type switch
        {
            CodeType.CSharp => "csharp",
            CodeType.CSS => "css",
            CodeType.Java => "java",
            CodeType.JavaScript => "javascript",
            CodeType.JSON => "json",
            CodeType.HTML => "html",
            CodeType.HLSL => "hlsl",
            CodeType.XML => "xml",
            CodeType.SQL => "sql",
            CodeType.SVG => "svg",
            CodeType.Docker => "docker",
            _ => null
        };

        if (language == null)
        {
            return null;
        }

        return $"language-{language}";
    }
}
