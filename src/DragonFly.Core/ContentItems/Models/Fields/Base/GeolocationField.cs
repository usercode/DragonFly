// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly;

[FieldOptions(typeof(GeolocationFieldOptions))]
public class GeolocationField : ContentField
{
    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public override void Clear()
    {
        Latitude = null;
        Longitude = null;
    }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (Latitude != null)
        {
            if (Latitude < -90 || Latitude > 90)
            {
                context.AddRangeValidation(nameof(Latitude), -90, 90);
            }
        }

        if (Longitude != null)
        {
            if (Longitude < -180 || Longitude > 180)
            {
                context.AddRangeValidation(nameof(Longitude), -180, 180);
            }
        }
    }
}
