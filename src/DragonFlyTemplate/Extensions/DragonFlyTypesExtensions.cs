// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class DragonFlyTypesExtensions
{
    public static string ToBootstrapCssClass(this ColorType colorType)
    {
        return colorType switch
        {
            ColorType.Primary => "primary",
            ColorType.Secondary => "secondary",
            ColorType.Success => "success",
            ColorType.Danger => "danger",
            ColorType.Warning => "warning",
            ColorType.Info => "info",
            ColorType.Light => "light",
            ColorType.Dark => "dark",
            _ => null
        };
    }

    public static string ToBootstrapCssClass(this ColumnWidth width)
    {
        return width switch
        {
            ColumnWidth.Max => "col-lg",
            ColumnWidth.Auto => "col-lg-auto",
            _ => $"col-lg-{(int)width}"
        };
    }

    public static string ToBootstrapCssClass(this HorizontalAlignment alignment)
    {
        return alignment switch
        {
            HorizontalAlignment.Start => null, // "justify-content-start",
            HorizontalAlignment.Center => "justify-content-center",
            HorizontalAlignment.End => "justify-content-end",
            HorizontalAlignment.Around => "justify-content-around",
            HorizontalAlignment.Between => "justify-content-between",
            HorizontalAlignment.Evenly => "justify-content-evenly",
            _ => null
        };
    }

    public static string ToBootstrapCss(this TextAlignment alignment)
    {
        return alignment switch
        {
            TextAlignment.Left => null, // "text-start",
            TextAlignment.Center => "text-center",
            TextAlignment.Right => "text-end",
            _ => null
        };
    }

    public static string ToPrismCssClass(this CodeType type)
    {
        string language = type switch
        {
            CodeType.CSharp => "csharp",
            CodeType.CSS => "css",
            CodeType.JavaScript => "javascript",
            CodeType.HTML => "html",
            CodeType.HLSL => "hlsl",
            CodeType.XML => "xml",
            CodeType.SQL => "sql",
            CodeType.SVG => "svg",
            _ => null
        };

        return $"language-{language}";
    }
}
