using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kt_system.Pages.Customers
{
    public class CreateModel : PageModel
    {
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
        
        
        //====================================================================================================================================
        //                                                  GET handler for form opening
        //====================================================================================================================================
        public void OnGet()
        {
        
        
        }





        //====================================================================================================================================
        //                                                  POST handler for form submission
        //====================================================================================================================================

        public IActionResult OnPost()
        {
            string sql = $@"
            INSERT INTO Customers
            (
                FirstName,
                MiddleName,
                LastName,
                AraName,

                DOB,
                Gender,
                MaritalStatus,
                Nationality,
                PreferredLanguage,

                EmiratesID,
                EmiratesIDExpiry,

                PassportNumber,
                PassportExpiry,

                Mobile,
                AlternateMobile,
                Email,

                Country,
                Emirate,
                City,
                Area,

                AddressLine1,
                AddressLine2,
                POBox,

                CreatedOn,
                IsActive
            )
            VALUES
            (
                '{FirstName}',
                '{MiddleName}',
                '{LastName}',
                '{AraName}',

                {(DOB.HasValue ? $"'{DOB.Value:yyyy-MM-dd}'" : "NULL")},
                '{Gender}',
                '{MaritalStatus}',
                '{Nationality}',
                '{PreferredLanguage}',

                '{EmiratesID}',
                {(EmiratesIDExpiry.HasValue ? $"'{EmiratesIDExpiry.Value:yyyy-MM-dd}'" : "NULL")},

                '{PassportNumber}',
                {(PassportExpiry.HasValue ? $"'{PassportExpiry.Value:yyyy-MM-dd}'" : "NULL")},

                '{Mobile}',
                '{AlternateMobile}',
                '{Email}',

                '{Country}',
                '{Emirate}',
                '{City}',
                '{Area}',

                '{AddressLine1}',
                '{AddressLine2}',
                '{POBox}',

                GETDATE(),
                1
            )";

            Console.WriteLine(sql);

            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }

    }
}
