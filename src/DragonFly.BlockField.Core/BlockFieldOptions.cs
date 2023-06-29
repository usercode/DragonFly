﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public class BlockFieldOptions : FieldOptions
{
    public override ContentField CreateContentField()
    {
        return new BlockField();
    }
}
