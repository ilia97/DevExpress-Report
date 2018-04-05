using System.Text;

namespace FewzionReport.Models
{
    public class Unit
    {
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public override string ToString()
        {
            return new StringBuilder(128)
                .Append(Name)
                .Append(" (")
                .Append(ShortCode)
                .Append(")")
                .ToString();
        }
    }
}