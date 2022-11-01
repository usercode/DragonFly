// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;

namespace DragonFly.AspNetCore;

public static class AlertTypeExtensions
{
    public static string ToBootstrapCssClass(this AlertType alertType)
    {
        return alertType switch
        {
            AlertType.Primary => "alert-primary",
            AlertType.Secondary => "alert alert-secondary",
            AlertType.Success => "alert-success",
            AlertType.Danger => "alert-danger",
            AlertType.Warning => "alert-warning",
            AlertType.Info => "alert-info",
            AlertType.Light => "alert-light",
            AlertType.Dark => "alert-dark",
            _ => string.Empty
        };
    }
}
