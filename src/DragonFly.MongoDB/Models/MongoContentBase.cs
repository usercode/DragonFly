﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoContentBase
/// </summary>
public abstract class MongoContentBase
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// CreatedAt
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// ModifiedAt
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// PublishedAt
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public int Version { get; set; }

    public override string ToString()
    {
        return $"{Id}";
    }
}
