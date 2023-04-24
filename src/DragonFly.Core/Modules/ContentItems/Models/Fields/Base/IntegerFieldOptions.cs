// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class IntegerFieldOptions : SingleValueFieldOptions<long>
{
    public long? MinValue { get; set; }
    public long? MaxValue { get; set; }

    public override ContentField CreateContentField()
    {
        return new IntegerField(DefaultValue);
    }
}
