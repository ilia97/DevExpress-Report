using System;
using System.Collections.Generic;
using System.Globalization;

namespace FewzionReport.Models
{
    public class AppliedKPI
    // ReSharper restore InconsistentNaming
    {
        public string Id { get; set; }
        /// <summary>
        /// Date to which this AppliedKPI applies.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Shift to which this AppliedKPI applies.
        /// </summary>
        public string Shift { get; set; }

        /// <summary>
        /// Process to which this AppliedKPI applies.
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// A ShortCode to identify this AppliedKPI. (unique for this Date, Shift and Process)
        /// </summary>
        public string ShortCode { get; set; }

        /// <summary>
        /// Type of AppliedKPI. (currently not in use)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Weekly snapshot of the Target value.
        /// </summary>
        public decimal? WeeklyPlanned { get; set; }

        /// <summary>
        /// Daily snapshot of the Target value.
        /// </summary>
        public decimal? DailyPlanned { get; set; }

        /// <summary>
        /// The Target value for this AppliedKPI.
        /// </summary>
        public decimal? Target { get; set; }

        /// <summary>
        /// The Actual value for this AppliedKPI.
        /// </summary>
        public decimal? Actual { get; set; }

        /// <summary>
        /// The Citect value for this AppliedKPI.
        /// </summary>
        public decimal? Citect { get; set; }

        /// <summary>
        /// Original Target value to determine if the Target has been changed by a user.
        /// </summary>
        public decimal? DefaultTarget { get; set; }

        /// <summary>
        /// Timestamp for when the Actual value was first added.
        /// </summary>
        public DateTime? ActualAddedTimestamp { get; set; }

        /// <summary>
        /// Timestamp for when the Actual value was last updated.
        /// </summary>
        public DateTime? ActualEditedTimestamp { get; set; }

        /// <summary>
        /// The order to display this AppliedKPI.
        /// </summary>
        public int DisplayOrder { get; set; }
        
        public bool Deleted { get; set; }
        
        public string ShiftPlan { get; set; }

        public static AppliedKPI CreateNew(string shortCode, decimal? target, string process, DateTime date, string shift, int displayOrder)
        {
            return new AppliedKPI
            {
                ShortCode = shortCode,
                Target = target,
                DefaultTarget = target,
                Process = process,
                Date = date,
                Shift = shift,
                DisplayOrder = displayOrder
            };
        }

        // ReSharper disable once InconsistentNaming
        public decimal? GetKPIValue(string property)
        {
            switch (property)
            {
                case "WeeklyPlanned":
                    return WeeklyPlanned;
                case "DailyPlanned":
                    return DailyPlanned;
                case "Target":
                    return Target;
                case "Actual":
                    return Actual;
                case "Citect":
                    return Citect;
                case "DefaultTarget":
                    return DefaultTarget;
            }
            throw new ArgumentException("Invalid property", "property");
        }

        public void SetKPIValue(decimal? value, string property)
        {
            switch (property)
            {
                case "WeeklyPlanned":
                    WeeklyPlanned = value;
                    break;
                case "DailyPlanned":
                    DailyPlanned = value;
                    break;
                case "Target":
                    Target = value;
                    break;
                case "Actual":
                    Actual = value;
                    break;
                case "Citect":
                    Citect = value;
                    break;
                case "DefaultTarget":
                    DefaultTarget = value;
                    break;
                default:
                    throw new ArgumentException("Invalid property", "property");
            }
        }

        private string _targetString;
        public string TargetString
        {
            get
            {
                return _targetString ?? GetTargetString();
            }
            set { _targetString = value; }
        }

        private string GetTargetString()
        {
            if (Target == null)
                return String.Empty;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (Target.Value <= -1000000000)
                return "N/A";
            return Target.Value.ToString("0.#############################", CultureInfo.InvariantCulture);
        }

        private string _actualString;
        public string ActualString
        {
            get
            {
                return _actualString ?? GetActualString();
            }
            set { _actualString = value; }
        }

        private string GetActualString()
        {
            if (Actual == null)
                return String.Empty;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (Actual.Value <= -1000000000)
                return "N/A";
            return Actual.Value.ToString("0.#############################", CultureInfo.InvariantCulture);
        }
        
        // ReSharper disable once InconsistentNaming
        public decimal VarianceAP
        {
            get
            {
                return Actual.GetValueOrDefault() - WeeklyPlanned.GetValueOrDefault();
            }
        }
        
        // ReSharper disable once InconsistentNaming
        public decimal VarianceAPPercentage
        {
            get
            {
                if (WeeklyPlanned == null || WeeklyPlanned == 0)
                    return 0;
                return Math.Abs(VarianceAP / (decimal)WeeklyPlanned);
            }
        }
        
        // ReSharper disable once InconsistentNaming
        public decimal VarianceCA
        {
            get
            {
                return Citect.GetValueOrDefault() - Actual.GetValueOrDefault();
            }
        }
        
        // ReSharper disable once InconsistentNaming
        public decimal VarianceCAPercentage
        {
            get
            {
                if (Actual == null || Actual == 0)
                    return 0;
                return Math.Abs(VarianceCA / (decimal)Actual);
            }
        }
        
        public string AggregateFunction { get; set; }

        #region Properties for report

        public static readonly string UnknownColor = "#FFFFFF"; // 255, 255, 255
        public static readonly string GreenColor = "#7EFF70"; // 126, 255, 112
        public static readonly string YellowColor = "#FFE561"; // 255, 229, 97
        public static readonly string RedColor = "#FF5D47"; // 255, 93, 71

        public string GetColor()
        {
            if (Actual == null || Target == null || Actual <= -1000000000 || Target <= -1000000000)
            {
                return UnknownColor;
            }

            var kpiRule = KPIs.GetKPI(ShortCode);

            if (kpiRule.GreaterActualIsBest || AggregateFunction == "TargetAchievedCount")
            {
                if (Actual >= Target)
                {
                    return GreenColor;
                }
                if (Actual >= Target * 0.8M)
                {
                    return YellowColor;
                }
            }
            else
            {
                if (Actual <= Target)
                {
                    return GreenColor;
                }
                if (Actual <= Target * 1.2M)
                {
                    return YellowColor;
                }
            }

            return RedColor;
        }

        #endregion

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

        #region ICloneable

        public object Clone()
        {
            return new AppliedKPI
            {
                Id = Id,
                Date = Date,
                Shift = Shift,
                Process = Process,
                ShortCode = ShortCode,
                Type = Type,
                WeeklyPlanned = WeeklyPlanned,
                DailyPlanned = DailyPlanned,
                Target = Target,
                Actual = Actual,
                Citect = Citect,
                DefaultTarget = DefaultTarget,
                ActualAddedTimestamp = ActualAddedTimestamp,
                ActualEditedTimestamp = ActualEditedTimestamp,
                DisplayOrder = DisplayOrder,
                Deleted = Deleted
            };
        }

        #endregion

    }

    public static class AppliedKPIExtensions
    {
        public static Dictionary<string, List<AppliedKPI>> MapByShiftPlanId(this IEnumerable<AppliedKPI> source)
        {
            var appliedKPIs = new Dictionary<string, List<AppliedKPI>>();
            foreach (var appliedKPI in source)
            {
                appliedKPIs.GetOrAdd(ShiftPlan.ShiftPlanId(appliedKPI.Process, appliedKPI.Date, appliedKPI.Shift), () => new List<AppliedKPI>()).Add(appliedKPI);
            }
            return appliedKPIs;
        }
    }
}