using System;
using System.Globalization;

namespace FewzionReport.Models
{
    public sealed class Shift
    {
        public class Builder
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public bool? StartsDayBefore { get; set; }
            public bool? EndsDayAfter { get; set; }

            public Shift Build() { return new Shift(Date.Date, Name, Group, Start, End, StartsDayBefore.GetValueOrDefault(), EndsDayAfter.GetValueOrDefault()); }
        }

        public static readonly string[] TimeFormats = { "HH:mm", "H:mm", "HH:m", "H:m" };

        public DateTime Date { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get { return EndTime.Subtract(StartTime); } }

        private Shift(DateTime date, string name, string group, string start, string end, bool startsDayBefore, bool endsDayAfter)
        {
            var startTime = date.Add(DateTime.ParseExact(start, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay);
            var endTime = date.Add(DateTime.ParseExact(end, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay);

            if (startsDayBefore)
                startTime = startTime.AddDays(-1);
            if (endsDayAfter)
                endTime = endTime.AddDays(1);

            Date = date;
            Name = name;
            Group = group;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}