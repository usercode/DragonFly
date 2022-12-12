// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class ColumnWidthExtensions
{
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
}
