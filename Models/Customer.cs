namespace kt_system.Models
{
    public class Customer
    {
        public int? CustomerID { get; set; }
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? AraName { get; set; }       
        public string? Nationality { get; set; }
        public string? CustomerType { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
    }
}
























































//namespace kt_system.Models
//{
//    public class Customer
//    {
//        public int CustomerID { get; set; }

//        // Personal Information
//        public string? FirstName { get; set; }
//        public string? MiddleName { get; set; }
//        public string? LastName { get; set; }
//        public string? AraName { get; set; }

//        public DateTime? DOB { get; set; }

//        public string? Gender { get; set; }
//        public string? MaritalStatus { get; set; }
//        public string? Nationality { get; set; }

//        public string? PreferredLanguage { get; set; }

//        // Emirates ID
//        public string? EmiratesID { get; set; }
//        public DateTime? EmiratesIDExpiry { get; set; }

//        // Passport
//        public string? PassportNumber { get; set; }
//        public DateTime? PassportExpiry { get; set; }

//        // Contact Details
//        public string? Mobile { get; set; }
//        public string? AlternateMobile { get; set; }
//        public string? Email { get; set; }

//        // Address
//        public string? Country { get; set; }
//        public string? Emirate { get; set; }
//        public string? City { get; set; }
//        public string? Area { get; set; }

//        public string? AddressLine1 { get; set; }
//        public string? AddressLine2 { get; set; }

//        public string? POBox { get; set; }

//        // Status
//        public string? CustomerStatus { get; set; }

//        // Audit
//        public string? CreatedBy { get; set; }
//        public DateTime? CreatedDate { get; set; }

//        public string? ModifiedBy { get; set; }
//        public DateTime? ModifiedDate { get; set; }
//    }
//}