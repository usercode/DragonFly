// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly;

public static class ContentErrors
{
    public static readonly IError NotFound = new Error("Content not found.");
    public static readonly IError InvalidState = new Error("Content has an invalid state.");
}
