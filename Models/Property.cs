namespace KT_System.Models
{
    public class Property
    {
        public int PropertyID { get; set; }

        // Property Details
        public string? PropertyType { get; set; }
        public string? PropertyDesc { get; set; }
        public string? MortgageDegree { get; set; }
        public string? MasterDevelopmentName { get; set; }
        public string? MasterDeveloperName { get; set; }
        public string? ProjectName { get; set; }
        public string? DeveloperName { get; set; }
        public string? BuildingName { get; set; }

        public decimal? OriginalPropertyValue { get; set; }
        public decimal? AppraisalValue { get; set; }

        public string? AppraisalBy { get; set; }
        public DateTime? AppraisalDate { get; set; }

        public string? PropertyStatus { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        // Registration Details
        public string? LotNo { get; set; }
        public string? PlotNo { get; set; }
        public string? TitleDeedNo { get; set; }

        public decimal? LandArea { get; set; }
        public decimal? BuiltupArea { get; set; }
        public decimal? PropertyValuePerUnit { get; set; }

        public string? RegistrationType { get; set; }
        public string? RegistrationNumber { get; set; }

        public string? RegisteredOnNameOf { get; set; }
        public string? Relation { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string? IssuedFrom { get; set; }

        // Address
        public string? VillaAptNo { get; set; }
        public string? BuildingCompoundName { get; set; }
        public string? StreetName { get; set; }

        public string? Country { get; set; }
        public string? Emirate { get; set; }
        public string? AreaName { get; set; }
    }
}

//namespace KT_System.Models
//{
//    public class Property

//    {
//        public int PropertyID { get; set; }
//        public string? PropertyType { get; set; }
//        public string? PropertyDesc { get; set; }
//        public string? MortgageDegree { get; set; }
//        public string? MasterDevelopmentName { get; set; }
//        public string? MasterDeveloperName { get; set; }
//        public string? ProjectName { get; set; }
//        public string? DeveloperName { get; set; }
//        public string? BuildingName { get; set; }
//        public decimal? OriginalPropertyValue { get; set; }
//        public decimal? AppraisalValue { get; set; }
//        public string? AppraisalBy { get; set; }
//        public DateTime? AppraisalDate { get; set; }
//        public string? PropertyStatus { get; set; }
//        public string? LotNo { get; set; }
//        public string? PlotNo { get; set; }
//        public string? TitleDeedNo { get; set; }
//        public string? Country { get; set; }
//        public string? Emirate { get; set; }
//        public string? AreaName { get; set; }
//        public string? StreetName { get; set; }
//        public string? VillaAptNo { get; set; }

//    }
//}