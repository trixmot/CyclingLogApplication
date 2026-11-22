using System;

namespace CyclingLogApplication
{
    public static class DaylightSavingTime
    {
        public static int DaysUntilNextTransition(DateTime date)
        {
            return DaysUntilNextTransition(date, TimeZoneInfo.Local);
        }

        public static int DaysUntilNextTransition(DateTime date, TimeZoneInfo timeZone)
        {
            if (timeZone == null)
            {
                throw new ArgumentNullException("timeZone");
            }

            DateTime startDate = date.Date;
            bool daylightSavingOnStartDate = IsDaylightSavingTime(startDate, timeZone);

            if (startDate > DateTime.MinValue.Date &&
                IsDaylightSavingTime(startDate.AddDays(-1), timeZone) != daylightSavingOnStartDate)
            {
                return 0;
            }

            // DST rules are normally annual. Two years also covers zones whose
            // next transition is more than a year away.
            for (int days = 1; days <= 366 * 2; days++)
            {
                DateTime candidateDate = startDate.AddDays(days);
                if (IsDaylightSavingTime(candidateDate, timeZone) != daylightSavingOnStartDate)
                {
                    return days;
                }
            }

            return -1;
        }

        private static bool IsDaylightSavingTime(DateTime date, TimeZoneInfo timeZone)
        {
            // Noon avoids the usual transition times and Unspecified makes the
            // value be interpreted in the supplied time zone.
            DateTime localNoon = DateTime.SpecifyKind(date.AddHours(12), DateTimeKind.Unspecified);
            return timeZone.IsDaylightSavingTime(localNoon);
        }
    }
}
