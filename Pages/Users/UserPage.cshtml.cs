using kt_system.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace kt_system.Pages.Users
{
    public class UserPageModel : PageModel
    {
        string cs;

        public UserPageModel(IConfiguration config)
        {
            cs = config.GetConnectionString("DefaultConnection");
        }

        // form fields
        [BindProperty] public int UserID { get; set; }
        [BindProperty] public string UserName { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string FullName { get; set; }
        [BindProperty] public string UserRole { get; set; }
        [BindProperty] public string UserManager { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string UserStatus { get; set; }
        [BindProperty] public string Remarks { get; set; }

        // dropdown lists
        //public List<string> Roles { get; set; } = new List<string> { "Admin", "Manager", "User" };
        //public List<string> Statuses { get; set; } = new List<string> { "Active", "Inactive", "Locked","Resigned","Terminated","OnHold" };
        public List<string> Managers { get; set; } = new List<string>();
        public List<Dictionary<string, object>> drpPropertyStatus { get; set; } = new();
        public string Message { get; set; }


        public List<string> drpUserRoles { get; set; } = new();
        public List<string> drpUserStatus { get; set; } = new();

        // id = null  -> new user,  id = 5 -> edit user 5
        public void OnGet(int? id)
        {
            LoadManagers();
            LoadDropDowns();

            if (id == null) return;

            UserID = id.Value;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM AS_Users WHERE UserID = @id", con);
                cmd.Parameters.AddWithValue("@id", UserID);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    UserName = r["UserName"].ToString();
                    FullName = r["FullName"].ToString();
                    UserRole = r["UserRole"].ToString();
                    UserManager = r["UserManager"].ToString();
                    Email = r["Email"].ToString();
                    UserStatus = r["UserStatus"].ToString();
                    Remarks = r["Remarks"].ToString();
                }
            }
        }

        public IActionResult OnPost()
        {
            LoadManagers();

            // simple checks
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(UserRole))
            {
                Message = "User name, full name and role are required.";
                return Page();
            }

            if (UserID == 0 && string.IsNullOrWhiteSpace(Password))
            {
                Message = "Password is required for a new user.";
                return Page();
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // check duplicate user name
                SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM AS_Users WHERE UserName = @u AND UserID <> @id", con);
                chk.Parameters.AddWithValue("@u", UserName);
                chk.Parameters.AddWithValue("@id", UserID);
                if ((int)chk.ExecuteScalar() > 0)
                {
                    Message = "This user name already exists.";
                    return Page();
                }

                string me = User.Identity.Name ?? "system";
                SqlCommand cmd;

                if (UserID == 0)   // new user
                {
                    cmd = new SqlCommand(@"INSERT INTO AS_Users
                        (UserName, PasswordHash, FullName, UserManager, UserRole, Email, UserStatus, Remarks, CreatedBy)
                        VALUES
                        (@UserName, @Hash, @FullName, @Manager, @Role, @Email, @Status, @Remarks, @By)", con);

                        cmd.Parameters.AddWithValue("@Hash", HashPassword(Password));
                }
                else               // edit user
                {
                    cmd = new SqlCommand(@"UPDATE AS_Users SET
                        UserName = @UserName, PasswordHash = @PasswordHash, FullName = @FullName, UserManager = @Manager,
                        UserRole = @Role, Email = @Email, UserStatus = @Status, Remarks = @Remarks,
                        LastUpdatedBy = @By, LastUpdatedOn = GETDATE()
                        WHERE UserID = @id", con);

                        cmd.Parameters.AddWithValue("@id", UserID);
                }

                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@PasswordHash", Password);
                cmd.Parameters.AddWithValue("@FullName", FullName);
                cmd.Parameters.AddWithValue("@Manager", UserManager ?? "");
                cmd.Parameters.AddWithValue("@Role", UserRole);
                cmd.Parameters.AddWithValue("@Email", Email ?? "");
                cmd.Parameters.AddWithValue("@Status", UserStatus);
                cmd.Parameters.AddWithValue("@Remarks", Remarks ?? "");
                cmd.Parameters.AddWithValue("@By", me);
                cmd.ExecuteNonQuery();

                //// when editing, change the password only if the user typed a new one
                //if (UserID != 0 && !string.IsNullOrWhiteSpace(Password))
                //{
                //    SqlCommand pw = new SqlCommand("UPDATE AS_Users SET PasswordHash = @Hash WHERE UserID = @id", con);
                //    pw.Parameters.AddWithValue("@Hash", HashPassword(Password));
                //    pw.Parameters.AddWithValue("@id", UserID);
                //    pw.ExecuteNonQuery();
                //}
            }

            return RedirectToPage("UserIndex");
        }






        string HashPassword(string password)
        {
            PasswordHasher<string> hasher = new PasswordHasher<string>();
            return hasher.HashPassword("user", password);
        }
        void LoadManagers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserName FROM AS_Users WHERE UserRole IN ('Supervisor', 'Admin') ORDER BY UserName", con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Managers.Add(r["UserName"].ToString());
                }
            }
        }
        void LoadDropDowns()
        {
            drpUserRoles = DaoHelper.GetCodes("UserRole");
            drpUserStatus = DaoHelper.GetCodes("UserStatus");
        }

    }
}



























//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Data.SqlClient;

//namespace kt_system.Pages.Users;

//public class EditModel : PageModel
//{
//    private readonly string _cs;
//    public EditModel(IConfiguration config)
//        => _cs = config.GetConnectionString("DefaultConnection")!;

//    [FromRoute] public int? Id { get; set; }
//    public bool IsNew => Id is null;

//    [BindProperty] public InputModel Input { get; set; } = new();

//    public List<SelectListItem> Roles { get; set; } = new();
//    public List<SelectListItem> Statuses { get; set; } = new();
//    public List<SelectListItem> Managers { get; set; } = new();

//    public class InputModel
//    {
//        [Required, StringLength(50)]
//        [Display(Name = "User Name")]
//        public string UserName { get; set; } = "";

//        [StringLength(100, MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        public string? Password { get; set; }          // required only on create

//        [Required, StringLength(100)]
//        [Display(Name = "Full Name")]
//        public string FullName { get; set; } = "";

//        [Required, StringLength(20)]
//        public string UserRole { get; set; } = "";

//        [StringLength(50)]
//        [Display(Name = "Manager")]
//        public string? UserManager { get; set; }

//        [EmailAddress, StringLength(100)]
//        public string? Email { get; set; }

//        [Required, StringLength(50)]
//        [Display(Name = "Status")]
//        public string UserStatus { get; set; } = "Active";

//        [StringLength(1000)]
//        public string? Remarks { get; set; }
//    }

//    public async Task<IActionResult> OnGetAsync()
//    {
//        await LoadListsAsync();
//        if (IsNew) return Page();

//        using var con = new SqlConnection(_cs);
//        using var cmd = new SqlCommand(
//            @"SELECT UserName, FullName, UserRole, UserManager, Email, UserStatus, Remarks
//              FROM AS_Users WHERE UserID = @id", con);
//        cmd.Parameters.AddWithValue("@id", Id!.Value);
//        await con.OpenAsync();
//        using var r = await cmd.ExecuteReaderAsync();
//        if (!await r.ReadAsync()) return NotFound();

//        Input = new InputModel
//        {
//            UserName = r.GetString(0),
//            FullName = r.GetString(1),
//            UserRole = r.GetString(2),
//            UserManager = r.IsDBNull(3) ? null : r.GetString(3),
//            Email = r.IsDBNull(4) ? null : r.GetString(4),
//            UserStatus = r.GetString(5),
//            Remarks = r.IsDBNull(6) ? null : r.GetString(6)
//        };
//        return Page();
//    }

//    public async Task<IActionResult> OnPostAsync()
//    {
//        await LoadListsAsync();

//        if (IsNew && string.IsNullOrWhiteSpace(Input.Password))
//            ModelState.AddModelError("Input.Password", "Password is required for a new user.");

//        if (!string.IsNullOrEmpty(Input.UserManager) &&
//            Input.UserManager.Equals(Input.UserName, StringComparison.OrdinalIgnoreCase))
//            ModelState.AddModelError("Input.UserManager", "A user cannot be their own manager.");

//        if (!ModelState.IsValid) return Page();

//        using var con = new SqlConnection(_cs);
//        await con.OpenAsync();

//        // Unique username check (excluding the current record when editing)
//        using (var chk = new SqlCommand(
//            "SELECT COUNT(1) FROM AS_Users WHERE UserName = @u AND UserID <> @id", con))
//        {
//            chk.Parameters.AddWithValue("@u", Input.UserName);
//            chk.Parameters.AddWithValue("@id", Id ?? 0);
//            if ((int)await chk.ExecuteScalarAsync()! > 0)
//            {
//                ModelState.AddModelError("Input.UserName", "This user name already exists.");
//                return Page();
//            }
//        }

//        var me = User.Identity?.Name ?? "system";
//        var hasher = new PasswordHasher<object>();
//        string? hash = string.IsNullOrWhiteSpace(Input.Password)
//            ? null : hasher.HashPassword(new object(), Input.Password);

//        SqlCommand cmd;
//        if (IsNew)
//        {
//            cmd = new SqlCommand(
//                @"INSERT INTO AS_Users
//                    (UserName, PasswordHash, FullName, UserManager, UserRole, Email,
//                     UserStatus, Remarks, CreatedBy, CreatedOn)
//                  VALUES
//                    (@UserName, @Hash, @FullName, @Manager, @Role, @Email,
//                     @Status, @Remarks, @By, GETDATE())", con);
//            cmd.Parameters.AddWithValue("@Hash", hash!);
//        }
//        else
//        {
//            cmd = new SqlCommand(
//                @"UPDATE AS_Users SET
//                    UserName = @UserName, FullName = @FullName, UserManager = @Manager,
//                    UserRole = @Role, Email = @Email, UserStatus = @Status,
//                    Remarks = @Remarks, LastUpdatedBy = @By, LastUpdatedOn = GETDATE(),
//                    PasswordHash = ISNULL(@Hash, PasswordHash)   -- blank = keep old password
//                  WHERE UserID = @id", con);
//            cmd.Parameters.AddWithValue("@Hash", (object?)hash ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@id", Id!.Value);
//        }

//        cmd.Parameters.AddWithValue("@UserName", Input.UserName);
//        cmd.Parameters.AddWithValue("@FullName", Input.FullName);
//        cmd.Parameters.AddWithValue("@Manager", (object?)Input.UserManager ?? DBNull.Value);
//        cmd.Parameters.AddWithValue("@Role", Input.UserRole);
//        cmd.Parameters.AddWithValue("@Email", (object?)Input.Email ?? DBNull.Value);
//        cmd.Parameters.AddWithValue("@Status", Input.UserStatus);
//        cmd.Parameters.AddWithValue("@Remarks", (object?)Input.Remarks ?? DBNull.Value);
//        cmd.Parameters.AddWithValue("@By", me);

//        using (cmd) await cmd.ExecuteNonQueryAsync();

//        TempData["Msg"] = IsNew ? "User created." : "User updated.";
//        return RedirectToPage("Index");
//    }

//    private async Task LoadListsAsync()
//    {
//        // Static for now. If you keep these in GeneralCodes, load them with
//        // DaoHelper.DList like you do elsewhere (e.g. CodeCategory='UserRoles').
//        Roles = new[] { "Admin", "Manager", "User" }
//                       .Select(x => new SelectListItem(x, x)).ToList();
//        Statuses = new[] { "Active", "Inactive", "Locked" }
//                       .Select(x => new SelectListItem(x, x)).ToList();

//        using var con = new SqlConnection(_cs);
//        using var cmd = new SqlCommand(
//            "SELECT UserName FROM AS_Users WHERE UserStatus = 'Active' AND UserID <> @id ORDER BY UserName", con);
//        cmd.Parameters.AddWithValue("@id", Id ?? 0);
//        await con.OpenAsync();
//        using var r = await cmd.ExecuteReaderAsync();
//        while (await r.ReadAsync())
//            Managers.Add(new SelectListItem(r.GetString(0), r.GetString(0)));
//    }
//}