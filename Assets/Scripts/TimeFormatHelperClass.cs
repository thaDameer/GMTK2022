public static class TimeFormatHelperClass
{
    public static string FormatTime(float time)
    {
        int minutes = (int)time / 60000;
        int seconds = (int)time / 1000 - 60 * minutes;
        int milliseconds = (int)time - minutes * 60000 - 1000 * seconds;
        if (time < 60)
            return string.Format("{0:0}:{1:0}", time, milliseconds);
        return string.Format("{0:0}:{1:0}:{2:0}", minutes, seconds, milliseconds);
    }
}