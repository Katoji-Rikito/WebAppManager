using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebAppManager.Filters;
using WebAppManager.Models;
using WebAppManager.Repositories;
using WebAppManager.Repositories.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(cfg =>
{
    _ = cfg.Filters.Add(typeof(ExceptionHandler));
});

// Thêm services authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromHours(1);
    option.SlidingExpiration = true;
    option.LoginPath = "/Account/Index";
    option.LogoutPath = "/Account/Logout";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.Cookie.Name = "WebAppManagerCookie";
});

// Thêm Scoped của GenericRepository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Thêm kết nối đến cơ sở dữ liệu MariaDB
string WebAppManagerDB = builder.Configuration.GetConnectionString("WebAppManagerConnection") ?? string.Empty;
builder.Services.AddDbContext<WebappmanagerContext>(options => options.UseMySql(WebAppManagerDB, ServerVersion.AutoDetect(WebAppManagerDB)));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
