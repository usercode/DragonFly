// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GridSpan
/// </summary>
public class GridSpan
{
    public GridSpan()
    {
    }

    public GridSpan(double value, GridUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Value
    /// </summary>
    public double Value { get; set; } = 1;

    /// <summary>
    /// Unit
    /// </summary>
    public GridUnit Unit { get; set; } = GridUnit.Fraction;
}
