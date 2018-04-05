using System.Collections.Generic;
using System.Linq;

namespace FewzionReport.Models
{
    public class ShiftRule
    {
        public string Id { get; set; }
        public string ShortCode { get; set; }
        public string Group { get; set; }
        public int RollAfterMinutes { get; set; }
        public string Colour { get; set; }
        public string ShiftIcon { get; set; }
        public bool DayShift { get; set; }
        public List<DayRule> DayRules { get; set; }

        public ShiftRule()
        {
            DayRules = new List<DayRule>();
        }

        public DayRule GetRule(string day)
        {
            return DayRules.FirstOrDefault(x => x.Id == day);
        }
    }
}