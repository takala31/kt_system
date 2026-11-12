using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Applications
{
    public class EditModel : PageModel
    {

        //Application information
        [BindProperty] public int AppID { get; set; }
        [BindProperty] public string? AppID_usr { get; set; }
        [BindProperty] public DateTime AppDate_usr { get; set; }
        [BindProperty] public string? AppProduct { get; set; }
        [BindProperty] public string? Stage { get; set; }

        //Customer information
        [BindProperty] public string FirstName { get; set; }
        [BindProperty] public string? MiddleName { get; set; }
        [BindProperty] public string? LastName { get; set; }
        [BindProperty] public string? Nationality { get; set; }
        [BindProperty] public string? CustomerType { get; set; }
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
        [BindProperty] public string? LastUpdatedBy { get; set; }


        public List<Dictionary<string, object>> drpProducts { get; set; } = new();
        public List<Dictionary<string, object>> drpNationalities { get; set; } = new();
        public List<Dictionary<string, object>> drpStages { get; set; } = new();


        //------------OnGet--------------

        public void OnGet(int id)

        {

            {
                //int nextId = DaoHelper.DMaxInt("AppID", "AS_Entries", "1=1") + 1;
                //AppID_usr = $"APP-{DateTime.Now.Year}-{nextId:000000}";
                drpProducts = DaoHelper.DList(@"SELECT Description As ProductCode FROM GeneralCodes where CodeCategory='Segments'");
                drpNationalities = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
                drpStages = DaoHelper.DList(@"SELECT Code As Stage FROM AS_Codes_stages Order by Owner, Code");
            }

            var row = DaoHelper.DRow($@"SELECT * FROM AS_Entries WHERE AppID={id}");

            AppID = id;
            AppID_usr = row["AppID_usr"]?.ToString();
            AppProduct = row["AppProduct"]?.ToString();
            Stage = row["Stage"]?.ToString();

            FirstName = row["FirstName"]?.ToString();
            MiddleName = row["MiddleName"]?.ToString();
            LastName = row["LastName"]?.ToString();
            Nationality = row["Nationality"]?.ToString();
            CustomerType = row["CustomerType"]?.ToString();

            EmployerCatg = row["EmployerCatg"]?.ToString();
            Employer = row["Employer"]?.ToString();
            EmployerActivity = row["EmployerActivity"]?.ToString();
            Salary = decimal.TryParse(row["Salary"]?.ToString(), out var sal) ? sal : null;
            DOJ = row["DOJ"] == DBNull.Value ? null : Convert.ToDateTime(row["DOJ"]);
            LOS = decimal.TryParse(row["LOS"]?.ToString(), out var los) ? los : null;
            BankingWith = row["BankingWith"]?.ToString();
            AccountNumber = row["AccountNumber"]?.ToString();

            PropertyType = row["PropertyType"]?.ToString();
            PropertyDesc = row["PropertyDesc"]?.ToString();
            MortgageDegree = row["MortgageDegree"]?.ToString();

            MasterDevelopmentName = row["MasterDevelopmentName"]?.ToString();
            MasterDeveloperName = row["MasterDeveloperName"]?.ToString();
            ProjectName = row["ProjectName"]?.ToString();
            DeveloperName = row["DeveloperName"]?.ToString();
            BuildingName = row["BuildingName"]?.ToString();

            OriginalPropertyValue = decimal.TryParse(row["OriginalPropertyValue"]?.ToString(), out var opv) ? opv : null;
            AppraisalValue = decimal.TryParse(row["AppraisalValue"]?.ToString(), out var av) ? av : null;
            AppraisalBy = row["AppraisalBy"]?.ToString();
            AppraisalDate = row["AppraisalDate"] == DBNull.Value ? null : Convert.ToDateTime(row["AppraisalDate"]);
            PropertyStatus = row["PropertyStatus"]?.ToString();
            DateOfCompletion = row["DateOfCompletion"] == DBNull.Value ? null : Convert.ToDateTime(row["DateOfCompletion"]);

            LotNo = row["LotNo"]?.ToString();
            PlotNo = row["PlotNo"]?.ToString();
            TitleDeedNo = row["TitleDeedNo"]?.ToString();
            LandArea = decimal.TryParse(row["LandArea"]?.ToString(), out var la) ? la : null;
            BuiltupArea = decimal.TryParse(row["BuiltupArea"]?.ToString(), out var bua) ? bua : null;
            PropertyValuePerUnit = decimal.TryParse(row["PropertyValuePerUnit"]?.ToString(), out var pvpu) ? pvpu : null;
            RegistrationType = row["RegistrationType"]?.ToString();
            RegistrationNumber = row["RegistrationNumber"]?.ToString();
            RegisteredOnNameOf = row["RegisteredOnNameOf"]?.ToString();
            Relation = row["Relation"]?.ToString();
            RegistrationDate = row["RegistrationDate"] == DBNull.Value ? null : Convert.ToDateTime(row["RegistrationDate"]);
            IssuedFrom = row["IssuedFrom"]?.ToString();
            VillaAptNo = row["VillaAptNo"]?.ToString();
            BuildingCompoundName = row["BuildingCompoundName"]?.ToString();
            StreetName = row["StreetName"]?.ToString();
            Country = row["Country"]?.ToString();
            Emirate = row["Emirate"]?.ToString();
            AreaName = row["AreaName"]?.ToString();

            OriginalPrice = decimal.TryParse(row["OriginalPrice"]?.ToString(), out var op) ? op : null;
            Downpayment = decimal.TryParse(row["Downpayment"]?.ToString(), out var dp) ? dp : null;
            NFA = decimal.TryParse(row["NFA"]?.ToString(), out var nfa) ? nfa : null;
            ProfitRate = decimal.TryParse(row["ProfitRate"]?.ToString(), out var pr) ? pr : null;
            Tenor = int.TryParse(row["Tenor"]?.ToString(), out var tenor) ? tenor : null;
            DSR = decimal.TryParse(row["DSR"]?.ToString(), out var dsr) ? dsr : null;

            SalesName = row["SalesName"]?.ToString();
            Remarks = row["Remarks"]?.ToString();

        }



        public IActionResult OnPost()
        {
            NFA = (OriginalPrice ?? 0) - (Downpayment ?? 0);

            string sql = $@"
                        UPDATE AS_Entries
                        SET
                            AppID_usr='{AppID_usr}',
                            AppProduct='{AppProduct}',
                            Stage='{Stage}',

                            FirstName='{FirstName}',
                            MiddleName='{MiddleName}',
                            LastName='{LastName}',
                            Nationality='{Nationality}',
                            CustomerType='{CustomerType}',    
                            DOB='{DOB}',    

                            EmployerCatg='{EmployerCatg}',
                            Employer='{Employer}',
                            EmployerActivity='{EmployerActivity}',
                            Salary={(Salary.HasValue?Salary.Value.ToString() : "NULL")},
                            DOJ={(DOJ.HasValue ? $"'{DOJ:yyyy-MM-dd}'" : "NULL")},
                            LOS={(LOS.HasValue ? LOS.Value.ToString() : "NULL")},
                            BankingWith='{BankingWith}',
                            AccountNumber='{AccountNumber}',

                            PropertyType='{PropertyType}',
                            PropertyDesc='{PropertyDesc}',
                            MortgageDegree='{MortgageDegree}',
                            MasterDevelopmentName='{MasterDevelopmentName}',
                            MasterDeveloperName='{MasterDeveloperName}',
                            ProjectName='{ProjectName}',
                            DeveloperName='{DeveloperName}',
                            BuildingName='{BuildingName}',

                            OriginalPropertyValue={(OriginalPropertyValue.HasValue ? OriginalPropertyValue.Value.ToString() : "NULL")},
                            AppraisalValue={(AppraisalValue.HasValue ? AppraisalValue.Value.ToString() : "NULL")},
                            AppraisalBy='{AppraisalBy}',
                            AppraisalDate={(AppraisalDate.HasValue ? $"'{AppraisalDate:yyyy-MM-dd}'" : "NULL")},
                            PropertyStatus='{PropertyStatus}',
                            DateOfCompletion={(DateOfCompletion.HasValue ? $"'{DateOfCompletion:yyyy-MM-dd}'" : "NULL")},

                            LotNo='{LotNo}',
                            PlotNo='{PlotNo}',
                            TitleDeedNo='{TitleDeedNo}',
                            LandArea={(LandArea.HasValue ? LandArea.Value.ToString() : "NULL")},
                            BuiltupArea={(BuiltupArea.HasValue ? BuiltupArea.Value.ToString() : "NULL")},
                            PropertyValuePerUnit={(PropertyValuePerUnit.HasValue ? PropertyValuePerUnit.Value.ToString() : "NULL")},

                            RegistrationType='{RegistrationType}',
                            RegistrationNumber='{RegistrationNumber}',
                            RegisteredOnNameOf='{RegisteredOnNameOf}',
                            Relation='{Relation}',
                            RegistrationDate={(RegistrationDate.HasValue ? $"'{RegistrationDate:yyyy-MM-dd}'" : "NULL")},
                            IssuedFrom='{IssuedFrom}',
                            VillaAptNo='{VillaAptNo}',
                            BuildingCompoundName='{BuildingCompoundName}',
                            StreetName='{StreetName}',
                            Country='{Country}',
                            Emirate='{Emirate}',
                            AreaName='{AreaName}',

                            OriginalPrice={(OriginalPrice.HasValue ? OriginalPrice.Value.ToString() : "NULL")},
                            Downpayment={(Downpayment.HasValue ? Downpayment.Value.ToString() : "NULL")},
                            NFA={(NFA.HasValue ? NFA.Value.ToString() : "NULL")},
                            ProfitRate={(ProfitRate.HasValue? ProfitRate.Value.ToString():"NULL")},
                            Tenor={(Tenor.HasValue? Tenor.Value.ToString() : "NULL")},
                            DSR={(DSR.HasValue ? DSR.Value.ToString(): "NULL")},


                            SalesName='{SalesName}',
                            Remarks='{Remarks}',

                            LastUpdatedBy ='{LastUpdatedBy}',
                            LastUpdatedOn=GETDATE() 
                            WHERE AppID={AppID}";

            //System.IO.File.WriteAllText(@"C:\Temp\UpdateSQL.txt",sql);
            //Console.WriteLine(sql);

            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }
    }
}


