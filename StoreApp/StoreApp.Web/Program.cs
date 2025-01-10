using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), 
    new MySqlServerVersion(new Version(8, 0, 21)), 
    b => b.MigrationsAssembly("StoreApp.Web")));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
// samsung-s24 -> urun detay sayfası

// products/telefon -> kategoriye ait ürün listesi
app.MapControllerRoute("product_in_category", "products/{category?}", new { controller = "Home", action = "Index" });
app.MapControllerRoute("product_details", "{name}", new { controller = "Home", action = "Details" });



app.MapDefaultControllerRoute();
app.MapRazorPages();
app.Run();
