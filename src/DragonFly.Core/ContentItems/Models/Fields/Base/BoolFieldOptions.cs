// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class BoolFieldOptions : SingleValueFieldOptions<bool>
{
    public override ContentField CreateContentField()
    {
        return new BoolField(DefaultValue);
    }
}
