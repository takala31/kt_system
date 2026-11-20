//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace kt_system.Pages.Account
//{
//    public class LoginModel : PageModel
//    {
//        public void OnGet()
//        {
//        }
//    }
//}



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kt_system.Helpers;
using System.Data;

namespace kt_system.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public void OnGet()
        {
            //if (string.IsNullOrEmpty(
            //    HttpContext.Session.GetString("UserName")))
            //{
            //    Response.Redirect("/Account/Login");
            //    return;
            //}

        }

        //public IActionResult OnGet()
        //{
        //    HttpContext.Session.Clear();

        //    return RedirectToPage("/Account/Login");
        //}

        public IActionResult OnPost()
        {
            string sql = $@"
                SELECT *
                FROM AS_Users
                WHERE UserName = '{UserName}'
                AND PasswordHash = '{Password}'
                AND UserStatus = 'Active'
            ";

            DataTable dt = DaoHelper.GetDataTable(sql);

            if (dt.Rows.Count == 0)
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] =
                    "Invalid Username or Password.";

                return Page();
            }

            DataRow r = dt.Rows[0];

            HttpContext.Session.SetString(
                "UserID",
                r["UserID"].ToString());

            HttpContext.Session.SetString(
                "UserName",
                r["UserName"].ToString());

            HttpContext.Session.SetString(
                "FullName",
                r["FullName"].ToString());

            HttpContext.Session.SetString(
                "UserRole",
                r["UserRole"].ToString());

            TempData["alertColor"] = "success";
            TempData["alertMessage"] =
                "Welcome back " +
                r["FullName"].ToString();

            return RedirectToPage("/Applications/Index");
        }
    }
}