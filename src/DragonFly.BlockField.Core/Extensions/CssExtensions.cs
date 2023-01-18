// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField.Css;

public static class CssExtensions
{
    public static string? ToCss(this GridUnit unit)
    {
        return unit switch
        {
            GridUnit.Pixel => "px",
            GridUnit.Percent => "%",
            GridUnit.Fraction => "fr",
            GridUnit.Auto => "auto",
            _ => null
        };
    }

    public static string? ToBootstrapCssClass(this ColorType colorType)
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

    public static string? ToBootstrapCssClass(this ColumnWidth width)
    {
        return width switch
        {
            ColumnWidth.Max => "col-lg",
            ColumnWidth.Auto => "col-lg-auto",
            > ColumnWidth.W1 and  < ColumnWidth.W12 => $"col-lg-{(int)width}",
            _ => null
        };
    }

    public static string? ToBootstrapCssClass(this HorizontalAlignment alignment)
    {
        return alignment switch
        {
            HorizontalAlignment.Start => "justify-content-start",
            HorizontalAlignment.Center => "justify-content-center",
            HorizontalAlignment.End => "justify-content-end",
            HorizontalAlignment.Around => "justify-content-around",
            HorizontalAlignment.Between => "justify-content-between",
            HorizontalAlignment.Evenly => "justify-content-evenly",
            _ => null
        };
    }

    public static string? ToBootstrapCss(this TextAlignment alignment)
    {
        return alignment switch
        {
            TextAlignment.Left => "text-start",
            TextAlignment.Center => "text-center",
            TextAlignment.Right => "text-end",
            _ => null
        };
    }

    public static string? ToPrismCssClass(this CodeType type)
    {
        string? language = type switch
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

        if (language == null)
        {
            return null;
        }

        return $"language-{language}";
    }
}
