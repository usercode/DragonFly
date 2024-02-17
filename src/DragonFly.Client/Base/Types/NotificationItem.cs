// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

/// <summary>
/// NotificationItem
/// </summary>
public class NotificationItem
{
    public NotificationItem(NotificationType type, string message)
    {
        Type = type;
        Message = message;
    }

    /// <summary>
    /// Type
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }


}
