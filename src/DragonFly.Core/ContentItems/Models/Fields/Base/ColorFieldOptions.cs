// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class ColorFieldOptions : ContentFieldOptions
{
    public ColorFieldOptions()
    {
        
    }

    public override IContentField CreateContentField()
    {
        return new ColorField();
    }
}
