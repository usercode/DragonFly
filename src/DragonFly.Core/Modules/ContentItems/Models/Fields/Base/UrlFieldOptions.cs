// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// UrlFieldOptions
/// </summary>
public class UrlFieldOptions : FieldOptions
{
    public UrlFieldOptions()
    {
    }

    public override ContentField CreateContentField()
    {
        return new UrlField();
    }
}
