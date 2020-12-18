using System;

namespace timesheet.business.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime StartOfWeek(DateTime date, DayOfWeek startDay = DayOfWeek.Sunday)
        {
            var diff = (7 + (date.DayOfWeek - startDay)) % 7;
            return date.AddDays(-1 * diff).Date;
        }
    }
}