// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class EmbedFieldOptions : ContentFieldOptions
{
    public override ContentField CreateContentField()
    {
        return new EmbedField();
    }
}
