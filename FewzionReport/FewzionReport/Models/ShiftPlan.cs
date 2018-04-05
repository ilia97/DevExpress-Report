using FewzionReport.Utils;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FewzionReport.Models
{
    public class ShiftPlan
    {
        public ShiftPlan()
        {
            People = new List<ShiftPlanPerson>();
            Equipment = new List<ShiftPlanEquipment>();
            Attachments = new List<ShiftPlanAttachment>();
            Checklist = new List<ShiftPlanChecklistQuestion>();
            Acknowledgements = new List<Acknowledgement>();
        }

        public string Location { get; set; }

        public string Process { get; set; }
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string CrewLeaderLabel { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime DefaultStartDateTime { get; set; }
        public DateTime EndDateTime
        {
            get
            {
                return StartDateTime.AddHours((double)ShiftLength);
            }
        }
        public string Shift { get; set; }
        public string Crew { get; set; }
        public decimal ShiftLength { get; set; }
        public decimal DefaultShiftLength { get; set; }

        public bool TasksLockProcessed { get; set; }

        public string DefaultCrewLeader { get; set; }
        public string CrewLeader { get; set; }
        public string CrewLeaderOrDefaultCrewLeader
        {
            get { return !String.IsNullOrEmpty(CrewLeader) ? CrewLeader : DefaultCrewLeader; }
        }
        public string ActualsCrewLeader { get; set; }   
        public string ActualsCrewLeaderName { get; set; }
        public string SafetyNotes { get; set; }
        public string FormattedSafetyNotes
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    SafetyNotes,
                    "</div>"
                );
            }
        }
        public string ShiftNotes { get; set; }
        public string FormattedShiftNotes
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    ShiftNotes,
                    "</div>"
                );
            }
        }
        public string ShiftReport { get; set; }
        public string FormattedShiftReport
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    ShiftReport,
                    "</div>"
                );
            }
        }
        public string SafetyReport { get; set; }
        public string FormattedSafetyReport
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    SafetyReport,
                    "</div>"
                );
            }
        }
        public string UnscheduledWorkCompleted { get; set; }
        public string FormattedUnscheduledWorkCompleted
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    UnscheduledWorkCompleted,
                    "</div>"
                );
            }
        }
        public string SuggestedWorkAndNotifications { get; set; }
        public string FormattedSuggestedWorkAndNotifications
        {
            get
            {
                return String.Concat(
                    "<div style=\"font-family: 'Arial Narrow', Arial, sans-serif; font-size: 12pt;\">",
                    SuggestedWorkAndNotifications,
                    "</div>"
                );
            }
        }
        
        public string WeekName
        {
            get
            {
                return DateTimeUtil.WeekName(Date, 0);
            }
        }
        
        public string WeekNumber
        {
            get
            {
                var startDate = new DateTime(2011, 12, 25);
                var span = Date.Date - startDate;
                var daysDiff = span.TotalDays + 6;
                int week = Convert.ToInt16(Math.Floor(daysDiff / 7));
                return week.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        public DateTime WeekStartDate
        {
            get
            {
                var startOfWeek = DateTimeUtil.GetStartOfWeek(Date, 0);
                return startOfWeek;
            }
        }


        public List<ShiftPlanPerson> People { get; set; }
        public List<ShiftPlanEquipment> Equipment { get; set; }

        public Approval WeeklyApproval { get; set; }
        public Approval DailyApproval { get; set; }

        public List<Acknowledgement> Acknowledgements;

        public List<ShiftPlanAttachment> Attachments { get; set; }
        public List<ShiftPlanChecklistQuestion> Checklist { get; set; }
        public static string ShiftPlanId(string processId, DateTime? date, string shift)
        {
            return String.Join("-", "shiftplans", processId, (date ?? DateTime.MinValue).ToString("yyyy-MM-dd"), shift);
        }

        public static string ExtractProcessId(string shiftPlanId)
        {
            return String.Join("-", shiftPlanId.Split('-').Skip(1).Take(2).ToArray());
        }

        public static string ExtractLocationId(string shiftPlanId)
        {
            return String.Join("-", shiftPlanId.Split('-').Skip(1).Take(2).ToArray());
        }

        public static string RemoveProcessId(string shiftPlanId)
        {
            var splits = shiftPlanId.Split('-');
            return new StringBuilder().Append(splits[0]).Append('-').Append(splits[3]).Append('-').Append(splits[4]).Append('-').Append(splits[5]).Append('-').Append(splits[6]).ToString();
        }

        public static string ReplaceProcessId(string shiftPlanId, string processId)
        {
            var splits = shiftPlanId.Split('-');
            return new StringBuilder().Append(splits[0]).Append('-').Append(processId).Append('-').Append(splits[3]).Append('-').Append(splits[4]).Append('-').Append(splits[5]).Append('-').Append(splits[6]).ToString();
        }

        public static DateTime ExtractDate(string shiftPlanId)
        {
            return DateTime.Parse(String.Join("-", shiftPlanId.Split('-').Skip(3).Take(3).ToArray()));
        }

        public static string ExtractShift(string shiftPlanId)
        {
            var splits = shiftPlanId.Split('-');
            return splits[6];
        }

        public static string RemoveShift(string shiftPlanId)
        {
            return String.Join("-", shiftPlanId.Split('-').Take(6).ToArray());
        }

        #region Properties for report

        public string GetFormattedDate()
        {
            return String.Format("{0} {1}{2} {3} {4}", Date.ToString("ddd"), Date.Day, DateTimeUtil.GetSuffix(Date), Date.ToString("MMM"), Date.Year);
        }

        #endregion

    }
}