using kt_system.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace kt_system.Pages.Customers
{
    public class CustomerIndexModel : PageModel
    {
        public List<Dictionary<string, object>> Customers { get; set; }= new();
        public int AllCount { get; set; }
        public int activeCount { get; set; }
        public int inactiveCount { get; set; }
        public int advocateCount { get; set; }
        public int qualifiedCount { get; set; }
        public int repeat_customerCount { get; set; }
        public int prospectCount { get; set; }
        public List<Dictionary<string, object>> Apps { get; set; } = new();

        public string? SearchText { get; set; }
        public string? statusPassed2 { get; set; }

        private void LoadPageData()
        {
            AllCount = DaoHelper.DCount("Customers", "1=1");
            activeCount = DaoHelper.DCount("Customers", "CustomerStatus='active'");
            inactiveCount = DaoHelper.DCount("Customers", "CustomerStatus='inactive'");
            advocateCount = DaoHelper.DCount("Customers", "CustomerStatus='advocate'");
            qualifiedCount = DaoHelper.DCount("Customers", "CustomerStatus='qualified'");
            repeat_customerCount = DaoHelper.DCount("Customers", "CustomerStatus='repeat_customer'");
            prospectCount = DaoHelper.DCount("Customers", "CustomerStatus='prospect'");
        }

        //public void OnGet()
        public void OnGet(string? statusPassed, string? searchText)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                HttpContext.Session.SetString("RequestingPage", "/customers/CustomerIndex");
                Response.Redirect("/Index?login=1"); return;
            }

            LoadPageData();
            statusPassed2 = statusPassed;

            if (string.IsNullOrEmpty(statusPassed) && string.IsNullOrEmpty(searchText))
            {
                Apps = new();
                return;
            }


            string sql = "SELECT * FROM Customers WHERE 1=1";
            if (!string.IsNullOrEmpty(statusPassed) && statusPassed != "All")
            {
                sql += $" AND CustomerStatus='{statusPassed}'";
            }
                            //CustomerID = '{searchText}'

            if (!string.IsNullOrEmpty(searchText))
            {
                sql += $@"AND (
                            FirstName LIKE '%{searchText}%'
                            OR MiddleName LIKE '%{searchText}%'
                            OR LastName LIKE '%{searchText}%'
                            OR CONCAT(FirstName,' ',MiddleName,' ',LastName) LIKE '%{searchText}%'
                            OR AraName LIKE '%{searchText}%'
                            OR EmiratesID LIKE '%{searchText}%'
                            OR PassportNumber LIKE '%{searchText}%'
                            OR Nationality LIKE '%{searchText}%'
                            OR Remarks LIKE '%{searchText}%'
            )";
            }

            sql += " ORDER BY CustomerID DESC";
            Customers = DaoHelper.DList(sql);
        }
    }
}

/*
            //else {sql = @"SELECT CustomerID,FirstName,LastName,MiddleName,AraName,DOB,Nationality,Mobile,Email,EmiratesID,EmiratesIDExpiry,PassportNumber,CustomerStatus FROM Customers";}

            LoadPageData();

            //Search parameters
            Stage = stagePassed;
            SearchText = searchText;
            string sql = "SELECT * FROM AS_Entries WHERE 1=1";

            if (string.IsNullOrEmpty(stagePassed) && string.IsNullOrEmpty(searchText))
            {
                Apps = new();
                return;
            }

            if (!string.IsNullOrEmpty(stagePassed) && stagePassed != "All")
            {
                sql += $" AND Stage='{stagePassed}'";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                sql += $@"
                        AND (
                            FirstName LIKE '%{searchText}%'
                            OR MiddleName LIKE '%{searchText}%'
                            OR LastName LIKE '%{searchText}%'
                            OR CONCAT(FirstName,' ',MiddleName,' ',LastName) LIKE '%{searchText}%'
                            OR AraName LIKE '%{searchText}%'
                            OR AppID_usr LIKE '%{searchText}%'
                            OR Employer LIKE '%{searchText}%'
                            OR Nationality LIKE '%{searchText}%'
                            OR Stage LIKE '%{searchText}%'
            )";
            }

            sql += " ORDER BY AppID DESC";

            Apps = DaoHelper.DList(sql);
 */