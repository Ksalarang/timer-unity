namespace Timer.Controllers
{
    public static class TimeConstants
    {
        public const int MillisInSecond = 1000;
        public const int SecondsInMinute = 60;
        public const int MinutesInHour = 60;
        public const int MillisInMinute = SecondsInMinute * MillisInSecond;
        public const int MillisInHour = MinutesInHour * MillisInMinute;
    }
}