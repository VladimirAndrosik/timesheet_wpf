using System;

namespace timesheet.core.Helpers
{
    public static class DateTimeHelpers
    {
        public static DateTime StartOfWeek(DateTime date, DayOfWeek startDay = DayOfWeek.Sunday)
        {
            var diff = (7 + (date.DayOfWeek - startDay)) % 7;
            return date.AddDays(-1 * diff).Date;
        }
    }
}