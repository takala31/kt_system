using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace kt_system.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]public string UserName { get; set; } = "";
        [BindProperty]public string Password { get; set; } = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        //public IActionResult OnPost()
        //{
        //    string sql = $@"
        //        SELECT *
        //        FROM AS_Users
        //        WHERE UserName = '{UserName}'
        //        AND PasswordHash = '{Password}'
        //        AND UserStatus = 'Active'
        //    ";

        //    DataTable dt = DaoHelper.GetDataTable(sql);

        //    if (dt.Rows.Count == 0)
        //    {
        //        TempData["alertColor"] = "danger";
        //        TempData["alertMessage"] ="Invalid Username or Password.";

        //        return Page();
        //    }

        //    DataRow r = dt.Rows[0];

        //    HttpContext.Session.SetString("UserID",r["UserID"].ToString());
        //    HttpContext.Session.SetString("UserName",r["UserName"].ToString());
        //    HttpContext.Session.SetString("FullName",r["FullName"].ToString());
        //    HttpContext.Session.SetString("UserRole",r["UserRole"].ToString());
        //    TempData["alertColor"] = "success";
        //    TempData["alertMessage"] ="Welcome back " +r["FullName"].ToString();
        //    return RedirectToPage("/Applications/Index");
        //}

        public IActionResult OnPost()
        {
            string status = DaoHelper.DLookup("UserStatus","As_Users","UserName='" + UserName + "'");

            if (string.IsNullOrEmpty(status))
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] = "Invalid Username";
                return Page();
            }

            if (status != "Active")
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] = "User not active : " + status;
                return Page();
            }

            string loginUser = DaoHelper.DLookup("UserName","As_Users","UserName='" + UserName + "' AND PasswordHash='" + Password +"'");

            if (string.IsNullOrEmpty(loginUser))
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] = "Wrong Password";
                return Page();
            }



            // Login success
            string sql = $@"SELECT * FROM AS_Users  WHERE UserName = '{UserName}' AND PasswordHash = '{Password}' AND UserStatus = 'Active' ";

            DataTable dt = DaoHelper.GetDataTable(sql);

            if (dt.Rows.Count == 0)
            {
                TempData["alertColor"] = "danger";
                TempData["alertMessage"] = "Invalid Username or Password.";

                return Page();
            }

            DataRow r = dt.Rows[0];

            HttpContext.Session.SetString("UserID", r["UserID"].ToString());
            HttpContext.Session.SetString("UserName", r["UserName"].ToString());
            HttpContext.Session.SetString("FullName", r["FullName"].ToString());
            HttpContext.Session.SetString("UserRole", r["UserRole"].ToString());
            //TempData["alertColor"] = "success";
            //TempData["alertMessage"] = "Welcome back " + r["FullName"].ToString();
            //return RedirectToPage("/Applications/Index");
            return RedirectToPage("/Index");
        }

        public void OnGet()
        {

        }

        //public IActionResult OnPost()
        //{
        //    if (UserName == "khalid" && Password == "123")
        //    {
        //        return RedirectToPage("/Applications/Index");
        //    }

        //    return Page();
        //}
    }
}
