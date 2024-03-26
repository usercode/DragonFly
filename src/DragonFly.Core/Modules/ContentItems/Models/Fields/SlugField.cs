// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;

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

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        if (HasValue == false)
        {
            if (options is SlugFieldOptions fieldOptions && string.IsNullOrEmpty(fieldOptions.TargetField) == false)
            {
                var sourceField = context.ContentItem.GetField<SingleValueField<string>>(fieldOptions.TargetField);

                if (sourceField?.Value != null)
                {
                    ISlugService slugService = DragonFlyApi.Default.ServiceProvider.GetRequiredService<ISlugService>();
                    
                    Value = slugService.Transform(sourceField.Value);
                }
            }
        }

        base.Validate(fieldName, options, context);
    }
}
