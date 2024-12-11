// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

public static class NotificationExtensions
{
    public static MessageIntent ToAlertCss(this NotificationType type)
    {
        return type switch
        {
            NotificationType.Success => MessageIntent.Success,
            NotificationType.Warning => MessageIntent.Warning,
            NotificationType.Error => MessageIntent.Error,
            _ => MessageIntent.Info
        };
    }
}
