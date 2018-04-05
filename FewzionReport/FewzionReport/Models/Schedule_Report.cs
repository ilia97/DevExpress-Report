using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport.Models
{
    class Schedule_Report
    {
        public Schedule_Report()
        {
            ShiftPlans = new List<ShiftPlan_Report>();
            WeeklyShiftPlans = new List<ShiftPlan_Report>();
        }

        public string Id { get; set; } // process Id goes in here
        public int DisplayOrder { get; set; } // process DisplayOrder goes in here
        public string Name { get; set; } // process Name goes in here
        public Process Process { get; set; }
        public DateTime Date { get; set; } // start date of schedule
        public List<ShiftPlan_Report> ShiftPlans { get; set; }
        public List<ShiftPlan_Report> WeeklyShiftPlans { get; set; }
        public int Page { get; set; }
        public IList<AppliedKPI> KPISummary { get; set; }  // KPI summary across shift plans

        public bool ShortRowOnReport { get; set; }

        public string WeekName
        {
            get
            {
                return "Name";
            }
        }
    }
}
