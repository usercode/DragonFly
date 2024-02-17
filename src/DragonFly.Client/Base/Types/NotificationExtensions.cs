// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;

namespace DragonFly.Client;

public static class NotificationExtensions
{
    public static BSColor ToAlertCss(this NotificationType type)
    {
        return type switch
        {
            NotificationType.Success => BSColor.Success,
            NotificationType.Warning => BSColor.Warning,
            NotificationType.Error => BSColor.Danger,
            _ => BSColor.Default
        };
    }
}
