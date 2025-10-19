using core_23webc_gr6.Middlewares;
using core_23webc_gr6.Models;
using Microsoft.Extensions.Options;
using core_23webc_gr6.Helper;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// CHNhu - Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
// endCHNhu

//VqNam đăng ký DatabaseHelper để sử dụng kết nối database 7/10/2025 dòng 28-->30
builder.Services.AddSingleton<DatabaseHelper>();

//end VqNam 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// CHNhu
// Middleware kiem tra url hop le
app.Use(async (context, next) =>
{
	await next();
	if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
	{
		context.Response.Redirect("/Home/Error");
	}
});
// endCHNhu

// CHNhu - 20/10/2025 - Sử dụng Session
app.UseSession();
// CHNhu - 20/10/2025 - Sử dụng Middleware xác thực Admin
app.UseMiddleware<AdminAuthMiddleware>();

app.UseRouting();

app.UseAuthorization();

// CHNhu
// Định nghĩa route khi có area, đặt route cụ thể lên trên route mặc định
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
// endCHNhu

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}"
);






//NTNguyen - Load products from JSON
app.UseProductMiddleware();
//endNTNguyen











// Ngoc Son



// Quoc Nam
app.UseRequestLogging();

app.Run();



