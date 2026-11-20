using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Redirect("/Index?login=1");

            //RedirectToPage("/Welcome", new { login = 1 })
            //RedirectToPage("/Account/Login");
        }
    }
}


