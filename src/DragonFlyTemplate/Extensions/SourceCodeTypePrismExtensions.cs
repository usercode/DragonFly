﻿using System;
using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class SourceCodeTypePrismExtensions
{
    public static string ToPrismCssClass(this CodeType type)
    {
        string language = type switch
        {
            CodeType.CSharp => "csharp",
            CodeType.CSS => "css",
            CodeType.JavaScript => "javascript",
            CodeType.HTML => "html",
            CodeType.XML => "xml",
            CodeType.SQL => "sql",
            CodeType.SVG => "svg",
            _ => string.Empty
        };

        return $"language-{language}";
    }
}