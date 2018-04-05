using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FewzionReport.Models
{
    public class ShiftPlanTask_Report
    {
        public ShiftPlanTask_Report()
        {
            RequiredPeople = new List<RequiredPerson>();
            RequiredEquipment = new List<RequiredEquipment>();
            AssignedPeople = new List<TaskResource>();
            AssignedEquipment = new List<TaskResource>();
            Locations = new List<TaskLocation>();
            Tags = new List<AppliedTag>();
            Actuals = new List<TaskActual>();
        }

        public string TargetAndUnit
        {
            get
            {
                if (Target == null)
                    return String.Empty;
                if (String.IsNullOrEmpty(Unit))
                    return Target.ToString();
                return String.Concat(Target.ToString(), Environment.NewLine, Unit);
            }
        }

        public string TargetAndUnitShortCode
        {
            get
            {
                if (Target == null)
                    return String.Empty;
                if (String.IsNullOrEmpty(Unit))
                    return Target.ToString();
                var unit = Units.GetUnitByName(Unit);
                if (unit == null)
                    return Target.ToString();
                return String.Concat(Target.ToString(), unit.ShortCode);
            }
        }

        public string ActualAndUnitShortCode
        {
            get
            {
                if (Actuals.Count() == 0)
                    return String.Empty;
                if (String.IsNullOrEmpty(Unit))
                {
                    return Actuals.OrderBy(x => x.Interval).Last().Actual.ToString();
                }
                var unit = Units.GetUnitByName(Unit);
                if (unit == null)
                {
                    return Actuals.OrderBy(x => x.Interval).Last().Actual.ToString();
                }
                return String.Concat(Actuals.OrderBy(x => x.Interval).Last().Actual.ToString(), unit.ShortCode);
            }
        }

        public string Actual
        {
            get
            {
                if (Actuals.Count() == 0)
                    return String.Empty;
                return Actuals.OrderBy(x => x.Interval).Last().Actual.ToString();
            }
        }

        public string GetTaskPeopleStr()
        {
            var result = String.Join(", ", AssignedPeople.Select(tr => tr.Name + " [" + tr.ShortCode + "]"));

            if (!String.IsNullOrEmpty(result)) result = "People: " + result;

            return result;
        }

        public string GetTaskEquipmentStr()
        {
            var result = String.Join(", ", AssignedEquipment.Select(tr => tr.Name + " [" + tr.ShortCode + "]"));

            if (!String.IsNullOrEmpty(result)) result = "Equipment: " + result;

            return result;
        }

        public string TypeShortCode { get; set; }

        static Regex digits = new Regex(@"\d");
        static Regex nondigits = new Regex(@"\D");

        public string Id { get; set; }
        public string TypeName { get; set; }
        public string TypeColor { get; set; }
        public int TypeOrder { get; set; }
        public string Process { get; set; }
        public bool? AddedBySystem { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public bool Contingency { get; set; }
        public string WorkOrder { get; set; }
        public string Type { get; set; }
        public decimal? Duration { get; set; }
        public decimal? Target { get; set; }
        public List<TaskActual> Actuals { get; set; }
        public string Unit { get; set; }
        public string LocationId { get; set; }
        public string Location { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedProcess { get; set; }
        public string Instructions { get; set; }
        public string ShiftNotes { get; set; }
        public string ShiftEndNotes { get; set; }
        public List<RequiredPerson> RequiredPeople { get; set; }
        public List<RequiredEquipment> RequiredEquipment { get; set; }
        public string StandardTaskId { get; set; }  // The id of the std task used to create this one - null if not from a std task
        public Guid? RecurrenceId { get; set; }  // The id of the recurrence used to create this one - null if not from a recurrence
        public bool? Changed { get; set; }  // Signals that the task changed since creation (used by recurrence rescheduling)
        public bool? Planned { get; set; } // Flag to indicate that the task is a planned task
        public DateTime? OriginalScheduledDate { get; set; }  // The date on which the task was originally scheduled (used by recurrence rescheduling)
        public string OriginalScheduledShift { get; set; }  // The shift on which the task was originally scheduled (used by recurrence rescheduling)
        public DateTime? ScheduledDate { get; set; }  // The date on which the task is scheduled
        public string ScheduledShift { get; set; }  // The shift on which the task is scheduled
        public string Status { get; set; }  // Pending, Approved, Rejected
        public string IncompleteTaskNotes { get; set; }
        public int? CompletionPercentage { get; set; }
        public List<TaskResource> AssignedPeople { get; set; }
        public List<TaskResource> AssignedEquipment { get; set; }
        public int? DueDaysAfter { get; set; }
        public string Group { get; set; }
        public int? DisplayOrder { get; set; }
        public string StoneDustingZone { get; set; } // If Task is of Type Stone Dusting or Stone Dust Sampling, the zone is specified here
        public DateTime? CompletionTimestamp { get; set; } // Set at the time the Process Superintendent verifies the task as completed
        public string CompletionVerifiedBy { get; set; } // The Id of the Process Superintendent that verified the task as completed
        public decimal? StoneDustKilograms { get; set; }
        public DateTime? StoneDustKilogramsVerifiedTimestamp { get; set; } // Set at the time the Stone Dust Coordinator verifies the kilos as correct
        public string StoneDustKilogramsVerifiedBy { get; set; } // The Id of the Stone Dust Coordinator that verified the kilos as correct
        public string StoneDustSampleId { get; set; }
        public bool Important { get; set; }
        public bool InspectionRequired { get; set; }
        public string WeekPlan { get; set; } // The Hi/Lo id of the Week Plan that this task belongs to
        public List<TaskLocation> Locations { get; set; }
        public bool Shadow { get; set; }
        public string Source { get; set; }
        public List<AppliedTag> Tags { get; set; }
        public DateTime StartTime { get; set; }
    }
}