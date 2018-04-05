using System;
using System.Collections.Generic;

namespace FewzionReport.Models
{
    public class ShiftGroup
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}