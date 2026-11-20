using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<Dictionary<string, object>> Customers { get; set; }= new();

        public void OnGet()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) {Response.Redirect("/Account/Login"); return;}
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) { Response.Redirect("/Index?login=1"); return; }

            string sql = @"
                SELECT
                    CustomerID,
                    FirstName,
                    LastName,
                    MiddleName,
                    AraName,
                    DOB,
                    Nationality,
                    Mobile,
                    Email,
                    EmiratesID,
                    EmiratesIDExpiry,
                    PassportNumber,
                    CustomerStatus
                    FROM Customers
                    ORDER BY CustomerID DESC";

            Customers = DaoHelper.DList(sql);
        }
    }
}

