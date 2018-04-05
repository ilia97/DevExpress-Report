using System;
using System.Collections.Generic;

namespace FewzionReport.Models
{
    public class Task
    {
        public Task()
        {
            RequiredPeople = new List<RequiredPerson>();
            RequiredEquipment = new List<RequiredEquipment>();
            AssignedProcessRequiredPeople = new List<RequiredPerson>();
            AssignedProcessRequiredEquipment = new List<RequiredEquipment>();
            AssignedPeople = new List<AssignedPerson>();
            AssignedEquipment = new List<AssignedEquipment>();
            Locations = new List<TaskLocation>();
            Attachments = new List<Attachment>();
            Tags = new List<AppliedTag>();
            Actuals = new List<TaskActual>();
        }
        
        public int? PlannedStartTimeOffset { get; set; }
        
        public int? ActualEndTimeOffset { get; set; }
        
        public int? ActualStartTimeOffset { get; set; }
        
        public bool? ShortIntervalControlRequired { get; set; }
        public string ShortIntervalControl { get; set; }
        
        public List<TaskActual> Actuals { get; set; }
        
        public string Process { get; set; }
        
        public bool? AddedBySystem { get; set; }
        
        public string Priority { get; set; }
        
        public string Description { get; set; }
        
        public bool? Contingency { get; set; }
        
        public string WorkOrder { get; set; }
        
        public string Type { get; set; }
        
        public decimal? Duration { get; set; }
        
        public decimal? Target { get; set; }
        
        public string Unit { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public string AssignedProcess { get; set; }
        
        public string Instructions { get; set; }
        
        public bool? InstructionsAcknowledged { get; set; }
        
        public string ShiftNotes { get; set; }
        
        public string ShiftEndNotes { get; set; }
        /// <summary>
        /// A collection specifying the number and type of People (by Occupation type) required to do the Task.
        /// </summary>
        public List<RequiredPerson> RequiredPeople { get; set; }
        /// <summary>
        /// A collection specifying the number and type of Equipment (by Equipment type) required to do the Task.
        /// </summary>
        public List<RequiredEquipment> RequiredEquipment { get; set; }
        /// <summary>
        /// A collection specifying the number and type of People (by Occupation type) required to do the Task, when the Task is used by another Process.
        /// </summary>
        public List<RequiredPerson> AssignedProcessRequiredPeople { get; set; }
        /// <summary>
        /// A collection specifying the number and type of Equipment (by Equipment type) required to do the Task, when the Task is used by another Process.
        /// </summary>
        public List<RequiredEquipment> AssignedProcessRequiredEquipment { get; set; }
        
        public string StandardTaskId { get; set; }
        
        public Guid? RecurrenceId { get; set; }
        
        public bool? Changed { get; set; }
        
        public DateTime? OriginalScheduledDate { get; set; }
        
        public string OriginalScheduledShift { get; set; }
        
        public DateTime? ScheduledDate { get; set; }
        
        public string ScheduledShift { get; set; }
        
        public bool? DefaultScheduledDate { get; set; } 
        
        public bool? DefaultScheduledShift { get; set; } 
        
        public string Status { get; set; } 
        
        public string IncompleteTaskNotes { get; set; }
        
        public int? CompletionPercentage { get; set; }
        
        public List<AssignedPerson> AssignedPeople { get; set; }
        
        public List<AssignedEquipment> AssignedEquipment { get; set; }
        
        public int? DueDaysAfter { get; set; }
        
        public string Group { get; set; }
        
        public DateTimeOffset? CompletionTimestamp { get; set; }
        
        public bool? Important { get; set; }
        
        public bool? InspectionRequired { get; set; }
        
        public bool? InspectionCompleted { get; set; }
        
        public string ShiftPlan { get; set; }
        
        public string AssignedShiftPlan { get; set; }
        
        public string WeekPlan { get; set; }
        
        public List<TaskLocation> Locations { get; set; }
        
        public string Source { get; set; }

        public string SourceId { get; set; }
        
        public List<Attachment> Attachments { get; set; }
        
        public List<AppliedTag> Tags { get; set; }
        
        public bool Deleted { get; set; }

        /// <summary>
        /// The shift notes as they stood before modification by Actuals.
        /// Effectively, these are the notes set at planning time.
        /// When a task is duplicated, the original notes go into the new tasks's notes fields. The idea is that notes applicable to actuals do not belong in the duplicated task.
        /// </summary>
        public string OriginalShiftNotes { get; set; }
        /// <summary>
        /// The shift end notes as they stood before modification by Actuals. See OriginalShiftNotes.
        /// </summary>
        public string OriginalShiftEndNotes { get; set; }

        public bool? Locked { set; get; }

        #region IDeletable
        // set the appropriate Property (eg Deleted) to true, or handle the deletion in some other way entirely (move the document somewhere else entirely)
        public void Delete()
        {
            Deleted = true;
        }

        // set the appropriate Property (eg Deleted) to false etc
        public void Undelete()
        {
            Deleted = false;
        }

        public bool IsDeleted()
        {
            return Deleted;
        }

        #endregion

        public static bool TaskMoved(Task task, out string from, out string to)
        {
            from = PositionName(task.OriginalScheduledDate.Value, task.OriginalScheduledShift);
            to = PositionName(task.ScheduledDate.Value, task.ScheduledShift);
            return !from.Equals(to);
        }

        public static string PositionName(DateTime date, string shift)
        {
            return String.Format("{0} {1}/S", date.ToShortDateString(), shift[0]);
        }

        
        public IList<AppliedTag> GetTags() { return Tags; }
        

    }

    public static class TaskExtensions
    {
        public static Dictionary<string, List<Task>> MapByShiftPlanId(this IEnumerable<Task> source)
        {
            var tasks = new Dictionary<string, List<Task>>();
            foreach (var task in source)
            {
                tasks.GetOrAdd(ShiftPlan.ShiftPlanId(task.Process, task.ScheduledDate, task.ScheduledShift), () => new List<Task>()).Add(task);
                if (!string.IsNullOrEmpty(task.AssignedProcess))
                {
                    tasks.GetOrAdd(ShiftPlan.ShiftPlanId(task.AssignedProcess, task.ScheduledDate, task.ScheduledShift), () => new List<Task>()).Add(task);
                }
            }
            return tasks;
        }
    }
}