using FewzionReport.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FewzionReport.Models
{
    public class ShiftPlan_Report
    {
        public ShiftPlan_Report()
        {
            KPIs = new List<AppliedKPI>();
            People = new List<ShiftPlanPerson_Report>();
            Equipment = new List<ShiftPlanEquipment>();
            Tasks = new List<ShiftPlanTask_Report>();
            Acknowledgements = new List<Acknowledgement_Report>();
        }

        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string CrewLeaderLabel { get; set; }
        public string Id { get; set; }
        public string Process { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Crew { get; set; }
        public decimal ShiftLength { get; set; }
        public string DefaultCrewLeader { get; set; }
        public string ActualsCrewLeader { get; set; }
        public string ActualsCrewLeaderName { get; set; }
        public string CrewLeader { get; set; }
        public string SafetyNotes { get; set; }
        public string ShiftNotes { get; set; }
        public string ShiftReport { get; set; }
        public string SafetyReport { get; set; }
        public string UnscheduledWorkCompleted { get; set; }
        public string SuggestedWorkAndNotifications { get; set; }
        public string ShadowTasksWarning { get; set; }
        public List<ShiftPlanChecklistQuestion> Checklist { get; set; }
        public List<AppliedKPI> KPIs { get; set; }
        public List<ShiftPlanPerson_Report> People { get; set; }
        public List<ShiftPlanEquipment> Equipment { get; set; }
        public List<ShiftPlanTask_Report> Tasks { get; set; }

        public bool EnableShiftPlanAcknowledgementBySchedule { get; set; }
        public bool EnableShiftPlanAcknowledgementByShiftPlan { get; set; }
        public bool EnableShiftPlanAcknowledgement { get; set; }
        public List<Acknowledgement_Report> Acknowledgements { get; set; }

        public decimal EffectiveShiftLength { get; set; }

        public string GetFormattedDate()
        {
            return String.Format("{0} {1}{2} {3} {4}", Date.ToString("ddd"), Date.Day, DateTimeUtil.GetSuffix(Date), Date.ToString("MMM"), Date.Year);
        }

        public string CrewLeaderOrDefaultCrewLeader
        {
            get { return !String.IsNullOrEmpty(CrewLeader) ? CrewLeader : DefaultCrewLeader; }
        }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime
        {
            get
            {
                return StartDateTime.AddHours((double)ShiftLength);
            }
        }

        // ReSharper disable once InconsistentNaming
        public List<AppliedKPI> GetVisibleKPIs()
        {
            // ReSharper disable InconsistentNaming
            var visibleKPIs = new List<AppliedKPI>();
            // ReSharper restore InconsistentNaming
            var kpiRules = FewzionReport.Models.KPIs.GetKPIs();
            foreach (var kpi in KPIs)
            {
                var rule = kpiRules.FirstOrDefault(x => x.ShortCode == kpi.ShortCode);
                if (rule == null || !rule.Visible) continue;
                var clone = (AppliedKPI)kpi.Clone();
                if (rule.FieldType == "Time")
                {
                    if (clone.Target != null && clone.Target > -1000000000)
                    {
                        if (rule.TransformMethod == "MinutesFromStart")
                        {
                            clone.TargetString = StartDateTime.AddMinutes((double)clone.Target).TimeOfDay.ToString(@"hh\:mm");
                        }
                        if (rule.TransformMethod == "MinutesFromEnd")
                        {
                            clone.TargetString = EndDateTime.AddMinutes(0 - (double)clone.Target).TimeOfDay.ToString(@"hh\:mm");
                        }
                    }

                    if (clone.Actual != null && clone.Actual > -1000000000)
                    {
                        if (rule.TransformMethod == "MinutesFromStart")
                        {
                            clone.ActualString = StartDateTime.AddMinutes((double)clone.Actual).TimeOfDay.ToString(@"hh\:mm");
                        }
                        if (rule.TransformMethod == "MinutesFromEnd")
                        {
                            clone.ActualString = EndDateTime.AddMinutes(0 - (double)clone.Actual).TimeOfDay.ToString(@"hh\:mm");
                        }
                    }
                }
                else
                {
                    var maxDecimals = rule.MaxDecimalDigits;
                    if (clone.Actual != null && clone.Actual > -1000000000)
                    {
                        var actual = Math.Round((decimal)clone.Actual, maxDecimals);
                        if (rule.FieldType == "Percentage") clone.ActualString = actual + "%";
                        else clone.Actual = actual;
                    }
                    if (clone.Target != null && clone.Target > -1000000000)
                    {
                        var target = Math.Round((decimal)clone.Target, maxDecimals);
                        if (rule.FieldType == "Percentage") clone.TargetString = target + "%";
                        else clone.Target = target;
                    }

                }
                visibleKPIs.Add(clone);
            }
            return visibleKPIs;
        }

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

        public string WeekName
        {
            get
            {
                return DateTimeUtil.WeekName(Date, 0);
            }
        }

        public static IEnumerable<TrackedResource> GetTrackedResources(ShiftPlan_Report shiftPlan)
        {
            return GetTrackedResources(new[] { shiftPlan });
        }

        public static IEnumerable<TrackedResource> GetTrackedResources(IList<ShiftPlan_Report> shiftPlans)
        {
            var peopleResources = new Dictionary<string, TrackedResource>();
            var equipmentResources = new Dictionary<string, TrackedResource>();

            foreach (var sp in shiftPlans)
            {
                if (sp.EffectiveShiftLength > 0)
                {
                    foreach (var t in sp.Tasks.Where(t => !t.Priority.Contains("C") && t.Duration > 0))
                    {
                        foreach (var rp in t.RequiredPeople.Where(rp => rp.Count > 0))
                        {
                            var trackedResource = peopleResources.GetOrAdd(rp.OccupationTypeShortCode,
                                // ReSharper disable once AccessToForEachVariableInClosure
                                () => new TrackedResource { Category = "P", Type = rp.OccupationTypeShortCode });
                            trackedResource.Required += rp.Count * (t.Duration ?? 0) / sp.EffectiveShiftLength;
                        }
                        foreach (var re in t.RequiredEquipment.Where(re => re.Count > 0))
                        {
                            var trackedResource = equipmentResources.GetOrAdd(re.EquipmentTypeShortCode,
                                // ReSharper disable once AccessToForEachVariableInClosure
                                () => new TrackedResource { Category = "E", Type = re.EquipmentTypeShortCode });
                            trackedResource.Required += re.Count * (t.Duration ?? 0) / sp.EffectiveShiftLength;
                        }
                    }
                }

                // ReSharper disable once LoopCanBePartlyConvertedToQuery
                foreach (var occupationShortCode in
                    sp.People.Where(p => p.Available && !String.IsNullOrEmpty(p.OccupationShortCode)).Select(p => p.OccupationShortCode))
                {
                    var trackedResource = peopleResources.GetOrAdd(occupationShortCode,
                        // ReSharper disable once AccessToForEachVariableInClosure
                        () => new TrackedResource { Category = "P", Type = occupationShortCode });
                    trackedResource.Available++;
                }

                // ReSharper disable once LoopCanBePartlyConvertedToQuery
                foreach (var type in
                    sp.Equipment.Where(e => e.Available && !String.IsNullOrEmpty(e.Type)).Select(e => e.Type))
                {
                    var trackedResource = equipmentResources.GetOrAdd(type,
                        // ReSharper disable once AccessToForEachVariableInClosure
                        () => new TrackedResource { Category = "E", Type = type });
                    trackedResource.Available++;
                }
            }

            return peopleResources.Values
                .Concat(equipmentResources.Values).ToList();
        }

    }
}