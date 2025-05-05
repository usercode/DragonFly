// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.ComponentModel;
using System.Globalization;

namespace DragonFly.Core;

internal class ContentReferenceTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            return ContentReference.Parse(stringValue, culture);
        }

        return base.ConvertFrom(context, culture, value);
    }
}
