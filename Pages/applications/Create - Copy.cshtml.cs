using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kt_system.Pages.Applications
{
    public class CreateModel : PageModel
    {
        //Application information
        [BindProperty] public string? AppID_usr { get; set; }
        [BindProperty] public DateTime AppDate_usr { get; set; } = DateTime.Today;
        [BindProperty] public string? AppProduct { get; set; }
        [BindProperty] public string? Stage { get; set; } = "DDEQC";

        //Customer information
        [BindProperty] public int? CustomerID { get; set; }
        [BindProperty] public string FirstName { get; set; } = "";
        [BindProperty] public string? MiddleName { get; set; }
        [BindProperty] public string? LastName { get; set; }
        [BindProperty] public string? Nationality { get; set; }
        [BindProperty] public string? CustomerType { get; set; }
        [BindProperty] public string? Mobile { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public DateTime? DOB { get; set; }


        //emloyment information
        [BindProperty] public string? Employer { get; set; }
        [BindProperty] public string? EmployerCatg { get; set; }
        [BindProperty] public string? EmployerActivity { get; set; }
        [BindProperty] public decimal? Salary { get; set; }
        [BindProperty] public DateTime? DOJ { get; set; }
        [BindProperty] public decimal? LOS { get; set; }
        [BindProperty] public string? BankingWith { get; set; }
        [BindProperty] public string? AccountNumber { get; set; }
        //------------------------------------



        //---------Property Details---------------------------
        [BindProperty] public string? PropertyType { get; set; }
        [BindProperty] public string? PropertyDesc { get; set; }
        [BindProperty] public string? MortgageDegree { get; set; }
        [BindProperty] public string? MasterDevelopmentName { get; set; }
        [BindProperty] public string? MasterDeveloperName { get; set; }
        [BindProperty] public string? ProjectName { get; set; }
        [BindProperty] public string? DeveloperName { get; set; }
        [BindProperty] public string? BuildingName { get; set; }
        [BindProperty] public decimal? OriginalPropertyValue { get; set; }
        [BindProperty] public decimal? AppraisalValue { get; set; }
        [BindProperty] public string? AppraisalBy { get; set; }
        [BindProperty] public DateTime? AppraisalDate { get; set; }
        [BindProperty] public string? PropertyStatus { get; set; }
        [BindProperty] public DateTime? DateOfCompletion { get; set; }
        //------------------------------------


        //---------Property Address Details ----------

        [BindProperty] public string? LotNo { get; set; }
        [BindProperty] public string? PlotNo { get; set; }
        [BindProperty] public string? TitleDeedNo { get; set; }
        [BindProperty] public decimal? LandArea { get; set; }
        [BindProperty] public decimal? BuiltupArea { get; set; }
        [BindProperty] public decimal? PropertyValuePerUnit { get; set; }
        [BindProperty] public string? RegistrationType { get; set; }
        [BindProperty] public string? RegistrationNumber { get; set; }
        [BindProperty] public string? RegisteredOnNameOf { get; set; }
        [BindProperty] public string? Relation { get; set; }
        [BindProperty] public DateTime? RegistrationDate { get; set; }
        [BindProperty] public string? IssuedFrom { get; set; }
        [BindProperty] public string? VillaAptNo { get; set; }
        [BindProperty] public string? BuildingCompoundName { get; set; }
        [BindProperty] public string? StreetName { get; set; }

        [BindProperty] public string? Country { get; set; }
        [BindProperty] public string? Emirate { get; set; }
        [BindProperty] public string? AreaName { get; set; }

        //---------Finance Details-------------------
        [BindProperty] public decimal? OriginalPrice { get; set; }
        [BindProperty] public decimal? Downpayment { get; set; }
        [BindProperty] public decimal? NFA { get; set; }

        [BindProperty] public decimal? ProfitRate { get; set; }
        [BindProperty] public int? Tenor { get; set; }
        [BindProperty] public decimal? DSR { get; set; }
        [BindProperty] public string? SalesName { get; set; }
        [BindProperty] public string? Remarks { get; set; }



        public List<Dictionary<string, object>> drpProducts { get; set; } = new();
        public List<Dictionary<string, object>> drpNationalities { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerCatg { get; set; } = new();
        public List<Dictionary<string, object>> drpEmployerActivity { get; set; } = new();
        public List<Dictionary<string, object>> drpStages { get; set; } = new();
        public List<Dictionary<string, object>> drpBankCode_UAECB { get; set; } = new();
        public List<Dictionary<string, object>> drpPropertyStatus { get; set; } = new();
       public List<SelectListItem> CustomerList { get; set; } = new();

        private void LoadDummyData(int appId)
        {
            //var row = DaoHelper.DRow("SELECT * FROM AS_Entries WHERE AppID = 41");
            var row = DaoHelper.DRow($"SELECT * FROM AS_Entries WHERE AppID = {appId}");


            AppID_usr = row["AppID_usr"]?.ToString();
            AppProduct = row["AppProduct"]?.ToString();
            Stage = row["Stage"]?.ToString();

            FirstName = row["FirstName"]?.ToString();
            MiddleName = row["MiddleName"]?.ToString();
            LastName = row["LastName"]?.ToString();

            // etc ...
        }
        private void LoadDummyData_text()
        {
            // Application
            AppID_usr = "TEST001";
            AppDate_usr = DateTime.Today;
            AppProduct = "Home Finance";
            Stage = "DDEQC";

            // Customer
            FirstName = "Fayez";
            MiddleName = "Mustafa";
            LastName = "Kurdi";
            Nationality = "United Arab Emirates";
            CustomerType = "Salaried";
            //DOB = new DateTime(1985, 5, 15);
            DOB = Convert.ToDateTime("1985-Feb-05");

            // Employment
            Employer = "Dubai Islamic Bank";
            EmployerCatg = "Government";
            EmployerActivity = "Banking";
            Salary = 25000;
            DOJ = new DateTime(2015, 1, 1);
            LOS = 11.8m;
            BankingWith = "DIB";
            AccountNumber = "1234567890";

            // Property Details
            PropertyType = "Villa";
            PropertyDesc = "5 Bedroom Villa";
            MortgageDegree = "First Degree";
            MasterDevelopmentName = "Arabian Ranches";
            MasterDeveloperName = "Emaar";
            ProjectName = "Ranches Phase 3";
            DeveloperName = "Emaar";
            BuildingName = "Villa Cluster A";
            OriginalPropertyValue = 2500000;
            AppraisalValue = 2450000;
            AppraisalBy = "Land Sterling";
            AppraisalDate = DateTime.Today.AddDays(-15);
            PropertyStatus = "Completed";
            DateOfCompletion = new DateTime(2022, 12, 31);

            // Property Address
            LotNo = "L-100";
            PlotNo = "P-200";
            TitleDeedNo = "TD-123456";
            LandArea = 8000;
            BuiltupArea = 5500;
            PropertyValuePerUnit = 450;
            RegistrationType = "Title Deed";
            RegistrationNumber = "REG-5555";
            RegisteredOnNameOf = "Ahmed Ali";
            Relation = "Self";
            RegistrationDate = new DateTime(2023, 1, 15);
            IssuedFrom = "Dubai Land Department";
            VillaAptNo = "Villa 45";
            BuildingCompoundName = "Arabian Ranches";
            StreetName = "Sheikh Mohammed Bin Zayed Road";
            Country = "United Arab Emirates";
            Emirate = "Dubai";
            AreaName = "Arabian Ranches";

            // Finance
            OriginalPrice = 2500000;
            Downpayment = 500000;
            NFA = 2000000;
            ProfitRate = 4.99m;
            Tenor = 25;
            DSR = 42.50m;
            SalesName = "Khalid Test";
            Remarks = "Auto-generated test record";
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

            if (!CustomerID.HasValue)
                return Page();

            string sql = $@"SELECT * FROM Customers WHERE CustomerID = {CustomerID}";
            var rows = DaoHelper.DList(sql);
            if (rows.Count == 0)
                return Page();
            var row = rows[0];

            FirstName = row["FirstName"]?.ToString();
            MiddleName = row["MiddleName"]?.ToString();
            LastName = row["LastName"]?.ToString();
            CustomerType = row["CustomerType"]?.ToString();
            Nationality = row["Nationality"]?.ToString();
            Mobile = row["Mobile"]?.ToString();
            Email = row["Email"]?.ToString();
            Employer = row["Employer"]?.ToString();
            EmployerCatg = row["EmployerCatg"]?.ToString();
            EmployerActivity = row["EmployerActivity"]?.ToString();
            BankingWith = row["BankingWith"]?.ToString();

            if (row["DOB"] != DBNull.Value)
                DOB = Convert.ToDateTime(row["DOB"]);
            if (row["DOJ"] != DBNull.Value)
                DOJ = Convert.ToDateTime(row["DOJ"]);
            if (row["Salary"] != DBNull.Value)
                Salary = Convert.ToDecimal(row["Salary"]);

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
            AppID_usr = $"APP-{DateTime.Now.Year}-{nextId:000000}";


            drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
            drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
            drpEmployerCatg = DaoHelper.DList(@"SELECT Description As EmployerCatg FROM GeneralCodes where CodeCategory='EmployerCatg'");
            drpEmployerActivity = DaoHelper.DList(@"SELECT Description As EmployerActivity FROM GeneralCodes where CodeCategory='EmployerActivity'");
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
            drpBankCode_UAECB = DaoHelper.DList(@"SELECT Description As BankCode_UAECB FROM GeneralCodes where CodeCategory='BankCode_UAECB'");
            drpPropertyStatus = DaoHelper.DList(@"SELECT Description As PropertyStatus FROM GeneralCodes where CodeCategory='PropertyStatus'");

            LoadCustomers();


        }
        //====================================================================================================================================





        //====================================================================================================================================
        //                                                  POST handler for form submission
        //====================================================================================================================================

        public IActionResult OnPost()
        {

            // Calculate LOS from DOJ
            if (DOJ.HasValue)
            {
                LOS = Math.Round(((decimal)(DateTime.Today - DOJ.Value).TotalDays) / 365.25m, 2);
            }

            NFA = (OriginalPrice ?? 0) - (Downpayment ?? 0);

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
                    '{AppID_usr}',
                    '{AppDate_usr:yyyy-MM-dd}',
                    '{AppProduct}',
                    '{Stage}',

                    {(CustomerID.HasValue ? CustomerID.Value.ToString() : "NULL")},
                    '{FirstName}',
                    '{MiddleName}',
                    '{LastName}',

                    '{Nationality}',
                    '{CustomerType}',
                    {(DOB.HasValue ? $"'{DOB.Value:yyyy-MM-dd}'" : "NULL")},

                    'Admin',
                    'Admin',
                    '{Employer}',
                    '{EmployerCatg}',
                    '{EmployerActivity}',
                    {(Salary.HasValue ? Salary.Value.ToString() : "NULL")},
                    {(DOJ.HasValue ? $"'{DOJ.Value:yyyy-MM-dd}'" : "NULL")},
                    {(LOS.HasValue ? LOS.Value.ToString() : "NULL")},
                    '{BankingWith}',
                    '{AccountNumber}'

                    ,'{PropertyType}'
                    ,'{PropertyDesc}'
                    ,'{MortgageDegree}'
                    ,'{MasterDevelopmentName}'
                    ,'{MasterDeveloperName}'
                    ,'{ProjectName}'
                    ,'{DeveloperName}'
                    ,'{BuildingName}'
                    ,{(OriginalPropertyValue.HasValue ? OriginalPropertyValue.Value.ToString() : "NULL")}
                    ,{(AppraisalValue.HasValue ? AppraisalValue.Value.ToString() : "NULL")}
                    ,'{AppraisalBy}'
                    ,{(AppraisalDate.HasValue ? $"'{AppraisalDate.Value:yyyy-MM-dd}'" : "NULL")}
                    ,'{PropertyStatus}'
                    ,{(DateOfCompletion.HasValue ? $"'{DateOfCompletion.Value:yyyy-MM-dd}'" : "NULL")}

                    ,'{LotNo}'
                    ,'{PlotNo}'
                    ,'{TitleDeedNo}'
                    ,{(LandArea.HasValue ? LandArea.Value.ToString() : "NULL")}
                    ,{(BuiltupArea.HasValue ? BuiltupArea.Value.ToString() : "NULL")}
                    ,{(PropertyValuePerUnit.HasValue ? PropertyValuePerUnit.Value.ToString() : "NULL")}
                    ,'{RegistrationType}'
                    ,'{RegistrationNumber}'
                    ,'{RegisteredOnNameOf}'
                    ,'{Relation}'
                    ,{(RegistrationDate.HasValue ? $"'{RegistrationDate.Value:yyyy-MM-dd}'" : "NULL")}
                    ,'{IssuedFrom}'
                    ,'{VillaAptNo}'
                    ,'{BuildingCompoundName}'
                    ,'{StreetName}'
                    ,'{Country}'
                    ,'{Emirate}'
                    ,'{AreaName}'

                    ,{(OriginalPrice.HasValue ? OriginalPrice.Value.ToString() : "NULL")}
                    ,{(Downpayment.HasValue ? Downpayment.Value.ToString() : "NULL")}
                    ,{(NFA.HasValue ? NFA.Value.ToString() : "NULL")}
                    ,{(ProfitRate.HasValue ? ProfitRate.Value.ToString() : "NULL")}
                    ,{(Tenor.HasValue ? Tenor.Value.ToString() : "NULL")}
                    ,{(DSR.HasValue ? DSR.Value.ToString() : "NULL")}
                    ,'{SalesName}'
                    ,'{Remarks}'

                )";


            //Console.WriteLine(sql);
           // throw new Exception(sql);
            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }
    }
}