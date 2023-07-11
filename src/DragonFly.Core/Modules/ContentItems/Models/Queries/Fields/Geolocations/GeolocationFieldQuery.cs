// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GeolocationFieldQuery
/// </summary>
public class GeolocationFieldQuery : FieldQuery
{
    /// <summary>
    /// Valid longitude values are between -180 and 180, both inclusive.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Valid latitude values are between -90 and 90, both inclusive.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// MinDistance
    /// </summary>
    public double? MinDistance { get; set; }

    /// <summary>
    /// MaxDistance
    /// </summary>
    public double? MaxDistance { get; set; }

    public override bool IsEmpty()
    {
        return Longitude == null || Latitude == null;
    }
}
