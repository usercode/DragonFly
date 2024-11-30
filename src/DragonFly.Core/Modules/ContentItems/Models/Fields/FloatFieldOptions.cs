// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class FloatFieldOptions : SingleValueFieldOptions<double?>
{
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }

    public override ContentField CreateContentField()
    {
        return new FloatField(DefaultValue);
    }
}
