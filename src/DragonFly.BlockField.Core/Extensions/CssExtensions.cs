// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Globalization;

namespace DragonFly.BlockField;

public static class CssExtensions
{
    public static string? ToGridTemplate(this IEnumerable<GridSpan> spans)
    {
        List<string> builder = new List<string>(spans.Count());

        foreach (GridSpan span in spans)
        {
            if (span.Unit == GridUnit.Auto)
            {
                builder.Add($"{span.Unit.ToCss()}");
            }
            else
            {
                builder.Add($"{span.Value.ToString(CultureInfo.InvariantCulture)}{span.Unit.ToCss()}");
            }
        }

        return string.Join(" ", builder);
    }

    public static string? ToCss(this GridUnit unit)
    {
        return ((GridUnit?)unit).ToCss();
    }

    public static string? ToCss(this GridUnit? unit)
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
        return ((ColorType?)colorType).ToBootstrapCssClass();
    }
     
    public static string? ToBootstrapCssClass(this ColorType? colorType)
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
        return ((ColumnWidth?)width).ToBootstrapCssClass();
    }

    public static string? ToBootstrapCssClass(this ColumnWidth? width)
    {
        return width switch
        {
            ColumnWidth.Max => "col-lg",
            ColumnWidth.Auto => "col-lg-auto",
            >= ColumnWidth.W1 and <= ColumnWidth.W12 => $"col-lg-{(int)width}",
            _ => null
        };
    }

    public static string? ToBootstrapCssClass(this HorizontalAlignment alignment)
    {
        return ((HorizontalAlignment?)alignment).ToBootstrapCssClass();
    }

    public static string? ToBootstrapCssClass(this HorizontalAlignment? alignment)
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
        return ((TextAlignment?)alignment).ToBootstrapCss();
    }

    public static string? ToBootstrapCss(this TextAlignment? alignment)
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
        return ((CodeType?)type).ToPrismCssClass();
    }

    public static string? ToPrismCssClass(this CodeType? type)
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
