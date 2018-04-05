namespace FewzionReport.Models
{
    public class AppliedTag
    {
        public string Id { get; set; }
        
        public string TagId { get; set; }

        public string Tag { get; set; }
        
        public bool ManuallyAdded { get; set; }
    }
}