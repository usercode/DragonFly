// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

public class MongoAssetFolder : MongoContentBase
{
    public string? Name { get; set; }

    public Guid? Parent { get; set; }
}
