// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GeolocationFieldOptions
/// </summary>
public class GeolocationFieldOptions : FieldOptions
{
    public override ContentField CreateContentField()
    {
        return new GeolocationField();
    }
}
