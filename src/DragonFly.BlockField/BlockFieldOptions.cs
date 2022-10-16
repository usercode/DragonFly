// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public class BlockFieldOptions : ContentFieldOptions
{
    public override IContentField CreateContentField()
    {
        return new BlockField();
    }
}
