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
            //var row = DaoHelper.DRow("SELECT * FROM AS_Entries WHERE AppID = " + appId);
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
            Cust.AraName = "ÝÇíÒ ãÕØÝì ßÑÏí";
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
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages where owner='sales' Order by Owner, Description_Action");
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
            appid = Convert.ToInt32(nextId);
            App.AppID_usr = $"APP-{DateTime.Now.Year}-{nextId:000000}";


            drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
            drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
            drpEmployerCatg = DaoHelper.DList(@"SELECT Description As EmployerCatg FROM GeneralCodes where CodeCategory='EmployerCatg'");
            drpEmployerActivity = DaoHelper.DList(@"SELECT Description As EmployerActivity FROM GeneralCodes where CodeCategory='EmployerActivity'");
            //drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
            drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages where owner='sales' Order by Owner, Description_Action");
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

            //throw new Exception("ONPOST FIRED");
            // Calculate LOS from DOJ
            if (Empl.DOJ.HasValue)
            {
                Empl.LOS = Math.Round(((decimal)(DateTime.Today - Empl.DOJ.Value).TotalDays) / 365.25m, 2);
            }

            App.NFA = (App.OriginalPrice ?? 0) - (App.Downpayment ?? 0);
            //throw new Exception("STEP 1");

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
                    AraName,
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
                    '{Cust.AraName}',

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
            //throw new Exception(sql);
            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }
    }
}