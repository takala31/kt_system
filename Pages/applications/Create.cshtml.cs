using kt_system.Helpers;
using kt_system.Models;
using KT_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kt_system.Pages.Applications
{
    public class CreateModel : PageModel
    {
        //Application information
        [BindProperty] public Application App { get; set; } = new();
        [BindProperty] public Customer Cust{ get; set; } = new();
        [BindProperty] public Employment Empl { get; set; } = new();
        [BindProperty] public Property  Prop { get; set; } = new();


        //[BindProperty] public string? AppID_usr { get; set; }
        //[BindProperty] public DateTime AppDate_usr { get; set; } = DateTime.Today;
        //[BindProperty] public string? AppProduct { get; set; }
        //[BindProperty] public string? Stage { get; set; } = "DDEQC";

        ////Customer information
        //[BindProperty] public int? CustomerID { get; set; }
        //[BindProperty] public string FirstName { get; set; } = "";
        //[BindProperty] public string? MiddleName { get; set; }
        //[BindProperty] public string? LastName { get; set; }
        //[BindProperty] public string? Nationality { get; set; }
        //[BindProperty] public string? CustomerType { get; set; }
        //[BindProperty] public string? Mobile { get; set; }
        //[BindProperty] public string? Email { get; set; }
        //[BindProperty] public DateTime? DOB { get; set; }


        //emloyment information
        //[BindProperty] public string? Employer { get; set; }
        //[BindProperty] public string? EmployerCatg { get; set; }
        //[BindProperty] public string? EmployerActivity { get; set; }
        //[BindProperty] public decimal? Salary { get; set; }
        //[BindProperty] public DateTime? DOJ { get; set; }
        //[BindProperty] public decimal? LOS { get; set; }
        //[BindProperty] public string? BankingWith { get; set; }
        //[BindProperty] public string? AccountNumber { get; set; }
        //------------------------------------



        //---------Property Details---------------------------
        //[BindProperty] public string? PropertyType { get; set; }
        //[BindProperty] public string? PropertyDesc { get; set; }
        //[BindProperty] public string? MortgageDegree { get; set; }
        //[BindProperty] public string? MasterDevelopmentName { get; set; }
        //[BindProperty] public string? MasterDeveloperName { get; set; }
        //[BindProperty] public string? ProjectName { get; set; }
        //[BindProperty] public string? DeveloperName { get; set; }
        //[BindProperty] public string? BuildingName { get; set; }
        //[BindProperty] public decimal? OriginalPropertyValue { get; set; }
        //[BindProperty] public decimal? AppraisalValue { get; set; }
        //[BindProperty] public string? AppraisalBy { get; set; }
        //[BindProperty] public DateTime? AppraisalDate { get; set; }
        //[BindProperty] public string? PropertyStatus { get; set; }
        //[BindProperty] public DateTime? DateOfCompletion { get; set; }
        //------------------------------------


        //---------Property Address Details ----------

        //[BindProperty] public string? LotNo { get; set; }
        //[BindProperty] public string? PlotNo { get; set; }
        //[BindProperty] public string? TitleDeedNo { get; set; }
        //[BindProperty] public decimal? LandArea { get; set; }
        //[BindProperty] public decimal? BuiltupArea { get; set; }
        //[BindProperty] public decimal? PropertyValuePerUnit { get; set; }
        //[BindProperty] public string? RegistrationType { get; set; }
        //[BindProperty] public string? RegistrationNumber { get; set; }
        //[BindProperty] public string? RegisteredOnNameOf { get; set; }
        //[BindProperty] public string? Relation { get; set; }
        //[BindProperty] public DateTime? RegistrationDate { get; set; }
        //[BindProperty] public string? IssuedFrom { get; set; }
        //[BindProperty] public string? VillaAptNo { get; set; }
        //[BindProperty] public string? BuildingCompoundName { get; set; }
        //[BindProperty] public string? StreetName { get; set; }

        //[BindProperty] public string? Country { get; set; }
        //[BindProperty] public string? Emirate { get; set; }
        //[BindProperty] public string? AreaName { get; set; }

        //---------Finance Details-------------------
        //[BindProperty] public decimal? OriginalPrice { get; set; }
        //[BindProperty] public decimal? Downpayment { get; set; }
        //[BindProperty] public decimal? NFA { get; set; }

        //[BindProperty] public decimal? ProfitRate { get; set; }
        //[BindProperty] public int? Tenor { get; set; }
        //[BindProperty] public decimal? DSR { get; set; }
        //[BindProperty] public string? SalesName { get; set; }
        //[BindProperty] public string? Remarks { get; set; }



        public List<Dictionary<string, object>> drpProducts { get; set; } = new();
        public List<Dictionary<string, object>> drpNationalities { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerCatg { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerActivity { get; set; } = new();
        public List<Dictionary<string, object>> drpStages { get; set; } = new();
        public List<Dictionary<string, object>> drpBankCode_UAECB { get; set; } = new();
        public List<Dictionary<string, object>> drpPropertyStatus { get; set; } = new();
        public List<Dictionary<string, object>> drpRelationType { get; set; } = new();
        public List<Dictionary<string, object>> drpRegistrationType { get; set; } = new();
        
       public List<SelectListItem> CustomerList { get; set; } = new();

        private void LoadDummyData(int appId)
        {
            //var row = DaoHelper.DRow("SELECT * FROM AS_Entries WHERE AppID = 41");
            var row = DaoHelper.DRow($"SELECT * FROM AS_Entries WHERE AppID = {appId}");


            App.AppID_usr = row["AppID_usr"]?.ToString();
            App.AppProduct = row["AppProduct"]?.ToString();
            App.Stage = row["Stage"]?.ToString();

            Cust.FirstName = row["FirstName"]?.ToString();
            Cust.MiddleName = row["MiddleName"]?.ToString();
            Cust.LastName = row["LastName"]?.ToString();

            // etc ...
        }
        private void LoadDummyData_text()
        {
            // Application
            App.AppID_usr = "TEST001";
            App.AppDate_usr = DateTime.Today;
            App.AppProduct = "Home Finance";
            App.Stage = "DDEQC";

            // Customer
            Cust.FirstName = "Fayez";
            Cust.MiddleName = "Mustafa";
            Cust.LastName = "Kurdi";
            Cust.Nationality = "United Arab Emirates";
            Cust.CustomerType = "Salaried";
            //DOB = new DateTime(1985, 5, 15);
            Cust.DOB = Convert.ToDateTime("1985-Feb-05");

            // Employment
            Empl.Employer = "Dubai Islamic Bank";
            Empl.EmployerCatg = "Government";
            Empl.EmployerActivity = "Banking";
            Empl.Salary = 25000;
            Empl.DOJ = new DateTime(2015, 1, 1);
            Empl.LOS = 11.8m;
            Empl.BankingWith = "DIB";
            Empl.AccountNumber = "1234567890";

            // Property Details
            Prop.AreaName= "Villa";
            Prop.PropertyDesc = "5 Bedroom Villa";
            Prop.MortgageDegree = "First Degree";
            Prop.MasterDevelopmentName = "Arabian Ranches";
            Prop.MasterDeveloperName = "Emaar";
            Prop.ProjectName = "Ranches Phase 3";
            Prop.DeveloperName = "Emaar";
            Prop.BuildingName = "Villa Cluster A";
            Prop.OriginalPropertyValue = 2500000;
            Prop.AppraisalValue = 2450000;
            Prop.AppraisalBy = "Land Sterling";
            Prop.AppraisalDate = DateTime.Today.AddDays(-15);
            Prop.PropertyStatus = "Completed";
            Prop.DateOfCompletion = new DateTime(2022, 12, 31);

            // Property Address
            Prop.LotNo = "L-100";
            Prop.PlotNo = "P-200";
            Prop.TitleDeedNo = "TD-123456";
            Prop.LandArea = 8000;
            Prop.BuiltupArea = 5500;
            Prop.PropertyValuePerUnit = 450;
            Prop.RegistrationType = "Title Deed";
            Prop.RegistrationNumber = "REG-5555";
            Prop.RegisteredOnNameOf = "Ahmed Ali";
            Prop.Relation = "Self";
            Prop.RegistrationDate = new DateTime(2023, 1, 15);
            Prop.IssuedFrom = "Dubai Land Department";
            Prop.VillaAptNo = "Villa 45";
            Prop.BuildingCompoundName = "Arabian Ranches";
            Prop.StreetName = "Sheikh Mohammed Bin Zayed Road";
            Prop.Country = "United Arab Emirates";
            Prop.Emirate = "Dubai";
            Prop.AreaName = "Arabian Ranches";

            // Finance
            App.OriginalPrice = 2500000;
            App.Downpayment = 500000;
            App.NFA = 2000000;
            App.ProfitRate = 4.99m;
            App.Tenor = 25;
            App.DSR = 42.50m;
            App.SalesName = "Khalid Test";
            App.Remarks = "Auto-generated test record";
        }
        private void LoadCustomers()
        {
            CustomerList.Clear();
            string sql = @"SELECT CustomerID,FirstName,LastName FROM Customers WHERE CustomerStatus = 'Active' ORDER BY FirstName, LastName";
            var rows = DaoHelper.DList(sql);

            foreach (var row in rows)
            {
                CustomerList.Add(new SelectListItem
                {
                    Value = row["CustomerID"].ToString(),

                    Text =
                        $"{row["CustomerID"]} - " +
                        $"{row["FirstName"]} " +
                        $"{row["LastName"]}"
                });
            }
        }

        public IActionResult OnPostLoadCustomer()
        {
            LoadCustomers();
            drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
            drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
            drpEmployerCatg = DaoHelper.DList(@"SELECT Description As EmployerCatg FROM GeneralCodes where CodeCategory='EmployerCatg'");
            drpEmployerActivity= DaoHelper.DList(@"SELECT Description As EmployerActivity FROM GeneralCodes where CodeCategory='EmployerActivity'");
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
            drpBankCode_UAECB = DaoHelper.DList(@"SELECT Description As BankCode_UAECB FROM GeneralCodes where CodeCategory='BankCode_UAECB'");
            drpPropertyStatus = DaoHelper.DList(@"SELECT Description As PropertyStatus FROM GeneralCodes where CodeCategory='PropertyStatus'");
            drpRelationType = DaoHelper.DList(@"SELECT Description As RelationType FROM GeneralCodes where CodeCategory='RelationType'");
            drpRegistrationType = DaoHelper.DList(@"SELECT Description As RegistrationType FROM GeneralCodes where CodeCategory='RegistrationType'");

            if (!Cust.CustomerID.HasValue)
                return Page();

            string sql = $@"SELECT * FROM Customers WHERE CustomerID = {Cust.CustomerID}";
            var rows = DaoHelper.DList(sql);
            if (rows.Count == 0)
                return Page();
            var row = rows[0];

            Cust.FirstName = row["FirstName"]?.ToString();
            Cust.MiddleName = row["MiddleName"]?.ToString();
            Cust.LastName = row["LastName"]?.ToString();
            Cust.CustomerType = row["CustomerType"]?.ToString();
            Cust.Nationality = row["Nationality"]?.ToString();
            Cust.Mobile = row["Mobile"]?.ToString();
            Cust.Email = row["Email"]?.ToString();
            Empl.Employer = row["Employer"]?.ToString();
            Empl.EmployerCatg = row["EmployerCatg"]?.ToString();
            Empl.EmployerActivity = row["EmployerActivity"]?.ToString();
            Empl.BankingWith = row["BankingWith"]?.ToString();

            if (row["DOB"] != DBNull.Value)
                Cust.DOB = Convert.ToDateTime(row["DOB"]);
            if (row["DOJ"] != DBNull.Value)
                Empl.DOJ = Convert.ToDateTime(row["DOJ"]);
            if (row["Salary"] != DBNull.Value)
                Empl.Salary = Convert.ToDecimal(row["Salary"]);

            ModelState.Clear();
            return Page();
        }


        //====================================================================================================================================
        //                                                  GET handler for form opening
        //====================================================================================================================================
        public void OnGet(bool loadsample = false, int? appid = null)
        {
            //if (!loadsample) return; LoadDummyData();
            //if (!loadsample) return; LoadDummyData_1(4);
            //if (!(appId > 0)) return; LoadDummyData(appId);

            appid = null;   // stop it for now. For testing purposes, you can set this to a specific AppID to load data from an existing application
            {
                if (loadsample && appid != null)
                {
                    // Load from existing application
                    LoadDummyData(appid.Value);
                }
                else if (loadsample)
                {
                    // Load hardcoded test data
                    LoadDummyData_text();
                }
            }


            int nextId = DaoHelper.DMaxInt("AppID", "AS_Entries", "1=1") + 1;
            App.AppID_usr = $"APP-{DateTime.Now.Year}-{nextId:000000}";


            drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
            drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
            drpEmployerCatg = DaoHelper.DList(@"SELECT Description As EmployerCatg FROM GeneralCodes where CodeCategory='EmployerCatg'");
            drpEmployerActivity = DaoHelper.DList(@"SELECT Description As EmployerActivity FROM GeneralCodes where CodeCategory='EmployerActivity'");
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
            drpBankCode_UAECB = DaoHelper.DList(@"SELECT Description As BankCode_UAECB FROM GeneralCodes where CodeCategory='BankCode_UAECB'");
            drpPropertyStatus = DaoHelper.DList(@"SELECT Description As PropertyStatus FROM GeneralCodes where CodeCategory='PropertyStatus'");
            drpRelationType = DaoHelper.DList(@"SELECT Description As RelationType FROM GeneralCodes where CodeCategory='RelationType'");
            drpRegistrationType = DaoHelper.DList(@"SELECT Description As RegistrationType FROM GeneralCodes where CodeCategory='RegistrationType'");

            LoadCustomers();


        }
        //====================================================================================================================================





        //====================================================================================================================================
        //                                                  POST handler for form submission
        //====================================================================================================================================

        public IActionResult OnPost()
        {

            // Calculate LOS from DOJ
            if (Empl.DOJ.HasValue)
            {
                Empl.LOS = Math.Round(((decimal)(DateTime.Today - Empl.DOJ.Value).TotalDays) / 365.25m, 2);
            }

            App.NFA = (App.OriginalPrice ?? 0) - (App.Downpayment ?? 0);

            string sql = $@"
                INSERT INTO AS_Entries
                (
                    AppID_usr,
                    AppDate_usr,
                    AppProduct,
                    Stage,

                    CustomerID,
                    FirstName,
                    MiddleName,
                    LastName,

                    Nationality,
                    CustomerType,
                    DOB,

                    CreatedBy,
                    LastUpdatedBy,

                    Employer,
                    EmployerCatg,
                    EmployerActivity,
                    Salary,
                    DOJ,
                    LOS,
                    BankingWith,
                    AccountNumber,

                    PropertyType,
                    PropertyDesc,
                    MortgageDegree,
                    MasterDevelopmentName,
                    MasterDeveloperName,
                    ProjectName,
                    DeveloperName,
                    BuildingName,
                    OriginalPropertyValue,
                    AppraisalValue,
                    AppraisalBy,
                    AppraisalDate,
                    PropertyStatus,
                    DateOfCompletion,
                    
                    LotNo,
                    PlotNo,
                    TitleDeedNo,
                    LandArea,
                    BuiltupArea,
                    PropertyValuePerUnit,
                    RegistrationType,
                    RegistrationNumber,
                    RegisteredOnNameOf,
                    Relation,
                    RegistrationDate,
                    IssuedFrom,
                    VillaAptNo,
                    BuildingCompoundName,
                    StreetName,
                    Country,
                    Emirate,
                    AreaName,

                    OriginalPrice,
                    Downpayment,
                    NFA,
                    ProfitRate,
                    Tenor,
                    DSR,
                    SalesName,
                    Remarks
                )
                VALUES
                (
                    '{App.AppID_usr}',
                    '{App.AppDate_usr:yyyy-MM-dd}',
                    '{App.AppProduct}',
                    '{App.Stage}',

                    {(Cust.CustomerID.HasValue? Cust.CustomerID.Value.ToString() : "NULL")},
                    '{Cust.FirstName}',
                    '{Cust.MiddleName}',
                    '{Cust.LastName}',

                    '{Cust.Nationality}',
                    '{Cust.CustomerType}',
                    {(Cust.DOB.HasValue ? $"'{Cust.DOB.Value:yyyy-MM-dd}'" : "NULL")},

                    'Admin',
                    'Admin',
                    '{Empl.Employer}',
                    '{Empl.EmployerCatg}',
                    '{Empl.EmployerActivity}',
                    {(Empl.Salary.HasValue ? Empl.Salary.Value.ToString() : "NULL")},
                    {(Empl.DOJ.HasValue ? $"'{Empl.DOJ.Value:yyyy-MM-dd}'" : "NULL")},
                    {(Empl.LOS.HasValue ? Empl.LOS.Value.ToString() : "NULL")},
                    '{Empl.BankingWith}',
                    '{Empl.AccountNumber}'

                    ,'{Prop.PropertyType}'
                    ,'{Prop.PropertyDesc}'
                    ,'{Prop.MortgageDegree}'
                    ,'{Prop.MasterDevelopmentName}'
                    ,'{Prop.MasterDeveloperName}'
                    ,'{Prop.ProjectName}'
                    ,'{Prop.DeveloperName}'
                    ,'{Prop.BuildingName}'
                    ,{(Prop.OriginalPropertyValue.HasValue ? Prop.OriginalPropertyValue.Value.ToString() : "NULL")}
                    ,{(Prop.AppraisalValue.HasValue ? Prop.AppraisalValue.Value.ToString() : "NULL")}
                    ,'{Prop.AppraisalBy}'
                    ,{(Prop.AppraisalDate.HasValue ? $"'{Prop.AppraisalDate.Value:yyyy-MM-dd}'" : "NULL")}
                    ,'{Prop.PropertyStatus}'
                    ,{(Prop.DateOfCompletion.HasValue ? $"'{Prop.DateOfCompletion.Value:yyyy-MM-dd}'" : "NULL")}
                    ,'{Prop.LotNo}'
                    ,'{Prop.PlotNo}'
                    ,'{Prop.TitleDeedNo}'
                    ,{(Prop.LandArea.HasValue ? Prop.LandArea.Value.ToString() : "NULL")}
                    ,{(Prop.BuiltupArea.HasValue ? Prop.BuiltupArea.Value.ToString() : "NULL")}
                    ,{(Prop.PropertyValuePerUnit.HasValue ? Prop.PropertyValuePerUnit.Value.ToString() : "NULL")}
                    ,'{Prop.RegistrationType}'
                    ,'{Prop.RegistrationNumber}'
                    ,'{Prop.RegisteredOnNameOf}'
                    ,'{Prop.Relation}'
                    ,{(Prop.RegistrationDate.HasValue ? $"'{Prop.RegistrationDate.Value:yyyy-MM-dd}'" : "NULL")}
                    ,'{Prop.IssuedFrom}'
                    ,'{Prop.VillaAptNo}'
                    ,'{Prop.BuildingCompoundName}'
                    ,'{Prop.StreetName}'
                    ,'{Prop.Country}'
                    ,'{Prop.Emirate}'
                    ,'{Prop.AreaName}'

                    ,{(App.OriginalPrice.HasValue ? App.OriginalPrice.Value.ToString() : "NULL")}
                    ,{(App.Downpayment.HasValue ? App.Downpayment.Value.ToString() : "NULL")}
                    ,{(App.NFA.HasValue ? App.NFA.Value.ToString() : "NULL")}
                    ,{(App.ProfitRate.HasValue ? App.ProfitRate.Value.ToString() : "NULL")}
                    ,{(App.Tenor.HasValue ? App.Tenor.Value.ToString() : "NULL")}
                    ,{(App.DSR.HasValue ? App.DSR.Value.ToString() : "NULL")}
                    ,'{App.SalesName}'
                    ,'{App.Remarks}'

                )";


            //Console.WriteLine(sql);
           // throw new Exception(sql);
            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }
    }
}