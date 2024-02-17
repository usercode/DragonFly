// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

public class MongoContentVersion
{
    public Guid Id { get; set; }

    public MongoContentItem Content { get; set; }
}
