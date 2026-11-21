using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Users
{
    public class UserIndexModel : PageModel
    {
        public int AllCount { get; set; }
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public int TerminatedCount { get; set; }
        public int OnHoldCount { get; set; }
        public int InActiveCount { get; set; }
        public List<Dictionary<string, object>> users { get; set; } = new();

        public string? SearchText { get; set; }
        public string? Status { get; set; }

        private void LoadPageData()
        {
            AllCount = DaoHelper.DCount("AS_Users", "1=1");
            ActiveCount = DaoHelper.DCount("AS_Users", "UserStatus='Active'");
            ResignedCount = DaoHelper.DCount("AS_Users", "UserStatus='Resigned'");
            TerminatedCount = DaoHelper.DCount("AS_Users", "UserStatus='Terminated'");
            OnHoldCount = DaoHelper.DCount("AS_Users", "UserStatus='OnHold'");
            InActiveCount = DaoHelper.DCount("AS_Users", "UserStatus='InActive'");
        }

        //OnGet start
        public void OnGet(string? statusPassed, string? searchText)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) { Response.Redirect("/Index?login=1"); return; }
            LoadPageData();

            //Search parameters
            //Status= statusPassed;
            SearchText = searchText;
            string sql = "SELECT * FROM As_Users WHERE 1=1";

            if (string.IsNullOrEmpty(statusPassed) && string.IsNullOrEmpty(searchText))
            {
                //sql += " ORDER BY UserID DESC";
                //users = DaoHelper.DList(sql);

                ////users = new();
                //return;
            }

            if (!string.IsNullOrEmpty(statusPassed) && statusPassed != "All")
            {
                sql += $" AND UserStatus='{statusPassed}'";
                users = DaoHelper.DList(sql);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                sql += $@"
                        AND (
                            FullName LIKE '%{searchText}%'
                            OR UserRole LIKE '%{searchText}%'
                            OR UserStatus LIKE '%{searchText}%'
                            OR UserManager LIKE '%{searchText}%'
                            OR UserID LIKE '%{searchText}%'
            )";
            }

            sql += " ORDER BY UserID DESC";

            users = DaoHelper.DList(sql);
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
