﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Contents.Content;

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
