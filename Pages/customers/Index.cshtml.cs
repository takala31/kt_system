using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<Dictionary<string, object>> Customers { get; set; }
            = new();

        public void OnGet()
        {
            string sql = @"
                SELECT
                    CustomerID,
                    FirstName,
                    LastName,
                    Nationality,
                    Mobile,
                    Email,
                    CustomerStatus
                FROM Customers
                ORDER BY CustomerID DESC";

            Customers = DaoHelper.DList(sql);
        }
    }
}

