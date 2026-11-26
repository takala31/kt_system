using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace kt_system.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        [BindProperty] public int CustomerID { get; set; }
        [BindProperty] public string? FirstName { get; set; }
        [BindProperty] public string? LastName { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return RedirectToPage("/Customers/CustomerIndex");

            string sql = $@"SELECT * FROM Customers WHERE CustomerID = {id}";
            var rows = DaoHelper.DList(sql);
            if (rows.Count == 0) return RedirectToPage("/Customers/CustomerIndex");

            var row = rows[0];
            CustomerID = Convert.ToInt32(row["CustomerID"]);
            FirstName = row["FirstName"]?.ToString();
            LastName = row["LastName"]?.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            string sql = $@"DELETE FROM Customers WHERE CustomerID = {CustomerID}";
            //string sql = $@"UPDATE Customers SET IsActive = 0, CustomerStatus='inactive' WHERE CustomerID = {CustomerID}";
            DaoHelper.DExecute(sql);

            return RedirectToPage("/Customers/CustomerIndex");
        }
    }
}