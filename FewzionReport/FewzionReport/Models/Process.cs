using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FewzionReport.Models
{
    public class Process
    {
        public Process()
        {
            DefaultKPIs = new List<DefaultKPI>();
            StandardTasks = new List<StandardTaskOld>();
            PlannedTasks = new List<PlannedTaskOld>();
            ShiftPlanChecklistQuestions = new List<ProcessChecklistQuestion>();
            TaskChecklistQuestions = new List<ProcessChecklistQuestion>();
            Locations = new List<ProcessLocation>();
            CrewLeaderOccupations = new List<string>();
            DailyActualsReportKPISummaryShortCodes = new List<string>();
        }

        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return new StringBuilder(128)
                .Append(Name)
                .Append(" (")
                .Append(ShortCode)
                .Append(")")
                .ToString();
        }

        public string Type { get; set; }

        public bool CanAcceptTasks { get; set; }

        public bool EnableShiftPlanApproval { get; set; }
        public bool EnableShiftPlanAcknowledgement { get; set; }
        public bool EnableShortIntervalControl { get; set; }
        public bool EnableShiftPlanNotesonActuals { get; set; }

        public bool ManagesEquipment { get; set; }
        public bool ManagesPeople { get; set; }
        //public bool ShowOnShiftPlan { get; set; }
        public bool ShowOnPlanningBoard { get; set; }
        public bool ShowOnManningReport { get; set; }
        //public bool ShowOnSchedule { get; set; }
        public bool ShowOn2448HourSchedule { get; set; }
        public bool ShowOnShiftSchedule { get; set; }
        public bool ShowOnWeeklySchedule { get; set; }
        public bool ShowOnEquipmentSchedule { get; set; }
        public bool ShowOnSmartboard { get; set; }

        public int ShiftPlanReportAdditionalTasksLines { get; set; }
        public int ShiftPlanReportTopDelaysAndOrStoppagesLines { get; set; }
        public bool ShowShiftPlanReport5WHY { get; set; }
        public bool ShowShiftPlanReportChecklist { get; set; }
        public string FirstKPIToTrack { get; set; }
        public string FirstKPIToTrackPeriod { get; set; }
        public string SecondKPIToTrack { get; set; }
        public string SecondKPIToTrackPeriod { get; set; }
        public string ThirdKPIToTrack { get; set; }
        public string ThirdKPIToTrackPeriod { get; set; }
        public string FourthKPIToTrack { get; set; }
        public string FourthKPIToTrackPeriod { get; set; }
        public string FifthKPIToTrack { get; set; }
        public string FifthKPIToTrackPeriod { get; set; }
        public string SixthKPIToTrack { get; set; }
        public string SixthKPIToTrackPeriod { get; set; }
        public decimal? TonnesMultiplier { get; set; }
        public decimal? WeeklyOperatingHoursTarget { get; set; }
        public bool Active { get; set; }

        public int DisplayOrder { get; set; }

        public bool Deleted { get; set; }

        public string RuntimeTable { get; set; }
        public string[] RuntimeTagArray { get; set; }
        public string ShearsTable { get; set; }
        public string[] ShearsTagArray { get; set; }
        public string UCTable { get; set; }
        public string[] UCTagArray { get; set; }
        public string ChockPosTable { get; set; }
        public string[] ChockPosTagArray { get; set; }
        public string TTFCTable { get; set; }
        public string[] TTFCTagArray { get; set; }

        public List<string> CrewLeaderOccupations { get; set; }
        // ReSharper disable InconsistentNaming
        public List<string> DailyActualsReportKPISummaryShortCodes { get; set; }
        public bool? DailyActualsReportKPISummaryByDepartment { get; set; }
        public bool? DailyActualsReportKPISummaryByProcess { get; set; }
        // ReSharper restore InconsistentNaming


        public List<DefaultKPI> DefaultKPIs { get; set; }
        public List<StandardTaskOld> StandardTasks { get; set; }
        public List<PlannedTaskOld> PlannedTasks { get; set; }
        public List<ProcessChecklistQuestion> ShiftPlanChecklistQuestions { get; set; }
        public List<ProcessChecklistQuestion> TaskChecklistQuestions { get; set; }

        public List<ProcessLocation> Locations { get; set; }
        
        public bool AcceptsTasks
        {
            get { return CanAcceptTasks; }
        }

        // ReSharper disable once InconsistentNaming
        public void AddDefaultKPI(string shortCode, params string[] shifts)
        {
            AddDefaultKPI(shortCode, null, shifts);
        }

        // ReSharper disable once InconsistentNaming
        public void AddDefaultKPI(string shortCode, decimal? target, params string[] shifts)
        {
            foreach (var shift in shifts.Where(shift => !DefaultKPIs.Any(x => x.Shift == shift && x.ShortCode == shortCode)))
            {
                DefaultKPIs.Add(new DefaultKPI
                {
                    Id = Guid.NewGuid().ToString(),
                    Shift = shift,
                    ShortCode = shortCode,
                    Target = target,
                    Enabled = true
                });
            }
        }
    }
}