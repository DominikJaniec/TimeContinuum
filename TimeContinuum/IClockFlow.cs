using System;

namespace TimeContinuum
{
    public interface IClockFlow
    {
        DateTimeOffset Instance { get; }
        DateTime CurrentClick { get; }
        TimeZoneInfo Config { get; }
    }
}
