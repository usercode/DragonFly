using System;
using DragonFly.Fields.BlockField;

namespace DragonFly.AspNetCore;

public static class SourceCodeTypePrismExtensions
{
    public static string ToPrismCssClass(this SourceCodeType type)
    {
        string language = type switch
        {
            SourceCodeType.CSharp => "csharp",
            SourceCodeType.CSS => "css",
            SourceCodeType.JavaScript => "javascript",
            SourceCodeType.HTML => "html",
            SourceCodeType.XML => "xml",
            SourceCodeType.SQL => "sql",
            SourceCodeType.SVG => "svg",
            _ => string.Empty
        };

        return $"language-{language}";
    }
}
