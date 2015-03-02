namespace Endjin.Licensing.Extensions
{
    #region Using Directives

    using System;

    #endregion

    public static class DateTimeExtensions
    {
        public static DateTimeOffset EndOfDay(this DateTimeOffset dateTime)
        {
            return new DateTimeOffset(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59));
        }

        public static DateTimeOffset LastDayOfMonth(this DateTimeOffset dateTime)
        {
            return new DateTimeOffset(new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month)));
        }
    }
}