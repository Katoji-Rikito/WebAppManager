using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using WebAppManager.Models;
using WebAppManager.Repositories;
using WebAppManager.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services and language to the container.
builder.Services
    .AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Thêm ngôn ngữ
builder.Services.AddLocalization(options=>options.ResourcesPath = "Languages");

// Thêm Scoped của GenericRepository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Thêm kết nối đến cơ sở dữ liệu MariaDB
string WebAppManagerDB = builder.Configuration.GetConnectionString("WebAppManagerConnection") ?? string.Empty;
builder.Services.AddDbContext<WebappmanagerContext>(options =>
{
    try { options.UseMySql(WebAppManagerDB, ServerVersion.AutoDetect(WebAppManagerDB)); }
    catch (Exception ex) { throw new Exception(ex.Message); }
});

var app = builder.Build();

// Thêm middleware ngôn ngữ
// Hiện hỗ trợ tiếng Việt (ưu tiên) và tiếng Anh
var supportedCultures = new[] { "vi-VN", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
