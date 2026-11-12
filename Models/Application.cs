
namespace kt_system.Models
{
    public class Application
    {
        // Application Information
        public int ApplicationID { get; set; }
        public string? AppID_usr { get; set; }
        public DateTime AppDate_usr { get; set; }= DateTime.Today;
        public string? AppProduct { get; set; }
        public string? Stage { get; set; }= "DDEQC";

        // References
        public int? CustomerID { get; set; }
        public int? PropertyID { get; set; }

        // Finance Details
        public decimal? OriginalPrice { get; set; }
        public decimal? Downpayment { get; set; }
        public decimal? NFA { get; set; }
        public decimal? ProfitRate { get; set; }
        public int? Tenor { get; set; }
        public decimal? DSR { get; set; }
        public string? SalesName { get; set; }
        public string? Remarks { get; set; }

        // Audit Fields
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}