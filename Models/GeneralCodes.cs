namespace kt_system.Models
{
    public class GeneralCode
    {
        public int GeneralCodeID { get; set; }
        public string? Code { get; set; }
        public string? CodeCategory { get; set; }
        public short? CodeCategoryID { get; set; }
        public string? Description { get; set; }
        public string? Description_2 { get; set; }
        public string? Description_3 { get; set; }
        public string? Description_4 { get; set; }
        public string? Description_5 { get; set; }
        public string? Remarks { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int? CodeNum { get; set; }
    }
}