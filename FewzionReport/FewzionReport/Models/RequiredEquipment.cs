namespace FewzionReport.Models
{
    public class RequiredEquipment
    {
        public string Id { get; set; }
        public string EquipmentType { get; set; } // The Id of the Equipment Type
        
        public string EquipmentTypeShortCode { get; set; }
        public decimal Count { get; set; }
    }
}