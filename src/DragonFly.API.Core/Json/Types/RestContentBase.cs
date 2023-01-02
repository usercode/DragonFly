// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public abstract class RestContentBase
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public int Version { get; set; }
}
