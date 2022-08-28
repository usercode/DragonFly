using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

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
