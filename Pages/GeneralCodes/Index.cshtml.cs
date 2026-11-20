using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kt_system.Helpers;
using kt_system.Models;

namespace kt_system.Pages.GeneralCodes
{
    public class IndexModel : PageModel
    {
        public List<SelectListItem> Categories { get; set; } = new();

        public List<GeneralCode> Codes { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SelectedCategory { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) {Response.Redirect("/Index?login=1"); return; }

            LoadCategories();

            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                LoadCodes();
            }
        }

        private void LoadCategories()
        {
            string sql = @"
                SELECT DISTINCT CodeCategory
                FROM GeneralCodes
                WHERE CodeCategory IS NOT NULL
                ORDER BY CodeCategory";

            var rows = DaoHelper.DList(sql);

            Categories = rows.Select(r => new SelectListItem
            {
                Value = r["CodeCategory"]?.ToString(),
                Text = r["CodeCategory"]?.ToString()
            }).ToList();
        }

        private void LoadCodes()
        {
            string sql = $@"
                SELECT *
                FROM GeneralCodes
                WHERE CodeCategory = '{SelectedCategory}'
                ORDER BY Description";

            var rows = DaoHelper.DList(sql);

            foreach (var row in rows)
            {
                Codes.Add(new GeneralCode
                {
                    GeneralCodeID = Convert.ToInt32(row["GeneralCodeID"]),
                    Code = row["Code"]?.ToString(),
                    Description = row["Description"]?.ToString(),
                    Remarks = row["Remarks"]?.ToString(),

                    CodeNum =
                        row["CodeNum"] == null
                            ? null
                            : Convert.ToInt32(row["CodeNum"])
                });
            }
        }
    }
}