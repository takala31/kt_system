namespace kt_system.Models
{
    public class Employment
    {
        public int EmploymentID { get; set; }
        public int CustomerID { get; set; }
        public string? Employer { get; set; }
        public string? EmployerCatg { get; set; }
        public string? EmployerActivity { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal? LOS { get; set; }
        public string? BankingWith { get; set; }
        public string? AccountNumber { get; set; }
    }
}