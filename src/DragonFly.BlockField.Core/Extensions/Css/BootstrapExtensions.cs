// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public static class BootstrapExtensions
{
    public static string? ToBootstrapTextBackgroundColor(this ColorType colorType)
    {
        return ToBootstrapTextBackgroundColor((ColorType?)colorType);
    }

    public static string? ToBootstrapTextBackgroundColor(this ColorType? colorType)
    {
        if (colorType == null)
        {
            return null;
        }

        return $"text-bg-{colorType.ToBootstrapElement()}";
    }

    public static string? ToBootstrapBorderColorClass(this ColorType colorType)
    {
        return ToBootstrapBorderColorClass((ColorType?)colorType);
    }

    public static string? ToBootstrapBorderColorClass(this ColorType? colorType)
    {
        if (colorType == null)
        {
            return null;
        }

        return $"border-{colorType.ToBootstrapElement()}";
    }

    public static string? ToBootstrapElement(this ColorType colorType)
    {
        return ((ColorType?)colorType).ToBootstrapElement();
    }
     
    public static string? ToBootstrapElement(this ColorType? colorType)
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

    public static string? ToBootstrapElement(this Breakpoint breakpoint)
    {
        return ToBootstrapElement((Breakpoint?)breakpoint);
    }

    public static string? ToBootstrapElement(this Breakpoint? breakpoint)
    {
        return breakpoint switch
        {
            Breakpoint.ExtraSmall => string.Empty,
            Breakpoint.Small => "sm",
            Breakpoint.Medium => "md",
            Breakpoint.Large => "lg",
            Breakpoint.ExtraLarge => "xl",
            Breakpoint.ExtraExtraLarge => "xxl",
            _ => null
        };
    }

    public static string? ToBootstrapClass(this Column column, Breakpoint breakpoint = Breakpoint.Large)
    {
        string? breakpointSuffix = breakpoint.ToBootstrapElement();

        if (breakpoint != Breakpoint.ExtraSmall)
        {
            breakpointSuffix = $"-{breakpointSuffix}";
        }

        return column.Width switch
        {
            ColumnWidth.Max => $"col{breakpointSuffix}",
            ColumnWidth.Auto => $"col{breakpointSuffix}-auto",
            ColumnWidth.Value => $"col{breakpointSuffix}-{column.Value}",
            _ => null
        };
    }

    public static string? ToBootstrapClass(this HorizontalAlignment alignment)
    {
        return ((HorizontalAlignment?)alignment).ToBootstrapClass();
    }

    public static string? ToBootstrapClass(this HorizontalAlignment? alignment)
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

    public static string? ToBootstrapClass(this TextAlignment alignment)
    {
        return ((TextAlignment?)alignment).ToBootstrapClass();
    }

    public static string? ToBootstrapClass(this TextAlignment? alignment)
    {
        return alignment switch
        {
            TextAlignment.Left => "text-start",
            TextAlignment.Center => "text-center",
            TextAlignment.Right => "text-end",
            _ => null
        };
    }
}

/// <summary>
/// Breakpoints are customizable widths that determine how your responsive layout behaves across device or viewport sizes in Bootstrap.
/// </summary>
public enum Breakpoint
{
    /// <summary>
    /// none
    /// </summary>
    ExtraSmall = 0,
    /// <summary>
    /// sm
    /// </summary>
    Small = 576,
    /// <summary>
    /// md
    /// </summary>
    Medium = 768,
    /// <summary>
    /// lg
    /// </summary>
    Large = 992,
    /// <summary>
    /// xl
    /// </summary>
    ExtraLarge = 1200,
    /// <summary>
    /// xxl
    /// </summary>
    ExtraExtraLarge = 1400
}
