using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kt_system.Helpers;
using kt_system.Models;

namespace kt_system.Pages.GeneralCodes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public GeneralCode GC { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return RedirectToPage("./Index");

            var row = DaoHelper.DRow($@"
                SELECT *
                FROM GeneralCodes
                WHERE GeneralCodeID = {id}");

            if (row == null || row.Count == 0)
                return RedirectToPage("./Index");

            GC.GeneralCodeID = Convert.ToInt32(row["GeneralCodeID"]);
            GC.Code = row["Code"]?.ToString();
            GC.CodeCategory = row["CodeCategory"]?.ToString();

            GC.CodeCategoryID =
                row["CodeCategoryID"] == null
                ? null
                : Convert.ToInt16(row["CodeCategoryID"]);

            GC.Description = row["Description"]?.ToString();
            GC.Description_2 = row["Description_2"]?.ToString();
            GC.Description_3 = row["Description_3"]?.ToString();
            GC.Description_4 = row["Description_4"]?.ToString();
            GC.Description_5 = row["Description_5"]?.ToString();

            GC.Remarks = row["Remarks"]?.ToString();

            GC.CreatedBy = row["CreatedBy"]?.ToString();
            GC.UpdatedBy = row["UpdatedBy"]?.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            string sql = $@"

            UPDATE GeneralCodes
            SET

                Code='{GC.Code}',

                Description='{GC.Description}',
                Description_2='{GC.Description_2}',
                Description_3='{GC.Description_3}',
                Description_4='{GC.Description_4}',
                Description_5='{GC.Description_5}',

                Remarks='{GC.Remarks}',

                UpdatedBy='Admin',
                UpdatedOn=GETDATE()

            WHERE GeneralCodeID={GC.GeneralCodeID}

            ";

            DaoHelper.DExecute(sql);

            return RedirectToPage("./Index",
                new { SelectedCategory = GC.CodeCategory });
        }



        public IActionResult OnPostDelete()
        {
            if (GC.GeneralCodeID <= 0)
            {
                return Page();
            }


            string category = GC.CodeCategory ?? "";
            DaoHelper.DExecute($@"DELETE FROM GeneralCodes WHERE GeneralCodeID = {GC.GeneralCodeID}");
            return RedirectToPage("./Index",new { SelectedCategory = category });
        }
    }
}