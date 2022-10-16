// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class SlugFieldOptions : StringFieldOptions
{
    public override IContentField CreateContentField()
    {
        return new SlugField(DefaultValue);
    }
}
