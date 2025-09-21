using core_group_ex_01.Middlewares;
using core_group_ex_01.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Son 
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
app.Use(async (context, next) => {
    await next();
    if(context.Response.StatusCode == 404) {
        context.Response.Redirect("/Home/Error");
    };
});










// Thao Nguyen













// Mong Kieu











// Ngoc Son
// Gọi Middleware load user
app.UseUserLoading();



// Quoc Nam


app.Run();
