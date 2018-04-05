using System;
using System.Collections.Generic;
using System.Linq;

namespace FewzionReport.Models
{
    public class Units
    {
        private static Func<Units> _valueFactory = () => new Units();

        private static Lazy<Units> _instance = new Lazy<Units>(_valueFactory);

        private readonly IList<Unit> _units;

        public Units() : this(new List<Unit>())
        {
        }

        public Units(IList<Unit> units)
        {
            _units = units;
        }

        public static void Reset()
        {
            _instance = new Lazy<Units>(_valueFactory);
        }

        public static void Reset(Func<Units> valueFactory)
        {
            _valueFactory = valueFactory;
            Reset();
        }

        public static IList<Unit> GetUnits()
        {
            return _instance.Value._units;
        }

        public static Unit GetUnitByName(string name)
        {
            return GetUnits().FirstOrDefault(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public static Unit GetUnitByShortCode(string shortCode)
        {
            return GetUnits().FirstOrDefault(x => String.Equals(x.ShortCode, shortCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}