using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Applications
{
    public class ApplicationIndexModel : PageModel
    {
        public int AllCount { get; set; }
        public int LeadCount { get; set; }
        public int OnProgressCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int DDEQCCount { get; set; }
        public int EndCount { get; set; }
        public List<Dictionary<string, object>> Apps { get; set; } = new();

        public string? SearchText { get; set; }
        public string? Stage { get; set; }


        private void LoadPageData()
        {
            string _UserRole = HttpContext.Session.GetString("UserRole");
            string _UserName = HttpContext.Session.GetString("UserName");
            if (_UserRole == "User")
            {
                AllCount = DaoHelper.DCount("AS_Entries", $"CreatedBy='{_UserName}'");
                LeadCount = DaoHelper.DCount("AS_Entries", $"Stage='Lead' AND CreatedBy='{_UserName}'");
                OnProgressCount = DaoHelper.DCount("AS_Entries", $"Stage='OnProgress' AND CreatedBy='{_UserName}'");
                ApprovedCount = DaoHelper.DCount("AS_Entries", $"Stage='Approved' AND CreatedBy='{_UserName}'");
                RejectedCount = DaoHelper.DCount("AS_Entries", $"Stage='Rejected' AND CreatedBy='{_UserName}'");
                DDEQCCount = DaoHelper.DCount("AS_Entries", $"Stage='DDEQC' AND CreatedBy='{_UserName}'");
                EndCount = DaoHelper.DCount("AS_Entries", $"Stage='End' AND CreatedBy='{_UserName}'");

            }
            else if (_UserRole == "Admin" || _UserRole == "Supervisor")
            {
                AllCount = DaoHelper.DCount("AS_Entries", "1=1");
                LeadCount = DaoHelper.DCount("AS_Entries", "Stage='Lead'");
                OnProgressCount = DaoHelper.DCount("AS_Entries", "Stage='OnProgress'");
                ApprovedCount = DaoHelper.DCount("AS_Entries", "Stage='Approved'");
                RejectedCount = DaoHelper.DCount("AS_Entries", "Stage='Rejected'");
                DDEQCCount = DaoHelper.DCount("AS_Entries", "Stage='DDEQC'");
                EndCount = DaoHelper.DCount("AS_Entries", "Stage='End'");
                }
            }

        //OnGet start
        public void OnGet(string? stagePassed, string? searchText)
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) { Response.Redirect("/Index?login=1"); return; }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                HttpContext.Session.SetString("RequestingPage", "/Applications/ApplicationIndex");
                Response.Redirect("/Index?login=1"); return;
            }


            LoadPageData();

            //Search parameters
            Stage = stagePassed;
            SearchText = searchText;
            string sql = "SELECT * FROM AS_Entries WHERE 1=1";

            string _UserRole = HttpContext.Session.GetString("UserRole");
            string _UserName = HttpContext.Session.GetString("UserName");

            if (_UserRole == "User") sql += $" AND CreatedBy = '{_UserName}'";

            if (string.IsNullOrEmpty(stagePassed) && string.IsNullOrEmpty(searchText))
            {
                sql += " ORDER BY AppID DESC";
                Apps = new();
                Apps = DaoHelper.DList(sql);
                return;
            }

            if (!string.IsNullOrEmpty(stagePassed) && stagePassed != "All")
            {
                sql += $" AND Stage='{stagePassed}'";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                sql += $@"
                        AND (
                            FirstName LIKE '%{searchText}%'
                            OR MiddleName LIKE '%{searchText}%'
                            OR LastName LIKE '%{searchText}%'
                            OR CONCAT(FirstName,' ',MiddleName,' ',LastName) LIKE '%{searchText}%'
                            OR AraName LIKE '%{searchText}%'
                            OR AppID_usr LIKE '%{searchText}%'
                            OR Employer LIKE '%{searchText}%'
                            OR Nationality LIKE '%{searchText}%'
                            OR Nationality LIKE '%{searchText}%'
                            OR CreatedBy LIKE '%{searchText}%'
                            OR Stage LIKE '%{searchText}%'
            )";
            }

            sql += " ORDER BY AppID DESC";

            Apps = DaoHelper.DList(sql);
        }




        //public string? Stage { get; set; }

        //public void OnGet(string? stagePassed)
        //{
        //    Stage = stagePassed;

        //    AllCount = DaoHelper.DCount("AS_Entries", "1=1");
        //    LeadCount = DaoHelper.DCount("AS_Entries", "Stage='Lead'");
        //    OnProgressCount = DaoHelper.DCount("AS_Entries", "Stage='OnProgress'");
        //    ApprovedCount = DaoHelper.DCount("AS_Entries", "Stage='Approved'");
        //    RejectedCount = DaoHelper.DCount("AS_Entries", "Stage='Rejected'");
        //    DDEQCCount = DaoHelper.DCount("AS_Entries", "Stage='DDEQC'");

        //    if (string.IsNullOrEmpty(stagePassed))
        //        return;

        //    string sql;

        //    if (stagePassed == "All")
        //    {
        //        sql = "SELECT * FROM AS_Entries ORDER BY AppID DESC";
        //    }
        //    else
        //    {
        //        sql = $"SELECT * FROM AS_Entries WHERE Stage='{stagePassed}'";
        //    }

        //    Apps = DaoHelper.DList(sql);
        //}











































































        //public string? Stage { get; set; }

        //public void OnGet(string? stagePassed)
        //{

        //    string sql = "";
        //    Stage = stagePassed;
        //    AllCount = DaoHelper.DCount("AS_Entries", "1=1");
        //    LeadCount = DaoHelper.DCount("AS_Entries", "Stage='Lead'");
        //    OnProgressCount = DaoHelper.DCount("AS_Entries", "Stage='OnProgress'");
        //    ApprovedCount = DaoHelper.DCount("AS_Entries", "Stage='Approved'");
        //    RejectedCount = DaoHelper.DCount("AS_Entries", "Stage='Rejected'");
        //    DDEQCCount = DaoHelper.DCount("AS_Entries", "Stage='DDEQC'");

        //    if (string.IsNullOrEmpty(stagePassed))
        //    {
        //        return;
        //    }

        //    else if (stagePassed == "All")
        //    {
        //        sql = $"SELECT * FROM AS_Entries ORDER BY AppID DESC'";
        //        Apps = DaoHelper.DList(sql);
        //    }

        //    else
        //        sql =$"SELECT * FROM AS_Entries WHERE Stage='{stagePassed}'";
        //        Apps = DaoHelper.DList(sql);
        //}



        //public void OnGet()
        //{
        //    AllCount = DaoHelper.DCount("AS_Entries", "1=1");
        //    LeadCount = DaoHelper.DCount("AS_Entries", "Stage='Lead'");
        //    OnProgressCount = DaoHelper.DCount("AS_Entries", "Stage='OnProgress'");
        //    ApprovedCount = DaoHelper.DCount("AS_Entries", "Stage='Approved'");
        //    RejectedCount = DaoHelper.DCount("AS_Entries", "Stage='Rejected'");
        //    DDEQCCount = DaoHelper.DCount("AS_Entries", "Stage='DDEQC'");

        //    if (string.IsNullOrEmpty(Stage) || Stage == "All")
        //    {
        //        string sql = $@"
        //            SELECT TOP 50
        //                AppID,
        //                RIGHT(AppID_usr,11) AS AppID_usr,
        //                FirstName,
        //                LastName,
        //                Nationality,
        //                Employer,   
        //                AppProduct,
        //                PropertyType,

        //                OriginalPrice,
        //                Downpayment,
        //                NFA,
        //                ProfitRate,
        //                Tenor,

        //                Stage,
        //                CreatedOn
        //            FROM AS_Entries
        //            ORDER BY AppID DESC";
        //        System.Diagnostics.Debug.WriteLine(sql);
        //        Apps = DaoHelper.DList(sql);            
        //    }
        //    else
        //    {
        //        string sql = $@"
        //        SELECT
        //            AppID,
        //            RIGHT(AppID_usr,11) AS AppID_usr,
        //            FirstName,
        //            LastName,
        //            Nationality,
        //            Employer,   
        //            AppProduct,
        //            PropertyType,

        //            OriginalPrice,
        //            Downpayment,
        //            NFA,
        //            ProfitRate,
        //            Tenor,

        //            Stage,
        //            CreatedOn
        //        FROM AS_Entries 
        //        WHERE Stage = '{Stage}'
        //        ORDER BY AppID DESC";

        //        System.Diagnostics.Debug.WriteLine(sql);
        //        Apps = DaoHelper.DList(sql);
        //    }
        //}
    }
}


//       [AppID]
//      ,[AppID_UNI]
//      ,[AppID_usr]
//      ,[AppDate_usr]
//      ,[AppProduct]
//      ,[Stage]
//      ,[FirstName]
//      ,[MiddleName]
//      ,[LastName]
//      ,[AraName]
//      ,[DOB]
//      ,[CustomerAge]
//      ,[Nationality]
//      ,[CustomerType]
//      ,[Employer]
//      ,[EmployerCatg]
//      ,[EmployerActivity]
//      ,[Salary]
//      ,[DOJ]
//      ,[LOS]
//      ,[BankingWith]
//      ,[AccountNumber]
//      ,[CarType]
//      ,[CarBrand]
//      ,[CarModel]
//      ,[CarAgency]
//      ,[PropertyType]
//      ,[PropertyDesc]
//      ,[MortgageDegree]
//      ,[MasterDevelopmentName]
//      ,[MasterDeveloperName]
//      ,[ProjectName]
//      ,[DeveloperName]
//      ,[BuildingName]
//      ,[OriginalPropertyValue]
//      ,[AppraisalValue]
//      ,[AppraisalBy]
//      ,[AppraisalDate]
//      ,[PropertyStatus]
//      ,[DateOfCompletion]
//      ,[LotNo]
//      ,[PlotNo]
//      ,[TitleDeedNo]
//      ,[LandArea]
//      ,[BuiltupArea]
//      ,[PropertyValuePerUnit]
//      ,[RegistrationType]
//      ,[RegistrationNumber]
//      ,[RegisteredOnNameOf]
//      ,[Relation]
//      ,[RegistrationDate]
//      ,[IssuedFrom]
//      ,[VillaAptNo]
//      ,[BuildingCompoundName]
//      ,[StreetName]
//      ,[Country]
//      ,[Emirate]
//      ,[AreaName]
//      ,[OriginalPrice]
//      ,[Downpayment]
//      ,[NFA]
//      ,[ProfitRate]
//      ,[Tenor]
//      ,[DSR]
//      ,[SalesName]
//      ,[Remarks]
//      ,[CreatedBy]
//      ,[CreatedOn]
//      ,[LastUpdatedBy]
//      ,[LastUpdatedOn]
