// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugField
/// </summary>
[FieldOptions(typeof(SlugFieldOptions))]
public class SlugField : TextBaseField
{
    public SlugField()
    {

    }

    public override bool CanSorting => true;

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
