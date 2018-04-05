using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport.Models
{
    public class ShiftRules
    {

        public ShiftRules()
        {
            Rules = new List<ShiftRule>();
        }

        public string Id { get; set; }

        private static Func<ShiftRules> _valueFactory = () => new ShiftRules();

        static Lazy<ShiftRules> _instance = new Lazy<ShiftRules>(_valueFactory);

        public int ArrivedOnTimeBeforeMinutes { get; set; }
        public int ArrivedOnTimeAfterMinutes { get; set; }
        public int LeftOnTimeBeforeMinutes { get; set; }
        public int LeftOnTimeAfterMinutes { get; set; }
        public int AbsentAfterMinutes { get; set; }

        public List<ShiftRule> Rules { get; set; }

        Dictionary<string, int> _shiftOrder = new Dictionary<string, int>();

        Dictionary<string, int> _dayOrder = new Dictionary<string, int>();

        public ShiftRules Initialize()
        {
            foreach (var shiftRule in Rules)
                foreach (var dayRule in shiftRule.DayRules.Where(dayRule => String.IsNullOrEmpty(dayRule.Group)))
                    dayRule.Group = String.Join("-", dayRule.Id, shiftRule.Id);

            _shiftOrder = Rules.Select((x, i) => new
            {
                x.Id,
                Order = i
            }).ToDictionary(x => x.Id, x => x.Order);

            var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            _dayOrder = Enumerable.Range(0, 7).Select(i => new
            {
                Id = days[i % 7],
                Order = i
            }).ToDictionary(x => x.Id, x => x.Order);

            return this;
        }

        public static Dictionary<string, int> ShiftOrder
        {
            get
            {
                return GetShiftRules()._shiftOrder;
            }
        }

        public static Dictionary<string, int> DayOrder
        {
            get
            {
                return GetShiftRules()._dayOrder;
            }
        }

        public static ShiftRules GetShiftRules()
        {
            return _instance.Value;
        }

        public static void Reset()
        {
            _instance = new Lazy<ShiftRules>(_valueFactory);
        }

        public static void Reset(Func<ShiftRules> valueFactory)
        {
            _valueFactory = valueFactory;
            Reset();
        }

        public static List<ShiftRule> GetRules()
        {
            return GetShiftRules().Rules;
        }

        public static ShiftRule GetRule(string shift)
        {
            return GetRules().FirstOrDefault(r => r.Id == shift);
        }

        public static string GetShiftColour(string shift)
        {
            return "#FFCC99";
        }

        public static IEnumerable<Shift> FindAllShiftsBetweenDates(DateTime start, DateTime end)
        {
            for (var date = new DateTime(start.Ticks).Date; date < end.Date; date = date.AddDays(1))
            {
                foreach (var shiftRule in GetRules())
                {
                    var dayRule = shiftRule.DayRules.FirstOrDefault(x => x.Id == date.ToString("dddd"));
                    if (dayRule == null) continue;

                    yield return CreateShift(date, shiftRule.Id, dayRule);
                }
            }
        }

        public static IEnumerable<ShiftGroup> FindAllShiftGroupsBetweenDates(DateTime start, DateTime end)
        {
            return FindAllShiftsBetweenDates(start, end)
                .GroupAdjacent(x => x.Group)
                .Select(g =>
                {
                    var name = g.Key.Split('-').Skip(1).First();
                    return new { Name = name, First = g.FirstOrDefault(x => x.Name == name), Shifts = g.ToList() };
                })
                .Where(g => g.First != null)
                .Select(g => new ShiftGroup
                {
                    Name = g.Name,
                    Date = g.First.Date,
                    StartTime = g.Shifts.MinBy(x => x.StartTime).StartTime,
                    EndTime = g.Shifts.MaxBy(x => x.EndTime).EndTime,
                    Shifts = g.Shifts
                });
        }

        private static Shift CreateShift(DateTime date, string shift, DayRule dayRule)
        {
            return new Shift.Builder
            {
                Date = date,
                Name = shift,
                Group = dayRule.Group,
                Start = dayRule.Start,
                End = dayRule.End,
                StartsDayBefore = dayRule.StartsDayBefore,
                EndsDayAfter = dayRule.EndsDayAfter
            }.Build();
        }

        #region Excluded Shifts

        private static string GetDayOfWeek(DateTime now)
        {
            var ret = "";
            switch (now.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    ret = "Monday";
                    break;
                case System.DayOfWeek.Tuesday:
                    ret = "Tuesday";
                    break;
                case System.DayOfWeek.Wednesday:
                    ret = "Wednesday";
                    break;
                case System.DayOfWeek.Thursday:
                    ret = "Thursday";
                    break;
                case System.DayOfWeek.Friday:
                    ret = "Friday";
                    break;
                case System.DayOfWeek.Saturday:
                    ret = "Saturday";
                    break;
                case System.DayOfWeek.Sunday:
                    ret = "Sunday";
                    break;
            }
            return ret;
        }

        public static List<string> GetTodaysExcludedShifts(DateTime now)
        {
            List<ShiftRule> shifts = ShiftRules.GetRules();
            List<string> excludeshifts = new List<string>();
            foreach (ShiftRule sr in shifts)
            {
                List<DayRule> dayrules = sr.DayRules;
                string nowday = GetDayOfWeek(now);
                foreach (DayRule srd in dayrules)
                {
                    if (srd.Id == nowday)
                    {
                        TimeSpan start = TimeSpan.Parse(srd.Start);
                        DateTime shiftStart = now.Date.AddMinutes(start.Minutes + (60 * start.Hours));

                        TimeSpan end = TimeSpan.Parse(srd.End);
                        DateTime shiftEnd = new DateTime();

                        Boolean endstmrw = false;
                        if (srd.EndsDayAfter != null)
                        {
                            endstmrw = (Boolean)srd.EndsDayAfter;
                        }
                        if (endstmrw)
                        {
                            shiftEnd = now.Date.AddMinutes(end.Minutes + (60 * end.Hours) + (60 * 24));
                        }
                        else
                        {
                            shiftEnd = now.Date.AddMinutes(end.Minutes + (60 * end.Hours));
                        }

                        if ((shiftStart <= now) && (shiftEnd >= now) || (shiftEnd < now))
                        {
                            excludeshifts.Add(sr.Id);
                        }

                    }
                }
            }

            return excludeshifts;
        }

        #endregion
    }
}
