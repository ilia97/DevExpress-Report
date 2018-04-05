namespace FewzionReport
{
    public class TrackedResource
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public decimal Required { get; set; }
        public decimal Available { get; set; }

        public bool RequiredOrAvailable
        {
            get
            {
                return Required > 0 || Available > 0;
            }
        }
    }
}