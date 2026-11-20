using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data;

namespace kt_system.Pages.Customers
{
    public class CreateModel : PageModel
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
        [BindProperty] public string? CustomerStatus { get; set; }
        

        [BindProperty] public string? AddressLine1 { get; set; }
        [BindProperty] public string? AddressLine2 { get; set; }
        [BindProperty] public string? POBox { get; set; }


        public List<Dictionary<string, object>> drpCustomerStatus { get; set; } = new();
        public List<Dictionary<string, object>> drpNationality { get; set; } = new();        
        [BindProperty] public string Mode { get; set; }   // "New" or "Edit"


        private void LoadPageData()
        {
            drpCustomerStatus = DaoHelper.DList(@"SELECT Description As CustomerStatus FROM GeneralCodes where CodeCategory='CustomerStatus'");
            drpNationality = DaoHelper.DList(@"SELECT Description As Nationality FROM GeneralCodes where CodeCategory='Nationality'");
        }



        //====================================================================================================================================
        //                                                  GET handler for form opening
        //====================================================================================================================================
        //public void OnGet() { }

        public void OnGet(int? id, string? DummyEntry)
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) {Response.Redirect("/Account/Login"); return;}
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) { Response.Redirect("/Index?login=1"); return; }

            LoadPageData();
            CustomerID = Convert.ToInt32(DaoHelper.DMax("CustomerID", "Customers", "1=1"))+1;
            Mode = HttpContext.Session.GetString("SaveStatus");

            if (id == null)
            {
                Mode = "New";
                if (DummyEntry == "yes")
                {
                    FirstName = "Jameel";
                    MiddleName = "Ali";
                    LastName = "Khan";
                    AraName = "جمیل علی خان";
                    Mobile = "1234567890";
                    Email = "jameel.khan@example.com";
                    Country = "United Arab Emirates";
                    Emirate = "Dubai";
                    City = "Dubai";
                    Area = "Downtown Dubai";
                    DOB = new DateTime(1990, 1, 1);
                    Gender = "Male";
                    MaritalStatus = "Single";
                    Nationality = "Syrian";
                    PreferredLanguage = "English";
                    EmiratesID = "784-1987-1234567-1";
                    EmiratesIDExpiry = new DateTime(2030, 1, 1);
                    PassportNumber = "A1234567";
                    PassportExpiry = new DateTime(2030, 1, 1);
                    AlternateMobile = "0987654321";
                    CustomerStatus = "Active";
                }
                return;
            }


            //if (id == null)
            //{
            //    //HttpContext.Session.SetString("SaveStatus", "Save New");
            //}
            //else
            //{
            //    //HttpContext.Session.SetString("SaveStatus", "Save Changes");

            //}

            //shorter:
            //HttpContext.Session.SetString("SaveStatus", id == null ? "Save New" : "Save Changes");



            Mode = "Not New";
            string sql = $"SELECT * FROM Customers WHERE CustomerID = {id}";

            DataTable dt = DaoHelper.GetDataTable(sql);

            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            CustomerID = Convert.ToInt32(row["CustomerID"]);

            FirstName = row["FirstName"].ToString();
            MiddleName = row["MiddleName"].ToString();
            LastName = row["LastName"].ToString();
            AraName = row["AraName"].ToString();

            Mobile = row["Mobile"].ToString();
            Email = row["Email"].ToString();

            Country = row["Country"].ToString();
            Emirate = row["Emirate"].ToString();
            City = row["City"].ToString();
            Area = row["Area"].ToString();

            DOB = row["DOB"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DOB"]);
            Gender = row["Gender"].ToString();
            MaritalStatus = row["MaritalStatus"].ToString();
            Nationality = row["Nationality"].ToString();
            PreferredLanguage = row["PreferredLanguage"].ToString();
            EmiratesID = row["EmiratesID"].ToString();
            EmiratesIDExpiry = row["EmiratesIDExpiry"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["EmiratesIDExpiry"]);
            PassportNumber = row["PassportNumber"].ToString();
            PassportExpiry= row["PassportExpiry"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["PassportExpiry"]);
            AlternateMobile = row["AlternateMobile"].ToString();
            CustomerStatus = row["CustomerStatus"].ToString();
        }



        //====================================================================================================================================
        //                                                  POST handler for form submission
        //====================================================================================================================================

        public IActionResult OnPost()
        {
            string sql;
            if (Mode == "New")
            {
                sql = $@"
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
                        IsActive,
                        CustomerStatus

                    )
                    VALUES
                    (
                        '{FirstName}',
                        '{MiddleName}',
                        '{LastName}',
                        '{AraName}',
                        {(DOB.HasValue ? $"'{Convert.ToDateTime(DOB)}'" : "NULL")},
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
                        1,
                        '{CustomerStatus}'
                    )";
            }
            else                                //if (Mode != "New") //Not new record. Save changes to an existing record
            {
                sql = $@"
                        UPDATE Customers
                        SET
                            FirstName = '{FirstName}',
                            MiddleName = '{MiddleName}',
                            LastName = '{LastName}',
                            AraName = '{AraName}',
                            DOB= '{DOB}',
                            Gender = '{Gender}',
                            MaritalStatus = '{MaritalStatus}',

                            Nationality = '{Nationality}',
                            PreferredLanguage = '{PreferredLanguage}',

                            EmiratesID='{EmiratesID}',
                            EmiratesIDExpiry='{EmiratesIDExpiry}',

                            PassportNumber='{PassportNumber}',
                            PassportExpiry='{PassportExpiry}',

                            Mobile = '{Mobile}',
                            AlternateMobile = '{AlternateMobile}',
                            Email = '{Email}',


                            Country = '{Country}',
                            Emirate = '{Emirate}',
                            City = '{City}',
                            Area = '{Area}',

                            AddressLine1 = '{AddressLine1}',
                            AddressLine2 = '{AddressLine2}',
                            POBox = '{POBox}',
                            
                            IsActive=1,
                            CustomerStatus = '{CustomerStatus}'

                        WHERE CustomerID = {CustomerID}";

            }

            DaoHelper.DExecute(sql);

            return RedirectToPage("Index");
        }




        //public IActionResult OnPost_old()
        //{
        //    string sql = $@"
        //    INSERT INTO Customers
        //    (
        //        FirstName,
        //        MiddleName,
        //        LastName,
        //        AraName,

        //        DOB,
        //        Gender,
        //        MaritalStatus,
        //        Nationality,
        //        PreferredLanguage,

        //        EmiratesID,
        //        EmiratesIDExpiry,

        //        PassportNumber,
        //        PassportExpiry,

        //        Mobile,
        //        AlternateMobile,
        //        Email,

        //        Country,
        //        Emirate,
        //        City,
        //        Area,

        //        AddressLine1,
        //        AddressLine2,
        //        POBox,

        //        CreatedOn,
        //        IsActive
        //    )
        //    VALUES
        //    (
        //        '{FirstName}',
        //        '{MiddleName}',
        //        '{LastName}',
        //        '{AraName}',

        //        {(DOB.HasValue ? $"'{DOB.Value:yyyy-MM-dd}'" : "NULL")},
        //        '{Gender}',
        //        '{MaritalStatus}',
        //        '{Nationality}',
        //        '{PreferredLanguage}',

        //        '{EmiratesID}',
        //        {(EmiratesIDExpiry.HasValue ? $"'{EmiratesIDExpiry.Value:yyyy-MM-dd}'" : "NULL")},

        //        '{PassportNumber}',
        //        {(PassportExpiry.HasValue ? $"'{PassportExpiry.Value:yyyy-MM-dd}'" : "NULL")},

        //        '{Mobile}',
        //        '{AlternateMobile}',
        //        '{Email}',

        //        '{Country}',
        //        '{Emirate}',
        //        '{City}',
        //        '{Area}',

        //        '{AddressLine1}',
        //        '{AddressLine2}',
        //        '{POBox}',

        //        GETDATE(),
        //        1
        //    )";

        //    Console.WriteLine(sql);

        //    DaoHelper.DExecute(sql);

        //    return RedirectToPage("Index");
        //}

    }
}
