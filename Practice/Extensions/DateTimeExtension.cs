namespace Practice.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime TrDate(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        }
    }
}
