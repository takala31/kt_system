using kt_system.Helpers;
using kt_system.Models;
using KT_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace kt_system.Pages.Applications
{
    public class EditModel : PageModel
    {
        //Application information
        [BindProperty] public Application App { get; set; } = new();
        [BindProperty] public Customer Cust { get; set; } = new();
        [BindProperty] public Employment Empl { get; set; } = new();
        [BindProperty] public Property Prop { get; set; } = new();

        public List<Dictionary<string, object>> drpProducts { get; set; } = new();
        public List<Dictionary<string, object>> drpNationalities { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerCatg { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerActivity { get; set; } = new();
        public List<Dictionary<string, object>> drpStages { get; set; } = new();
        public List<Dictionary<string, object>> drpBankCode_UAECB { get; set; } = new();
        public List<Dictionary<string, object>> drpPropertyStatus { get; set; } = new();
        public List<Dictionary<string, object>> drpPropertyType{ get; set; } = new();
        public List<Dictionary<string, object>> drpRelationType { get; set; } = new();
        public List<Dictionary<string, object>> drpRegistrationType { get; set; } = new();

        public List<SelectListItem> CustomerList { get; set; } = new();

        //====================================================================================================================================
        //                                                  GET handler for form opening
        //====================================================================================================================================
        //public void OnGet(bool loadsample = false, int? appid = null)
        public void OnGet(int id)
        {

            drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
            drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
            drpEmployerCatg = DaoHelper.DList(@"SELECT Description As EmployerCatg FROM GeneralCodes where CodeCategory='EmployerCatg'");
            drpEmployerActivity = DaoHelper.DList(@"SELECT Description As EmployerActivity FROM GeneralCodes where CodeCategory='EmployerActivity'");
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages where owner='sales' Order by Owner, Description_Action");
            drpBankCode_UAECB = DaoHelper.DList(@"SELECT Description As BankCode_UAECB FROM GeneralCodes where CodeCategory='BankCode_UAECB'");
            drpPropertyStatus = DaoHelper.DList(@"SELECT Description As PropertyStatus FROM GeneralCodes where CodeCategory='PropertyStatus'");
            drpPropertyType = DaoHelper.DList(@"SELECT Description As PropertyType FROM GeneralCodes where CodeCategory='PropertyType'");
            drpRelationType = DaoHelper.DList(@"SELECT Description As RelationType FROM GeneralCodes where CodeCategory='RelationType'");
            drpRegistrationType = DaoHelper.DList(@"SELECT Description As RegistrationType FROM GeneralCodes where CodeCategory='RegistrationType'");

            var row = DaoHelper.DRow($@"SELECT * FROM AS_Entries WHERE AppID={id}");

            App.AppID = id;
            App.AppID_usr = row["AppID_usr"]?.ToString();
            App.AppProduct = row["AppProduct"]?.ToString();
            App.Stage = row["Stage"]?.ToString();

            Cust.FirstName = row["FirstName"]?.ToString();
            Cust.MiddleName = row["MiddleName"]?.ToString();
            Cust.LastName = row["LastName"]?.ToString();
            Cust.AraName = row["AraName"]?.ToString();
            Cust.Nationality = row["Nationality"]?.ToString();
            Cust.CustomerType = row["CustomerType"]?.ToString();

            Empl.EmployerCatg = row["EmployerCatg"]?.ToString();
            Empl.Employer = row["Employer"]?.ToString();
            Empl.EmployerActivity = row["EmployerActivity"]?.ToString();
            Empl.Salary = decimal.TryParse(row["Salary"]?.ToString(), out var sal) ? sal : null;
            Empl.DOJ = row["DOJ"] == DBNull.Value ? null : Convert.ToDateTime(row["DOJ"]);
            Empl.LOS = decimal.TryParse(row["LOS"]?.ToString(), out var los) ? los : null;
            Empl.BankingWith = row["BankingWith"]?.ToString();
            Empl.AccountNumber = row["AccountNumber"]?.ToString();

            Prop.PropertyType = row["PropertyType"]?.ToString();
            Prop.PropertyDesc = row["PropertyDesc"]?.ToString();
            Prop.MortgageDegree = row["MortgageDegree"]?.ToString();

            Prop.MasterDevelopmentName = row["MasterDevelopmentName"]?.ToString();
            Prop.MasterDeveloperName = row["MasterDeveloperName"]?.ToString();
            Prop.ProjectName = row["ProjectName"]?.ToString();
            Prop.DeveloperName = row["DeveloperName"]?.ToString();
            Prop.BuildingName = row["BuildingName"]?.ToString();

            Prop.OriginalPropertyValue = decimal.TryParse(row["OriginalPropertyValue"]?.ToString(), out var opv) ? opv : null;
            Prop.AppraisalValue = decimal.TryParse(row["AppraisalValue"]?.ToString(), out var av) ? av : null;
            Prop.AppraisalBy = row["AppraisalBy"]?.ToString();
            Prop.AppraisalDate = row["AppraisalDate"] == DBNull.Value ? null : Convert.ToDateTime(row["AppraisalDate"]);
            Prop.PropertyStatus = row["PropertyStatus"]?.ToString();
            Prop.DateOfCompletion = row["DateOfCompletion"] == DBNull.Value ? null : Convert.ToDateTime(row["DateOfCompletion"]);

            Prop.LotNo = row["LotNo"]?.ToString();
            Prop.PlotNo = row["PlotNo"]?.ToString();
            Prop.TitleDeedNo = row["TitleDeedNo"]?.ToString();
            Prop.LandArea = decimal.TryParse(row["LandArea"]?.ToString(), out var la) ? la : null;
            Prop.BuiltupArea = decimal.TryParse(row["BuiltupArea"]?.ToString(), out var bua) ? bua : null;
            Prop.PropertyValuePerUnit = decimal.TryParse(row["PropertyValuePerUnit"]?.ToString(), out var pvpu) ? pvpu : null;
            Prop.RegistrationType = row["RegistrationType"]?.ToString();
            Prop.RegistrationNumber = row["RegistrationNumber"]?.ToString();
            Prop.RegisteredOnNameOf = row["RegisteredOnNameOf"]?.ToString();
            Prop.Relation = row["Relation"]?.ToString();
            Prop.RegistrationDate = row["RegistrationDate"] == DBNull.Value ? null : Convert.ToDateTime(row["RegistrationDate"]);
            Prop.IssuedFrom = row["IssuedFrom"]?.ToString();
            Prop.VillaAptNo = row["VillaAptNo"]?.ToString();
            Prop.BuildingCompoundName = row["BuildingCompoundName"]?.ToString();
            Prop.StreetName = row["StreetName"]?.ToString();
            Prop.Country = row["Country"]?.ToString();
            Prop.Emirate = row["Emirate"]?.ToString();
            Prop.AreaName = row["AreaName"]?.ToString();

            App.OriginalPrice = decimal.TryParse(row["OriginalPrice"]?.ToString(), out var op) ? op : null;
            App.Downpayment = decimal.TryParse(row["Downpayment"]?.ToString(), out var dp) ? dp : null;
            App.NFA = decimal.TryParse(row["NFA"]?.ToString(), out var nfa) ? nfa : null;
            App.ProfitRate = decimal.TryParse(row["ProfitRate"]?.ToString(), out var pr) ? pr : null;
            App.Tenor = int.TryParse(row["Tenor"]?.ToString(), out var tenor) ? tenor : null;
            App.DSR = decimal.TryParse(row["DSR"]?.ToString(), out var dsr) ? dsr : null;

            App.SalesName = row["SalesName"]?.ToString();
            App.Remarks = row["Remarks"]?.ToString();


        }





        //====================================================================================================================================
        //                                                  POST handler for form submission
        //====================================================================================================================================

        public IActionResult OnPost()
        {
            App.NFA = (App.OriginalPrice ?? 0) - (App.Downpayment ?? 0);

            string sql = $@"
                        UPDATE AS_Entries
                        SET
                            AppID_usr='{App.AppID_usr}',
                            AppProduct='{App.AppProduct}',
                            Stage='{App.Stage}',

                            FirstName='{Cust.FirstName}',
                            MiddleName='{Cust.MiddleName}',
                            LastName='{Cust.LastName}',
                            AraName='{Cust.AraName}',
                            Nationality='{Cust.Nationality}',
                            CustomerType='{Cust.CustomerType}',    
                            DOB='{Cust.DOB}',    

                            EmployerCatg='{Empl.EmployerCatg}',
                            Employer='{Empl.Employer}',
                            EmployerActivity='{Empl.EmployerActivity}',
                            Salary={(Empl.Salary.HasValue ? Empl.Salary.Value.ToString() : "NULL")},
                            DOJ={(Empl.DOJ.HasValue ? $"'{Empl.DOJ:yyyy-MM-dd}'" : "NULL")},
                            LOS={(Empl.LOS.HasValue ? Empl.LOS.Value.ToString() : "NULL")},
                            BankingWith='{Empl.BankingWith}',
                            AccountNumber='{Empl.AccountNumber}',

                            PropertyType='{Prop.PropertyType}',
                            PropertyDesc='{Prop.PropertyDesc}',
                            MortgageDegree='{Prop.MortgageDegree}',
                            MasterDevelopmentName='{Prop.MasterDevelopmentName}',
                            MasterDeveloperName='{Prop.MasterDeveloperName}',
                            ProjectName='{Prop.ProjectName}',
                            DeveloperName='{Prop.DeveloperName}',
                            BuildingName='{Prop.BuildingName}',

                            OriginalPropertyValue={(Prop.OriginalPropertyValue.HasValue ? Prop.OriginalPropertyValue.Value.ToString() : "NULL")},
                            AppraisalValue={(Prop.AppraisalValue.HasValue ? Prop.AppraisalValue.Value.ToString() : "NULL")},
                            AppraisalBy='{Prop.AppraisalBy}',
                            AppraisalDate={(Prop.AppraisalDate.HasValue ? $"'{Prop.AppraisalDate:yyyy-MM-dd}'" : "NULL")},
                            PropertyStatus='{Prop.PropertyStatus}',
                            DateOfCompletion={(Prop.DateOfCompletion.HasValue ? $"'{Prop.DateOfCompletion:yyyy-MM-dd}'" : "NULL")},

                            LotNo='{Prop.LotNo}',
                            PlotNo='{Prop.PlotNo}',
                            TitleDeedNo='{Prop.TitleDeedNo}',
                            LandArea={(Prop.LandArea.HasValue ? Prop.LandArea.Value.ToString() : "NULL")},
                            BuiltupArea={(Prop.BuiltupArea.HasValue ? Prop.BuiltupArea.Value.ToString() : "NULL")},
                            PropertyValuePerUnit={(Prop.PropertyValuePerUnit.HasValue ? Prop.PropertyValuePerUnit.Value.ToString() : "NULL")},

                            RegistrationType='{Prop.RegistrationType}',
                            RegistrationNumber='{Prop.RegistrationNumber}',
                            RegisteredOnNameOf='{Prop.RegisteredOnNameOf}',
                            Relation='{Prop.Relation}',
                            RegistrationDate={(Prop.RegistrationDate.HasValue ? $"'{Prop.RegistrationDate:yyyy-MM-dd}'" : "NULL")},
                            IssuedFrom='{Prop.IssuedFrom}',
                            VillaAptNo='{Prop.VillaAptNo}',
                            BuildingCompoundName='{Prop.BuildingCompoundName}',
                            StreetName='{Prop.StreetName}',
                            Country='{Prop.Country}',
                            Emirate='{Prop.Emirate}',
                            AreaName='{Prop.AreaName}',

                            OriginalPrice={(App.OriginalPrice.HasValue ? App.OriginalPrice.Value.ToString() : "NULL")},
                            Downpayment={(App.Downpayment.HasValue ? App.Downpayment.Value.ToString() : "NULL")},
                            NFA={(App.NFA.HasValue ? App.NFA.Value.ToString() : "NULL")},
                            ProfitRate={(App.ProfitRate.HasValue ? App.ProfitRate.Value.ToString() : "NULL")},
                            Tenor={(App.Tenor.HasValue ? App.Tenor.Value.ToString() : "NULL")},
                            DSR={(App.DSR.HasValue ? App.DSR.Value.ToString() : "NULL")},

                            SalesName='{App.SalesName}',
                            Remarks='{App.Remarks}',

                            LastUpdatedBy ='{App.LastUpdatedBy}',
                            LastUpdatedOn=GETDATE() 
                            WHERE AppID={App.AppID}";
            
                            DaoHelper.DExecute(sql);

                            TempData["SuccessMessage"] = "Changes saved successfully.";
                            return RedirectToPage(new { id = App.AppID });

                            //return RedirectToPage("Index");
                            //return Page();
                            //await _context.SaveChangesAsync();
                            //return RedirectToPage(new { id = App.AppID });

        }
    }
}


//string sql = $@"
//    INSERT INTO AS_Entries
//    (
//        AppID_usr,
//        AppDate_usr,
//        AppProduct,
//        Stage,

//        CustomerID,
//        FirstName,
//        MiddleName,
//        LastName,

//        Nationality,
//        CustomerType,
//        DOB,

//        CreatedBy,
//        LastUpdatedBy,

//        Employer,
//        EmployerCatg,
//        EmployerActivity,
//        Salary,
//        DOJ,
//        LOS,
//        BankingWith,
//        AccountNumber,

//        PropertyType,
//        PropertyDesc,
//        MortgageDegree,
//        MasterDevelopmentName,
//        MasterDeveloperName,
//        ProjectName,
//        DeveloperName,
//        BuildingName,
//        OriginalPropertyValue,
//        AppraisalValue,
//        AppraisalBy,
//        AppraisalDate,
//        PropertyStatus,
//        DateOfCompletion,

//        LotNo,
//        PlotNo,
//        TitleDeedNo,
//        LandArea,
//        BuiltupArea,
//        PropertyValuePerUnit,
//        RegistrationType,
//        RegistrationNumber,
//        RegisteredOnNameOf,
//        Relation,
//        RegistrationDate,
//        IssuedFrom,
//        VillaAptNo,
//        BuildingCompoundName,
//        StreetName,
//        Country,
//        Emirate,
//        AreaName,

//        OriginalPrice,
//        Downpayment,
//        NFA,
//        ProfitRate,
//        Tenor,
//        DSR,
//        SalesName,
//        Remarks
//    )
//    VALUES
//    (
//        '{App.AppID_usr}',
//        '{App.AppDate_usr:yyyy-MM-dd}',
//        '{App.AppProduct}',
//        '{App.Stage}',

//        {(Cust.CustomerID.HasValue ? Cust.CustomerID.Value.ToString() : "NULL")},
//        '{Cust.FirstName}',
//        '{Cust.MiddleName}',
//        '{Cust.LastName}',

//        '{Cust.Nationality}',
//        '{Cust.CustomerType}',
//        {(Cust.DOB.HasValue ? $"'{Cust.DOB.Value:yyyy-MM-dd}'" : "NULL")},

//        'Admin',
//        'Admin',
//        '{Empl.Employer}',
//        '{Empl.EmployerCatg}',
//        '{Empl.EmployerActivity}',
//        {(Empl.Salary.HasValue ? Empl.Salary.Value.ToString() : "NULL")},
//        {(Empl.DOJ.HasValue ? $"'{Empl.DOJ.Value:yyyy-MM-dd}'" : "NULL")},
//        {(Empl.LOS.HasValue ? Empl.LOS.Value.ToString() : "NULL")},
//        '{Empl.BankingWith}',
//        '{Empl.AccountNumber}'

//        ,'{Prop.PropertyType}'
//        ,'{Prop.PropertyDesc}'
//        ,'{Prop.MortgageDegree}'
//        ,'{Prop.MasterDevelopmentName}'
//        ,'{Prop.MasterDeveloperName}'
//        ,'{Prop.ProjectName}'
//        ,'{Prop.DeveloperName}'
//        ,'{Prop.BuildingName}'
//        ,{(Prop.OriginalPropertyValue.HasValue ? Prop.OriginalPropertyValue.Value.ToString() : "NULL")}
//        ,{(Prop.AppraisalValue.HasValue ? Prop.AppraisalValue.Value.ToString() : "NULL")}
//        ,'{Prop.AppraisalBy}'
//        ,{(Prop.AppraisalDate.HasValue ? $"'{Prop.AppraisalDate.Value:yyyy-MM-dd}'" : "NULL")}
//        ,'{Prop.PropertyStatus}'
//        ,{(Prop.DateOfCompletion.HasValue ? $"'{Prop.DateOfCompletion.Value:yyyy-MM-dd}'" : "NULL")}
//        ,'{Prop.LotNo}'
//        ,'{Prop.PlotNo}'
//        ,'{Prop.TitleDeedNo}'
//        ,{(Prop.LandArea.HasValue ? Prop.LandArea.Value.ToString() : "NULL")}
//        ,{(Prop.BuiltupArea.HasValue ? Prop.BuiltupArea.Value.ToString() : "NULL")}
//        ,{(Prop.PropertyValuePerUnit.HasValue ? Prop.PropertyValuePerUnit.Value.ToString() : "NULL")}
//        ,'{Prop.RegistrationType}'
//        ,'{Prop.RegistrationNumber}'
//        ,'{Prop.RegisteredOnNameOf}'
//        ,'{Prop.Relation}'
//        ,{(Prop.RegistrationDate.HasValue ? $"'{Prop.RegistrationDate.Value:yyyy-MM-dd}'" : "NULL")}
//        ,'{Prop.IssuedFrom}'
//        ,'{Prop.VillaAptNo}'
//        ,'{Prop.BuildingCompoundName}'
//        ,'{Prop.StreetName}'
//        ,'{Prop.Country}'
//        ,'{Prop.Emirate}'
//        ,'{Prop.AreaName}'

//        ,{(App.OriginalPrice.HasValue ? App.OriginalPrice.Value.ToString() : "NULL")}
//        ,{(App.Downpayment.HasValue ? App.Downpayment.Value.ToString() : "NULL")}
//        ,{(App.NFA.HasValue ? App.NFA.Value.ToString() : "NULL")}
//        ,{(App.ProfitRate.HasValue ? App.ProfitRate.Value.ToString() : "NULL")}
//        ,{(App.Tenor.HasValue ? App.Tenor.Value.ToString() : "NULL")}
//        ,{(App.DSR.HasValue ? App.DSR.Value.ToString() : "NULL")}
//        ,'{App.SalesName}'
//        ,'{App.Remarks}'

//    )";


//Console.WriteLine(sql);
// throw new Exception(sql);
//DaoHelper.DExecute(sql);

//return RedirectToPage("Index");
//        }
//    }
//}









//using kt_system.Helpers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace kt_system.Pages.Applications
//{
//    public class EditModel : PageModel
//    {

//        //Application information
//        [BindProperty] public int AppID { get; set; }
//        [BindProperty] public string? AppID_usr { get; set; }
//        [BindProperty] public DateTime AppDate_usr { get; set; }
//        [BindProperty] public string? AppProduct { get; set; }
//        [BindProperty] public string? Stage { get; set; }

//        //Customer information
//        [BindProperty] public string FirstName { get; set; }
//        [BindProperty] public string? MiddleName { get; set; }
//        [BindProperty] public string? LastName { get; set; }
//        [BindProperty] public string? Nationality { get; set; }
//        [BindProperty] public string? CustomerType { get; set; }
//        [BindProperty] public DateTime? DOB { get; set; }

//        //emloyment information
//        [BindProperty] public string? Employer { get; set; }
//        [BindProperty] public string? EmployerCatg { get; set; }
//        [BindProperty] public string? EmployerActivity { get; set; }
//        [BindProperty] public decimal? Salary { get; set; }
//        [BindProperty] public DateTime? DOJ { get; set; }
//        [BindProperty] public decimal? LOS { get; set; }
//        [BindProperty] public string? BankingWith { get; set; }
//        [BindProperty] public string? AccountNumber { get; set; }
//        //------------------------------------



//        //---------Property Details---------------------------
//        [BindProperty] public string? PropertyType { get; set; }
//        [BindProperty] public string? PropertyDesc { get; set; }
//        [BindProperty] public string? MortgageDegree { get; set; }
//        [BindProperty] public string? MasterDevelopmentName { get; set; }
//        [BindProperty] public string? MasterDeveloperName { get; set; }
//        [BindProperty] public string? ProjectName { get; set; }
//        [BindProperty] public string? DeveloperName { get; set; }
//        [BindProperty] public string? BuildingName { get; set; }
//        [BindProperty] public decimal? OriginalPropertyValue { get; set; }
//        [BindProperty] public decimal? AppraisalValue { get; set; }
//        [BindProperty] public string? AppraisalBy { get; set; }
//        [BindProperty] public DateTime? AppraisalDate { get; set; }
//        [BindProperty] public string? PropertyStatus { get; set; }
//        [BindProperty] public DateTime? DateOfCompletion { get; set; }
//        //------------------------------------


//        //---------Property Address Details ----------

//        [BindProperty] public string? LotNo { get; set; }
//        [BindProperty] public string? PlotNo { get; set; }
//        [BindProperty] public string? TitleDeedNo { get; set; }
//        [BindProperty] public decimal? LandArea { get; set; }
//        [BindProperty] public decimal? BuiltupArea { get; set; }
//        [BindProperty] public decimal? PropertyValuePerUnit { get; set; }
//        [BindProperty] public string? RegistrationType { get; set; }
//        [BindProperty] public string? RegistrationNumber { get; set; }
//        [BindProperty] public string? RegisteredOnNameOf { get; set; }
//        [BindProperty] public string? Relation { get; set; }
//        [BindProperty] public DateTime? RegistrationDate { get; set; }
//        [BindProperty] public string? IssuedFrom { get; set; }
//        [BindProperty] public string? VillaAptNo { get; set; }
//        [BindProperty] public string? BuildingCompoundName { get; set; }
//        [BindProperty] public string? StreetName { get; set; }
//        [BindProperty] public string? Country { get; set; }
//        [BindProperty] public string? Emirate { get; set; }
//        [BindProperty] public string? AreaName { get; set; }

//        //---------Finance Details-------------------
//        [BindProperty] public decimal? OriginalPrice { get; set; }
//        [BindProperty] public decimal? Downpayment { get; set; }
//        [BindProperty] public decimal? NFA { get; set; }
//        [BindProperty] public decimal? ProfitRate { get; set; }
//        [BindProperty] public int? Tenor { get; set; }
//        [BindProperty] public decimal? DSR { get; set; }
//        [BindProperty] public string? SalesName { get; set; }
//        [BindProperty] public string? Remarks { get; set; }
//        [BindProperty] public string? LastUpdatedBy { get; set; }


//        public List<Dictionary<string, object>> drpProducts { get; set; } = new();
//        public List<Dictionary<string, object>> drpNationalities { get; set; } = new();
//        public List<Dictionary<string, object>> drpStages { get; set; } = new();


//        //------------OnGet--------------

//        public void OnGet(int id)

//        {

//            {
//                //int nextId = DaoHelper.DMaxInt("AppID", "AS_Entries", "1=1") + 1;
//                //AppID_usr = $"APP-{DateTime.Now.Year}-{nextId:000000}";
//                drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
//                drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
//                drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
//            }

//            var row = DaoHelper.DRow($@"SELECT * FROM AS_Entries WHERE AppID={id}");

//            AppID = id;
//            AppID_usr = row["AppID_usr"]?.ToString();
//            AppProduct = row["AppProduct"]?.ToString();
//            Stage = row["Stage"]?.ToString();

//            FirstName = row["FirstName"]?.ToString();
//            MiddleName = row["MiddleName"]?.ToString();
//            LastName = row["LastName"]?.ToString();
//            Nationality = row["Nationality"]?.ToString();
//            CustomerType = row["CustomerType"]?.ToString();

//            EmployerCatg = row["EmployerCatg"]?.ToString();
//            Employer = row["Employer"]?.ToString();
//            EmployerActivity = row["EmployerActivity"]?.ToString();
//            Salary = decimal.TryParse(row["Salary"]?.ToString(), out var sal) ? sal : null;
//            DOJ = row["DOJ"] == DBNull.Value ? null : Convert.ToDateTime(row["DOJ"]);
//            LOS = decimal.TryParse(row["LOS"]?.ToString(), out var los) ? los : null;
//            BankingWith = row["BankingWith"]?.ToString();
//            AccountNumber = row["AccountNumber"]?.ToString();

//            PropertyType = row["PropertyType"]?.ToString();
//            PropertyDesc = row["PropertyDesc"]?.ToString();
//            MortgageDegree = row["MortgageDegree"]?.ToString();

//            MasterDevelopmentName = row["MasterDevelopmentName"]?.ToString();
//            MasterDeveloperName = row["MasterDeveloperName"]?.ToString();
//            ProjectName = row["ProjectName"]?.ToString();
//            DeveloperName = row["DeveloperName"]?.ToString();
//            BuildingName = row["BuildingName"]?.ToString();

//            OriginalPropertyValue = decimal.TryParse(row["OriginalPropertyValue"]?.ToString(), out var opv) ? opv : null;
//            AppraisalValue = decimal.TryParse(row["AppraisalValue"]?.ToString(), out var av) ? av : null;
//            AppraisalBy = row["AppraisalBy"]?.ToString();
//            AppraisalDate = row["AppraisalDate"] == DBNull.Value ? null : Convert.ToDateTime(row["AppraisalDate"]);
//            PropertyStatus = row["PropertyStatus"]?.ToString();
//            DateOfCompletion = row["DateOfCompletion"] == DBNull.Value ? null : Convert.ToDateTime(row["DateOfCompletion"]);

//            LotNo = row["LotNo"]?.ToString();
//            PlotNo = row["PlotNo"]?.ToString();
//            TitleDeedNo = row["TitleDeedNo"]?.ToString();
//            LandArea = decimal.TryParse(row["LandArea"]?.ToString(), out var la) ? la : null;
//            BuiltupArea = decimal.TryParse(row["BuiltupArea"]?.ToString(), out var bua) ? bua : null;
//            PropertyValuePerUnit = decimal.TryParse(row["PropertyValuePerUnit"]?.ToString(), out var pvpu) ? pvpu : null;
//            RegistrationType = row["RegistrationType"]?.ToString();
//            RegistrationNumber = row["RegistrationNumber"]?.ToString();
//            RegisteredOnNameOf = row["RegisteredOnNameOf"]?.ToString();
//            Relation = row["Relation"]?.ToString();
//            RegistrationDate = row["RegistrationDate"] == DBNull.Value ? null : Convert.ToDateTime(row["RegistrationDate"]);
//            IssuedFrom = row["IssuedFrom"]?.ToString();
//            VillaAptNo = row["VillaAptNo"]?.ToString();
//            BuildingCompoundName = row["BuildingCompoundName"]?.ToString();
//            StreetName = row["StreetName"]?.ToString();
//            Country = row["Country"]?.ToString();
//            Emirate = row["Emirate"]?.ToString();
//            AreaName = row["AreaName"]?.ToString();

//            OriginalPrice = decimal.TryParse(row["OriginalPrice"]?.ToString(), out var op) ? op : null;
//            Downpayment = decimal.TryParse(row["Downpayment"]?.ToString(), out var dp) ? dp : null;
//            NFA = decimal.TryParse(row["NFA"]?.ToString(), out var nfa) ? nfa : null;
//            ProfitRate = decimal.TryParse(row["ProfitRate"]?.ToString(), out var pr) ? pr : null;
//            Tenor = int.TryParse(row["Tenor"]?.ToString(), out var tenor) ? tenor : null;
//            DSR = decimal.TryParse(row["DSR"]?.ToString(), out var dsr) ? dsr : null;

//            SalesName = row["SalesName"]?.ToString();
//            Remarks = row["Remarks"]?.ToString();

//        }



//        public IActionResult OnPost()
//        {
//            NFA = (OriginalPrice ?? 0) - (Downpayment ?? 0);

//            string sql = $@"
//                        UPDATE AS_Entries
//                        SET
//                            AppID_usr='{AppID_usr}',
//                            AppProduct='{AppProduct}',
//                            Stage='{Stage}',

//                            FirstName='{FirstName}',
//                            MiddleName='{MiddleName}',
//                            LastName='{LastName}',
//                            Nationality='{Nationality}',
//                            CustomerType='{CustomerType}',    
//                            DOB='{DOB}',    

//                            EmployerCatg='{EmployerCatg}',
//                            Employer='{Employer}',
//                            EmployerActivity='{EmployerActivity}',
//                            Salary={(Salary.HasValue?Salary.Value.ToString() : "NULL")},
//                            DOJ={(DOJ.HasValue ? $"'{DOJ:yyyy-MM-dd}'" : "NULL")},
//                            LOS={(LOS.HasValue ? LOS.Value.ToString() : "NULL")},
//                            BankingWith='{BankingWith}',
//                            AccountNumber='{AccountNumber}',

//                            PropertyType='{PropertyType}',
//                            PropertyDesc='{PropertyDesc}',
//                            MortgageDegree='{MortgageDegree}',
//                            MasterDevelopmentName='{MasterDevelopmentName}',
//                            MasterDeveloperName='{MasterDeveloperName}',
//                            ProjectName='{ProjectName}',
//                            DeveloperName='{DeveloperName}',
//                            BuildingName='{BuildingName}',

//                            OriginalPropertyValue={(OriginalPropertyValue.HasValue ? OriginalPropertyValue.Value.ToString() : "NULL")},
//                            AppraisalValue={(AppraisalValue.HasValue ? AppraisalValue.Value.ToString() : "NULL")},
//                            AppraisalBy='{AppraisalBy}',
//                            AppraisalDate={(AppraisalDate.HasValue ? $"'{AppraisalDate:yyyy-MM-dd}'" : "NULL")},
//                            PropertyStatus='{PropertyStatus}',
//                            DateOfCompletion={(DateOfCompletion.HasValue ? $"'{DateOfCompletion:yyyy-MM-dd}'" : "NULL")},

//                            LotNo='{LotNo}',
//                            PlotNo='{PlotNo}',
//                            TitleDeedNo='{TitleDeedNo}',
//                            LandArea={(LandArea.HasValue ? LandArea.Value.ToString() : "NULL")},
//                            BuiltupArea={(BuiltupArea.HasValue ? BuiltupArea.Value.ToString() : "NULL")},
//                            PropertyValuePerUnit={(PropertyValuePerUnit.HasValue ? PropertyValuePerUnit.Value.ToString() : "NULL")},

//                            RegistrationType='{RegistrationType}',
//                            RegistrationNumber='{RegistrationNumber}',
//                            RegisteredOnNameOf='{RegisteredOnNameOf}',
//                            Relation='{Relation}',
//                            RegistrationDate={(RegistrationDate.HasValue ? $"'{RegistrationDate:yyyy-MM-dd}'" : "NULL")},
//                            IssuedFrom='{IssuedFrom}',
//                            VillaAptNo='{VillaAptNo}',
//                            BuildingCompoundName='{BuildingCompoundName}',
//                            StreetName='{StreetName}',
//                            Country='{Country}',
//                            Emirate='{Emirate}',
//                            AreaName='{AreaName}',

//                            OriginalPrice={(OriginalPrice.HasValue ? OriginalPrice.Value.ToString() : "NULL")},
//                            Downpayment={(Downpayment.HasValue ? Downpayment.Value.ToString() : "NULL")},
//                            NFA={(NFA.HasValue ? NFA.Value.ToString() : "NULL")},
//                            ProfitRate={(ProfitRate.HasValue? ProfitRate.Value.ToString():"NULL")},
//                            Tenor={(Tenor.HasValue? Tenor.Value.ToString() : "NULL")},
//                            DSR={(DSR.HasValue ? DSR.Value.ToString(): "NULL")},


//                            SalesName='{SalesName}',
//                            Remarks='{Remarks}',

//                            LastUpdatedBy ='{LastUpdatedBy}',
//                            LastUpdatedOn=GETDATE() 
//                            WHERE AppID={AppID}";

//            //System.IO.File.WriteAllText(@"C:\Temp\UpdateSQL.txt",sql);
//            //Console.WriteLine(sql);

//            DaoHelper.DExecute(sql);

//            return RedirectToPage("Index");
//        }
//    }
//}


