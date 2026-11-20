using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kt_system.Helpers;
using kt_system.Models;

namespace kt_system.Pages.GeneralCodes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public GeneralCode GC { get; set; } = new();

        public void OnGet(string? category = null)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) { Response.Redirect("/Index?login=1"); return; }

            GC.CodeCategory = category;

            if (!string.IsNullOrEmpty(category))
            {
                GC.CodeCategoryID = Convert.ToInt16(
                    DaoHelper.DLookup(
                        "MAX(CodeCategoryID)",
                        "GeneralCodes",
                        $"CodeCategory='{category}'")
                    ?? "0");
            }
        }

        public IActionResult OnPost()
        {
            // Duplicate check

            bool exists = DaoHelper.DExists(
                "GeneralCodes",
                $"CodeCategory='{GC.CodeCategory}' AND Code='{GC.Code}'");

            if (exists)
            {
                ModelState.AddModelError("", "Code already exists.");
                return Page();
            }

            string sql = $@"

            INSERT INTO GeneralCodes
            (
                Code,
                CodeCategory,
                CodeCategoryID,
                Description,
                Description_2,
                Description_3,
                Description_4,
                Description_5,
                Remarks,
                CreatedBy,
                CreatedOn,
                CodeNum
            )

            VALUES
            (
                '{GC.Code}',
                '{GC.CodeCategory}',
                {GC.CodeCategoryID},

                '{GC.Description}',
                '{GC.Description_2}',
                '{GC.Description_3}',
                '{GC.Description_4}',
                '{GC.Description_5}',

                '{GC.Remarks}',

                'Admin',
                GETDATE(),

                NULL
            )

            ";

            DaoHelper.DExecute(sql);

            return RedirectToPage(
                "./Index",
                new { SelectedCategory = GC.CodeCategory });
        }
    }
}