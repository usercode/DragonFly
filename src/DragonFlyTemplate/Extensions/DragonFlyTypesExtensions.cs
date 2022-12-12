// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class DragonFlyTypesExtensions
{
    public static string ToBootstrapCssClass(this AlertType alertType)
    {
        return alertType switch
        {
            AlertType.Primary => "alert-primary",
            AlertType.Secondary => "alert alert-secondary",
            AlertType.Success => "alert-success",
            AlertType.Danger => "alert-danger",
            AlertType.Warning => "alert-warning",
            AlertType.Info => "alert-info",
            AlertType.Light => "alert-light",
            AlertType.Dark => "alert-dark",
            _ => string.Empty
        };
    }

    public static string ToBootstrapCssClass(this ColumnWidth width)
    {
        return width switch
        {
            ColumnWidth.Max => "col-lg",
            ColumnWidth.Auto => "col-auto",
            _ => $"col-lg-{(int)width}"
        };
    }

    public static string ToBootstrapCssClass(this HorizontalAlignment alignment)
    {
        return alignment switch
        {
            HorizontalAlignment.Start => "justify-content-start",
            HorizontalAlignment.Center => "justify-content-center",
            HorizontalAlignment.End => "justify-content-end",
            HorizontalAlignment.Around => "justify-content-around",
            HorizontalAlignment.Between => "justify-content-between",
            HorizontalAlignment.Evenly => "justify-content-evenly",
            _ => string.Empty
        };
    }

    public static string ToBootstrapCss(this TextAlignment alignment)
    {
        return alignment switch
        {
            TextAlignment.Left => "text-start",
            TextAlignment.Center => "text-center",
            TextAlignment.Right => "text-end",
            _ => string.Empty
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
            _ => string.Empty
        };

        return $"language-{language}";
    }
}
