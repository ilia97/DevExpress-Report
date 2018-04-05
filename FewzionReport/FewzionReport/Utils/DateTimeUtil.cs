using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport.Utils
{
    public class DateTimeUtil
    {
        public static DateTime GetStartOfWeek(DateTime dt, int startOfWeek)
        {
            return dt.AddDays(-1 * ((6 + (int)dt.DayOfWeek - startOfWeek) % 7)).Date;
        }

        public static int[] ConvertToISOWeekNumber(DateTime dt)
        {
            var dayOfWeek = (int)dt.DayOfWeek;
            dt = dt.AddDays(-1 * ((dayOfWeek + 6) % 7) + 3); // Nearest Thu
            var jan4 = new DateTime(dt.Year, 1, 4, 0, 0, 0, DateTimeKind.Utc); // Jan 4 is in Week 1 for ISO 8601
            var weekNumber = Math.Round((dt.Subtract(jan4).TotalMilliseconds) / (7 * 864e5)) + 1;
            return new int[] { dt.Year, (int)weekNumber, dayOfWeek == 0 ? 7 : dayOfWeek };
        }

        public static DateTime ConvertFromISOWeekNumber(int[] ywd)
        {
            var jan4 = new DateTime(ywd[0], 1, 4, 0, 0, 0, DateTimeKind.Utc); // ISO 8601
            var dayOfWeek = (int)jan4.DayOfWeek;
            return jan4.AddDays(-1 * ((dayOfWeek + 6) % 7) + 7 * ywd[1] + ywd[2] - 8);
        }

        public static int[] ConvertToWeekNumber(DateTime dt, int startOfWeek)
        {
            var jan1 = new DateTime(dt.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dayOfYear = Math.Round(dt.Subtract(jan1).TotalMilliseconds / 864e5); // 0-based day of year
            dayOfYear = dayOfYear - (7 + startOfWeek - (int)jan1.DayOfWeek) % 7 + 6;
            return new int[] { dt.Year, (int)(1 + dayOfYear / 7), (int)(1 + (7 + dayOfYear) % 7) };
        }

        public static DateTime ConvertFromWeekNumber(int[] ywd, int startOfWeek)
        {
            var jan1 = new DateTime(ywd[0], 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dayOfYear = (ywd[1] - 1) * 7 + ywd[2];
            dayOfYear = dayOfYear + (7 + startOfWeek - (int)jan1.DayOfWeek) % 7 - 6;
            return jan1.AddDays(dayOfYear - 1);
        }

        public static string WeekName(DateTime dt, int startOfWeek)
        {
            var date = GetStartOfWeek(dt, startOfWeek);
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            int[] ywd1 = ConvertToWeekNumber(date, startOfWeek);
            int[] ywd2 = ConvertToWeekNumber(date.AddDays(6), startOfWeek);

            string weekName = String.Format("Week {0} {1}", ywd1[1], ywd1[0]);

            if (ywd1[1] != ywd2[1])
                weekName = String.Format("{0} and Week {1} {2}", weekName, ywd2[1], ywd2[0]);

            return weekName;
        }

        public static string GetSuffix(DateTime date)
        {
            switch (date.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
