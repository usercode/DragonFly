﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IChildBlocks
{
    IEnumerable<BlockContext> GetBlocks();
}
