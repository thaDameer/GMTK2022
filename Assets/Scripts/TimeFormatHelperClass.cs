using System;

public static class TimeFormatHelperClass
{
    public static string FormatTime(float time)
    {
        var timeSpan = TimeSpan.FromSeconds(time);
        if (timeSpan.Seconds < 60)
            return string.Format("{0:0}:{1:0}", timeSpan.Seconds, timeSpan.Milliseconds);
        return string.Format("{0:0}:{1:0}:{2:0}", timeSpan.Minutes, timeSpan.Seconds,timeSpan.Milliseconds);
    }
}