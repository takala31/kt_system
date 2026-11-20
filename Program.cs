using kt_system.Helpers;

var builder = WebApplication.CreateBuilder(args);

// 1. Register ALL services before Build()
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(30));

var app = builder.Build();


// 1. Register ALL services before Build()
app.UseSession();


// 2. Configure the middleware pipeline after Build()
DaoHelper.Init(builder.Configuration);

app.UseStaticFiles();
app.UseRouting();
app.UseSession();          // once, after UseRouting, before MapRazorPages
app.MapRazorPages();

app.Run();










//using kt_system.Helpers;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRazorPages();
//builder.Services.AddSession();
//var app = builder.Build();
//app.UseSession();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseSession();
//app.MapRazorPages();
//DaoHelper.Init(builder.Configuration);

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(30));

//app.Run();















//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();
