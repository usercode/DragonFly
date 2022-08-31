using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Fields.BlockField;

namespace DragonFly.AspNetCore;

public static class ColumnWidthExtensions
{
    public static string ToBootstrapCssClass(this ColumnWidth width)
    {
        if (width == ColumnWidth.Max)
        {
            return "col";
        }

        if (width == ColumnWidth.Auto)
        {
            return $"col-auto";
        }

        return $"col-{(int)width}";
    }
}
