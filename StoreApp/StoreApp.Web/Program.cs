using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), 
    new MySqlServerVersion(new Version(8, 0, 21)), 
    b => b.MigrationsAssembly("StoreApp.Web")));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
var app = builder.Build();

app.UseStaticFiles();

// samsung-s24 -> urun detay sayfası

// products/telefon -> kategoriye ait ürün listesi
app.MapControllerRoute("product_in_category", "products/{category?}", new { controller = "Home", action = "Index" });
app.MapControllerRoute("product_details", "{name}", new { controller = "Home", action = "Details" });



app.MapDefaultControllerRoute();

app.Run();
