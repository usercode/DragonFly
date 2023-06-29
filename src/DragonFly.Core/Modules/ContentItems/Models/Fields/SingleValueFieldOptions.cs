// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class SingleValueFieldOptions<T> : FieldOptions
    where T : struct
{ 

    public T? DefaultValue { get; set; }
}
