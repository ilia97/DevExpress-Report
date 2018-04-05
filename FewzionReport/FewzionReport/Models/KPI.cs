using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FewzionReport.Models
{
    public class KPI
    {
        public KPI()
        {
            TargetDependencies = new List<string>();
            ActualDependencies = new List<string>();
        }
        public string Id { get; set; }
        /// <summary>
        /// A descriptive name for the KPI.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A unique short code to identify this KPI.
        /// </summary>
        public string ShortCode { get; set; }
        /// <summary>
        /// A unique varible name to identify this KPI within calculation expressions.
        /// </summary>
        public string VariableName { get; set; }
        /// <summary>
        /// A detailed description of the KPI's actual value.
        /// </summary>
        public string ActualDescription { get; set; }
        /// <summary>
        /// A detailed description of the KPI's target value.
        /// </summary>
        public string TargetDescription { get; set; }
        /// <summary>
        /// Indicates if it is best to have a greater actual value.
        /// </summary>
        public bool GreaterActualIsBest { get; set; }
        /// <summary>
        /// The data type used for storing target and actual values. E.g. Numeric.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The field type used for target and actual input. E.g. Numeric, Time.
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// The method used for transforming a field type to its underlying data type. E.g. MinutesFromStart, MinutesFromEnd.
        /// </summary>
        public string TransformMethod { get; set; }
        /// <summary>
        /// Indicates that the target value is calculated automatically.
        /// </summary>
        public bool TargetCalculated { get; set; }
        /// <summary>
        /// Indicates that the actual value is calculated automatically.
        /// </summary>
        public bool ActualCalculated { get; set; }
        /// <summary>
        /// Indicates that the citect value is calculated automatically.
        /// </summary>
        public bool CitectCalculated { get; set; }
        /// <summary>
        /// Indicates that the KPI is visible on all screens and reports.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Indicates that the KPI is visible on the weekly schedule summary.
        /// </summary>
        public bool VisibleOnWeeklyScheduleSummary { get; set; }
        /// <summary>
        /// The function used for aggregating target and actual values. E.g. Sum, Average, TargetAchievedCount
        /// </summary>
        public string AggregateFunction { get; set; }
        /// <summary>
        /// A custom expression for aggregating target and actual values.
        /// </summary>
        public string AggregateExpression { get; set; }
        /// <summary>
        /// Indicates if zero values should be ignored from the aggregate calculation.
        /// </summary>
        public bool? AggregateIgnoreZeros { get; set; }
        /// <summary>
        /// Indicates if zero values should be hidden.
        /// </summary>
        public bool HideZeros { get; set; }
        /// <summary>
        /// The minimum allowed value.
        /// </summary>
        public int? MinValue { get; set; }
        /// <summary>
        /// The maximum allowed value.
        /// </summary>
        public int? MaxValue { get; set; }
        /// <summary>
        /// The minimum allowed digits before the decimal place.
        /// </summary>
        public int MinWholeDigits { get; set; }
        /// <summary>
        /// The maximum allowed digits before the decimal place.
        /// </summary>
        public int MaxWholeDigits { get; set; }
        /// <summary>
        /// The minimum allowed digits after the decimal place.
        /// </summary>
        public int MinDecimalDigits { get; set; }
        /// <summary>
        /// The maximum allowed digits after the decimal place.
        /// </summary>
        public int MaxDecimalDigits { get; set; }
        /// <summary>
        /// The order to display this KPI.
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Indicates if this KPI is active.
        /// </summary>
        public bool Active { get; set; }
        
        public bool Deleted { get; set; }
        
        public List<string> TargetDependencies { get; set; }
        public List<string> ActualDependencies { get; set; }

        /// <summary>
        /// The expression for calculating KPI target values.
        /// </summary>
        public string TargetCalculationExpression { get; set; }
        /// <summary>
        /// The expression for calculating KPI actual values.
        /// </summary>
        public string ActualCalculationExpression { get; set; }

        #region Methods

        public string ToString()
        {
            return new StringBuilder(128)
                .Append(Name)
                .Append(" (")
                .Append(ShortCode)
                .Append(")")
                .ToString();
        }

        public string GenerateVariableName()
        {
            var variableName = Name.Replace("_", " ").Replace("'", "").ToLower();

            variableName = Regex.Replace(variableName, "\\b\\w", match => match.Value.ToUpper());
            variableName = Regex.Replace(variableName, "\\W+", "");
            variableName = Regex.Replace(variableName, "^\\w", match => match.Value.ToLower());

            return "_" + variableName;
        }

        public static string GetVariableNameSuffix(string variableName)
        {
            return variableName.EndsWith("Target") ? "Target" : "Actual";
        }

        public string GetCalculationExpression(string property)
        {
            if (property == "Target") return TargetCalculationExpression;
            if (property == "Actual") return ActualCalculationExpression;
            throw new ArgumentException("Invalid property", "property");
        }

        public List<string> GetDependencies(string property)
        {
            if (property == "Target") return TargetDependencies;
            if (property == "Actual") return ActualDependencies;
            throw new ArgumentException("Invalid property", "property");
        }

        #endregion

        #region IDeletable

        public void Delete()
        {
            Deleted = true;
        }

        public void Undelete()
        {
            Deleted = false;
        }

        public bool IsDeleted()
        {
            return Deleted;
        }

        #endregion
    }
}