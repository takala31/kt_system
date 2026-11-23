using kt_system.Helpers;
using kt_system.Models;
using KT_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Security.Cryptography;

namespace kt_system.Pages.Applications
{
    public class applicationModel : PageModel
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
        public List<Dictionary<string, object>> drpPropertyType { get; set; } = new();
        public List<Dictionary<string, object>> drpRelationType { get; set; } = new();
        public List<Dictionary<string, object>> drpRegistrationType { get; set; } = new();
        public List<SelectListItem> CustomerList { get; set; } = new();
        [BindProperty] public string Mode { get; set; } = "";

        public DataTable dtLog { get; set; } = new();

        //====================================================================================================================================
        //                                                  GET handler for form opening
        //====================================================================================================================================
        //public void OnGet(bool loadsample = false, int? appid = null)
        //public void OnGet(int id)
        //{
        private void LoadDummyData_text()
        {
            // Application
            if (Mode == "New")
            {
                App.AppID = DaoHelper.DMaxInt("AppID", "as_entries", "1=1") + 1;
                App.AppID_usr = "APP-2026-" + App.AppID.ToString("D6");
            }
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
            Prop.AreaName = "Villa";
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

        private void LoadPageData()
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
        }

        public void OnGet(int? id, string mode)
        {

            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) {Response.Redirect("/Account/Login"); return;}
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) {Response.Redirect("/Index?login=1"); return;}

            dtLog = DaoHelper.GetDataTable($"SELECT * FROM AppLog WHERE AppID = {id} ORDER BY ChangedDate DESC");

            LoadPageData();

            Mode = mode;
            if (Mode == "New")
            {
                App = new Application();
                LoadDummyData_text();
                return;
            }

            //IsEditMode = true;
            // Load existing application
            var row = DaoHelper.DRow($@"SELECT * FROM AS_Entries WHERE AppID={id.Value}");

            App.AppID = id.Value;
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

            #region Valiation Block
            var errors = ValidateApplication();
            if (errors.Count > 0)
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] = "• " + string.Join("<br>• ", errors);
                LoadPageData();
                return Page();
            }
            #endregion

            App.NFA = (App.OriginalPrice ?? 0) - (App.Downpayment ?? 0);
                #region EDIT
                string sqlEdit;
                if (Mode == "Edit")
                {sqlEdit = $@"
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

                DaoHelper.DExecute(sqlEdit);
                AppLogHelper(App.AppID, "UPDATE");

                TempData["alertColor"] = "success";
                TempData["alertMessage"] = "Changes saved successfully.";
                return RedirectToPage(new { id = App.AppID, mode = "Edit" });
                //return RedirectToPage(new { id = App.AppID });
            }
            #endregion

            #region INSERT
            else if (Mode == "New")
            {
                if (Empl.DOJ.HasValue) Empl.LOS = Math.Round(((decimal)(DateTime.Today - Empl.DOJ.Value).TotalDays) / 365.25m, 2);
                App.NFA = (App.OriginalPrice ?? 0) - (App.Downpayment ?? 0);  //throw new Exception("STEP 1");

                string sqlInsert;
                sqlInsert = $@"
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

                    {(Cust.CustomerID.HasValue ? Cust.CustomerID.Value.ToString() : "NULL")},
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

                    DaoHelper.DExecute(sqlInsert);
                    AppLogHelper(App.AppID, "CREATE");


                    TempData["alertColor"] = "success";
                    TempData["alertMessage"] = "New application added successfully.";
                    return RedirectToPage(new{id = App.AppID,mode = "Edit"});
                #endregion
            }
            //else
            throw new Exception("Invalid Mode: " + Mode);
        }

        private void AppLogHelper (BigInteger AppId, string LogAction)
            {
                string LogChangedBy =HttpContext.Session.GetString("UserName") ?? "";                   
                if (AppId <= 0) return;

                string sql = $@"INSERT INTO AS_Entries_Log
                (
                    Action,
                    ChangedBy,
                    ChangedDate,
                    AppID,
                    AppID_UNI,
                    AppID_usr,
                    AppDate_usr,
                    AppProduct,
                    Stage,
                    FirstName,
                    MiddleName,
                    LastName,
                    AraName,
                    DOB,
                    CustomerAge,
                    Nationality,
                    CustomerType,
                    Employer,
                    EmployerCatg,
                    EmployerActivity,
                    Salary,
                    DOJ,
                    LOS,
                    BankingWith,
                    AccountNumber,
                    CarType,
                    CarBrand,
                    CarModel,
                    CarAgency,
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
                    Remarks,
                    CreatedBy,
                    CreatedOn,
                    LastUpdatedBy,
                    LastUpdatedOn,
                    CustomerID
                )
                SELECT
                    '{LogAction}',
                    '{LogChangedBy}',
                    GETDATE(),
                    AppID,
                    AppID_UNI,
                    AppID_usr,
                    AppDate_usr,
                    AppProduct,
                    Stage,
                    FirstName,
                    MiddleName,
                    LastName,
                    AraName,
                    DOB,
                    CustomerAge,
                    Nationality,
                    CustomerType,
                    Employer,
                    EmployerCatg,
                    EmployerActivity,
                    Salary,
                    DOJ,
                    LOS,
                    BankingWith,
                    AccountNumber,
                    CarType,
                    CarBrand,
                    CarModel,
                    CarAgency,
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
                    Remarks,
                    CreatedBy,
                    CreatedOn,
                    LastUpdatedBy,
                    LastUpdatedOn,
                    CustomerID
                FROM AS_Entries
                WHERE AppID= " + App.AppID;

                        DaoHelper.DExecute(sql);
                    }


        private List<string> ValidateApplication()
        {
            List<string> errors = new();

            if (string.IsNullOrWhiteSpace(App.Stage)) errors.Add("Stage is required");
            if (string.IsNullOrWhiteSpace(Cust.FirstName)) errors.Add("First Name is required");
            if (string.IsNullOrWhiteSpace(Cust.LastName)) errors.Add("Last Name is required");
            if (string.IsNullOrWhiteSpace(Cust.AraName)) errors.Add("Arabic Name is required");             
            if (string.IsNullOrWhiteSpace(Cust.CustomerType)) errors.Add("Customer Type is required");
            if (string.IsNullOrWhiteSpace(App.AppProduct)) errors.Add("Product is required");

            return errors;
        }
    }

}

//Console.WriteLine(sql);
//throw new Exception(sql);