using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Applications
{
    public class IndexModel : PageModel
    {
        public List<Dictionary<string, object>> Apps { get; set; }
            = new();

        public void OnGet()
        {
            Apps = DaoHelper.DList(@"
                SELECT TOP 25
                    AppID,
                    RIGHT(AppID_usr,11) AS AppID_usr,
                    FirstName,
                    LastName,
                    Nationality,
                    Employer,   
                    AppProduct,
                    PropertyType,

                    OriginalPrice,
                    Downpayment,
                    NFA,
                    ProfitRate,
                    Tenor,

                    Stage,
                    CreatedOn
                FROM AS_Entries
                ORDER BY AppID DESC");
        }
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
