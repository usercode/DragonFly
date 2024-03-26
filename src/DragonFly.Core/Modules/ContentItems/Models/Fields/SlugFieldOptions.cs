// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class SlugFieldOptions : FieldOptions
{
    public override ContentField CreateContentField()
    {
        return new SlugField();
    }

    /// <summary>
    /// Gets the name of a single value field which is used for the slug value.
    /// </summary>
    public string? TargetField { get; set; }
}
