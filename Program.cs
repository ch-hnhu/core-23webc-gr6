using core_group_ex_01.Middlewares;
using core_group_ex_01.Models;
using core_group_ex_01.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Kieu
// dang ky cau hinh AppConfig de su dung trong toan bo ung dung
builder.Services.AddOptions<AppConfig>().Bind(builder.Configuration.GetSection("AppConfig")).ValidateDataAnnotations().ValidateOnStart();
// dang ky singleton de co the inject AppConfig vao trong controller
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppConfig>>().Value);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



// Hue Nhu
// Middleware kiem tra url hop le
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Home/Error");
    }
    ;
});

// Thao Nguyen
app.UseUserLoading();

// Mong Kieu
//Test config có đọc được không
app.MapGet("/config", (AppConfig config) =>
{
    return Results.Json(config);
});










// Ngoc Son



// Quoc Nam


app.Run();



