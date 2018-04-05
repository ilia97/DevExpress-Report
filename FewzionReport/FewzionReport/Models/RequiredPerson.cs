namespace FewzionReport.Models
{
    public class RequiredPerson
    {
        public string Id { get; set; }
        public string OccupationType { get; set; } // The Id of the Occupation Type
        
        public string OccupationTypeShortCode { get; set; }
        public decimal Count { get; set; }
    }
}