// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugField
/// </summary>
[Field]
[FieldOptions(typeof(SlugFieldOptions))]
public partial class SlugField : TextBaseField
{
    public SlugField()
    {

    }

    public SlugField(string? text)
    {
        Value = text;
    }

    protected override void OnValueChanging(ref string? newValue)
    {
        //if (newValue != null)
        //{
        //    newValue = Slugify.ToSlug(newValue);
        //}
    }
}
