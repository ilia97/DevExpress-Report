using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FewzionReport.Models
{
    public class Schedule
    {
        public Schedule()
        {
            ShiftPlans = new List<ShiftPlan>();
            WeeklyShiftPlans = new List<ShiftPlan>();
            ShiftPlanTasks = new Dictionary<string, IList<Task>>();
        }

        public string Id { get; set; } // process Id goes in here
        public int DisplayOrder { get; set; } // process DisplayOrder goes in here
        public string Name { get; set; } // process Name goes in here
        public Process Process { get; set; }
        public bool ShortRowOnReport { get; set; }
        public DateTime Date { get; set; } // start date of schedule
        public List<ShiftPlan> ShiftPlans { get; set; }
        public List<ShiftPlan> WeeklyShiftPlans { get; set; }
        public Dictionary<string, IList<Task>> ShiftPlanTasks { get; set; }

        public string WeekName
        {
            get
            {
                return "Week";
            }
        }

    }
}
