// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Globalization;

namespace DragonFly;

public static class CssExtensions
{
    public static string? ToCssGridTemplate(this IList<GridSpan> spans)
    {
        string[] builder = new string[spans.Count];

        int i = 0;

        foreach (GridSpan span in spans)
        {
            if (span.Unit == GridUnit.Auto)
            {
                builder[i] = $"{span.Unit.ToCss()}";
            }
            else
            {
                builder[i] = $"{span.Value.ToString(CultureInfo.InvariantCulture)}{span.Unit.ToCss()}";
            }

            i++;
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
}
