// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Current()
    {
        return DateTime.Now;
    }
}
