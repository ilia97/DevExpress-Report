using System;
using System.Collections.Generic;
using System.Linq;

namespace FewzionReport.Models
{
    public class KPIs
    {
        private static Func<KPIs> _valueFactory = () => new KPIs();

        static Lazy<KPIs> _instance = new Lazy<KPIs>(_valueFactory);

        readonly Dictionary<string, KPI> _shortCodes;

        readonly Dictionary<string, KPI> _variableNames;

        readonly Dictionary<string, int> _displayOrder;

        public KPIs()
            : this(new List<KPI>())
        {
        }

        public KPIs(IList<KPI> kpis)
        {
            if (kpis == null) throw new ArgumentNullException("kpis");

            _shortCodes = kpis.ToDictionary(k => k.ShortCode);

            // Build a dictionary of [variable names -> KPIs] for both "Target" and "Actual" suffixes. E.g. "_timeToFirstCoalTarget" and "_timeToFirstCoalActual".
            _variableNames = kpis.SelectMany(k => new[] { k.VariableName + "Target", k.VariableName + "Actual" }, (k, v) => new { KPI = k, VariableName = v })
                .ToDictionary(k => k.VariableName, k => k.KPI);

            _displayOrder = kpis.ToDictionary(k => k.ShortCode, k => k.DisplayOrder);

            foreach (var kpi in kpis)
            {
                if (!string.IsNullOrEmpty(kpi.TargetCalculationExpression))
                    kpi.TargetDependencies = ExtractIdentifiers(kpi.TargetCalculationExpression, "Target");

                if (!string.IsNullOrEmpty(kpi.ActualCalculationExpression))
                    kpi.ActualDependencies = ExtractIdentifiers(kpi.ActualCalculationExpression, "Actual");
            }
        }

        private List<string> ExtractIdentifiers(string calculationExpression, string property)
        {
            var tokens = new List<string>();
            var identifiers = new HashSet<string>(); // Use HashSet to ensure uniqueness.
            foreach (var identifier in tokens)
            {
                if (_variableNames.ContainsKey(identifier)) // Identifier matches variable name (with a suffix).
                {
                    identifiers.Add(identifier);
                }
                else if (_variableNames.ContainsKey(identifier + property)) // Identifier matches variable name (without a suffix).
                {
                    identifiers.Add(identifier + property);
                }
            }
            return identifiers.ToList();
        }

        public static Dictionary<string, KPI> VariableNames
        {
            get
            {
                return _instance.Value._variableNames;
            }
        }

        public static Dictionary<string, int> DisplayOrder
        {
            get
            {
                return _instance.Value._displayOrder;
            }
        }

        public static void Reset()
        {
            _instance = new Lazy<KPIs>(_valueFactory);
        }

        public static void Reset(Func<KPIs> valueFactory)
        {
            _valueFactory = valueFactory;
            Reset();
        }

        // ReSharper disable InconsistentNaming
        public static IList<KPI> GetKPIs()
        // ReSharper restore InconsistentNaming
        {
            return _instance.Value._shortCodes.Values.ToList();
        }

        // ReSharper disable InconsistentNaming
        public static KPI GetKPI(string shortCode)
        // ReSharper restore InconsistentNaming
        {
            //return GetKPIs().FirstOrDefault(kpi => kpi.ShortCode == shortCode);
            return _instance.Value._shortCodes.GetValueOrDefault(shortCode);
        }

        // ReSharper disable InconsistentNaming
        public static KPI GetKPIByVariableName(string variableName)
        // ReSharper restore InconsistentNaming
        {
            return VariableNames.GetValueOrDefault(variableName);
        }

        // ReSharper disable InconsistentNaming
        public static int GetDisplayOrder(string shortCode)
        // ReSharper restore InconsistentNaming
        {
            return DisplayOrder.GetValueOrDefault(shortCode, int.MaxValue);
        }
    }
}