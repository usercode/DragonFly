// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class LocalDateTimeService : IDateTimeService
{
    public DateTime Current()
    {
        return DateTime.Now;
    }
}
