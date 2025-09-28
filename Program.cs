using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Middlewares;
using core_23webc_gr6.Models;
using core_23webc_gr6.Repositories;
using core_23webc_gr6.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//LTMKieu
// dang ky cau hinh AppConfig de su dung trong toan bo ung dung
builder.Services.AddOptions<AppConfig>().Bind(builder.Configuration.GetSection("AppConfig")).ValidateDataAnnotations().ValidateOnStart();
// dang ky singleton de co the inject AppConfig vao trong controller
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppConfig>>().Value);

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
//endLTMKieu
// Add services to the container.
builder.Services.AddControllersWithViews();
//Thao Nguyen
// Đăng ký UserService (Scoped)
builder.Services.AddScoped<IUserService, UserService>();
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



// CHNhu
// Middleware kiem tra url hop le
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Home/Error");
    }
});
// endCHNhu

// Thao Nguyen
app.UseUserLoading();

// LTMKieu
//Test config có đọc được không
app.MapGet("/config", (AppConfig config) =>
{
    return Results.Json(config);
});
//endLTMKieu










// Ngoc Son



// Quoc Nam
app.UseRequestLogging(); 

app.Run();



