// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

[Field]
[FieldOptions(typeof(GeolocationFieldOptions))]
[FieldQuery(typeof(GeolocationFieldQuery))]
public partial class GeolocationField
{
    /// <summary>
    /// Valid longitude values are between -180 and 180, both inclusive.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Valid latitude values are between -90 and 90, both inclusive.
    /// </summary>
    public double? Latitude { get; set; }

    public override void Clear()
    {
        Latitude = null;
        Longitude = null;
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        if (Latitude != null)
        {
            if (Latitude < -90 || Latitude > 90)
            {
                context.AddRangeValidation($"{fieldName} (Latitude)", -90, 90);
            }
        }

        if (Longitude != null)
        {
            if (Longitude < -180 || Longitude > 180)
            {
                context.AddRangeValidation($"{fieldName} (Longitude)", -180, 180);
            }
        }
    }
}
