// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class ReferenceFieldOptions : ContentFieldOptions
{


    public override IContentField CreateContentField()
    {
        return new ReferenceField();
    }
}
