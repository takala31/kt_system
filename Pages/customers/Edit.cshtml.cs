using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace kt_system.Pages.Customers
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int CustomerID { get; set; }
        [BindProperty] public string? FirstName { get; set; }
        [BindProperty] public string? MiddleName { get; set; }
        [BindProperty] public string? LastName { get; set; }
        [BindProperty] public string? AraName { get; set; }
        [BindProperty] public DateTime? DOB { get; set; }
        [BindProperty] public string? Gender { get; set; }
        [BindProperty] public string? MaritalStatus { get; set; }
        [BindProperty] public string? Nationality { get; set; }
        [BindProperty] public string? PreferredLanguage { get; set; }
        [BindProperty] public string? EmiratesID { get; set; }
        [BindProperty] public DateTime? EmiratesIDExpiry { get; set; }
        [BindProperty] public string? PassportNumber { get; set; }
        [BindProperty] public DateTime? PassportExpiry { get; set; }
        [BindProperty] public string? Mobile { get; set; }
        [BindProperty] public string? AlternateMobile { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Country { get; set; }
        [BindProperty] public string? Emirate { get; set; }
        [BindProperty] public string? City { get; set; }
        [BindProperty] public string? Area { get; set; }

        [BindProperty] public string? AddressLine1 { get; set; }
        [BindProperty] public string? AddressLine2 { get; set; }
        [BindProperty] public string? POBox { get; set; }
        [BindProperty] public string? CustomerStatus { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }


            string sql = $@"
                        SELECT *
                        FROM Customers
                        WHERE CustomerID = {id}";

            var rows = DaoHelper.DList(sql);
            if (rows.Count == 0)
            {
                return RedirectToPage("./Index");
            }

            var row = rows[0];
            CustomerID = Convert.ToInt32(row["CustomerID"]);
            FirstName = row["FirstName"]?.ToString();
            MiddleName = row["MiddleName"]?.ToString();
            LastName = row["LastName"]?.ToString();
            AraName = row["AraName"]?.ToString();

            Nationality = row["Nationality"]?.ToString();

            Mobile = row["Mobile"]?.ToString();
            AlternateMobile = row["AlternateMobile"]?.ToString();

            Email = row["Email"]?.ToString();

            Gender = row["Gender"]?.ToString();
            MaritalStatus = row["MaritalStatus"]?.ToString();

            Country = row["Country"]?.ToString();
            Emirate = row["Emirate"]?.ToString();
            City = row["City"]?.ToString();
            Area = row["Area"]?.ToString();

            AddressLine1 = row["AddressLine1"]?.ToString();
            AddressLine2 = row["AddressLine2"]?.ToString();
            POBox = row["POBox"]?.ToString();

            EmiratesID = row["EmiratesID"]?.ToString();
            PassportNumber = row["PassportNumber"]?.ToString();

            if (row["DOB"] != DBNull.Value)
                DOB = Convert.ToDateTime(row["DOB"]);

            if (row["EmiratesIDExpiry"] != DBNull.Value)
                EmiratesIDExpiry = Convert.ToDateTime(row["EmiratesIDExpiry"]);

            if (row["PassportExpiry"] != DBNull.Value)
                PassportExpiry = Convert.ToDateTime(row["PassportExpiry"]);


            CustomerStatus = row["CustomerStatus"]?.ToString();

            return Page();}


        public IActionResult OnPost()
        {
            string sql = $@"
                        UPDATE Customers
                        SET
                            FirstName = '{FirstName}',
                            MiddleName = '{MiddleName}',
                            LastName = '{LastName}',
                            AraName = '{AraName}',

                            Nationality = '{Nationality}',

                            Mobile = '{Mobile}',
                            AlternateMobile = '{AlternateMobile}',
                            Email = '{Email}',

                            Gender = '{Gender}',
                            MaritalStatus = '{MaritalStatus}',

                            Country = '{Country}',
                            Emirate = '{Emirate}',
                            City = '{City}',
                            Area = '{Area}',

                            AddressLine1 = '{AddressLine1}',
                            AddressLine2 = '{AddressLine2}',
                            POBox = '{POBox}',
                            
                            CustomerStatus = '{CustomerStatus}'

                        WHERE CustomerID = {CustomerID}";

            DaoHelper.DExecute(sql);

            return RedirectToPage("./Index");
        }
    }
}
