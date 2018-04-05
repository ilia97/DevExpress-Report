namespace FewzionReport.Models
{
    public class DefaultKPI
    {
        public string Id { get; set; }
        public string Shift { get; set; }

        public string ShortCode { get; set; }
        public decimal? Target { get; set; }
        public bool Enabled { get; set; }
    }
}