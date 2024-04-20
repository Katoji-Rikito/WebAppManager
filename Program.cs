using Microsoft.EntityFrameworkCore;
using WebAppManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm kết nối đến cơ sở dữ liệu MariaDB
string WebAppManagerDB = builder.Configuration.GetConnectionString("WebAppManagerConnection") ?? throw new InvalidOperationException("Không tìm thấy chuỗi kết nối tới cơ sở dữ liệu!");
builder.Services.AddDbContext<WebappmanagerContext>(options =>
{
    try { options.UseMySql(WebAppManagerDB, ServerVersion.AutoDetect(WebAppManagerDB)); }
    catch (Exception ex) { throw new Exception(ex.Message); }
});

var app = builder.Build();

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
