using Microsoft.EntityFrameworkCore;
using WebAppManager.Models;
using WebAppManager.Repositories;
using WebAppManager.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm Scoped của GenericRepository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Thêm kết nối đến cơ sở dữ liệu MariaDB
string WebAppManagerDB = builder.Configuration.GetConnectionString("WebAppManagerConnection") ?? string.Empty;
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
