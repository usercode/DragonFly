using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class ColumnWidthExtensions
{
    public static string ToBootstrapCssClass(this ColumnWidth width)
    {
        if (width == ColumnWidth.Max)
        {
            return "col-lg";
        }

        if (width == ColumnWidth.Auto)
        {
            return $"col-auto";
        }

        return $"col-lg-{(int)width}";
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
