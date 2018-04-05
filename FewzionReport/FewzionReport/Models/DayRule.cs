using System;

namespace FewzionReport.Models
{
    public class DayRule
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool? StartsDayBefore { get; set; }
        public bool? EndsDayAfter { get; set; }
        
        public string ReviewDay { get; set; }
        
        public string StartsEnds
        {
            get
            {
                if (StartsDayBefore ?? false) return "StartsDayBefore";
                if (EndsDayAfter ?? false) return "EndsDayAfter";
                return null;
            }
        }
        public string TTFCOffset { get; set; }
        public bool? WeekendShift { get; set; }
        
        public decimal Hours
        {
            get
            {
                if (EndTime > StartTime)  // If on the same day
                    return (decimal)EndTime.Subtract(StartTime).TotalHours;

                return (decimal)(24 - StartTime.TotalHours + EndTime.TotalHours);
            }
        }
        
        public TimeSpan StartTime
        {
            get
            {
                return TimeSpan.Parse(Start);
            }
        }
        
        public TimeSpan EndTime
        {
            get
            {
                return TimeSpan.Parse(End);
            }
        }
        
        // ReSharper disable once InconsistentNaming
        public short TTFCOffsetInt
        {
            get
            {
                short result;
                if (!Int16.TryParse(TTFCOffset, out result))
                    result = 0;
                return result;
            }
        }
    }
}