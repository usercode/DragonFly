// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using MudBlazor;

namespace DragonFly.Client;

public static class NotificationExtensions
{
    public static Severity ToAlertCss(this NotificationType type)
    {
        return type switch
        {
            NotificationType.Success => Severity.Success,
            NotificationType.Warning => Severity.Warning,
            NotificationType.Error => Severity.Error,
            _ => Severity.Normal
        };
    }
}
