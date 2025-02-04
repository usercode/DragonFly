﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

public static class CssExtensions
{
    public static string ToCss(this BackgroundTaskState state)
    {
        return state switch
        {
            BackgroundTaskState.Awaiting => null,
            BackgroundTaskState.Completed => "list-group-item-success",
            BackgroundTaskState.Failed => "list-group-item-danger",
            BackgroundTaskState.Canceled => "list-group-item-warning",
            BackgroundTaskState.Canceling => "list-group-item-warning",
            _ => null
        };
    }
}
