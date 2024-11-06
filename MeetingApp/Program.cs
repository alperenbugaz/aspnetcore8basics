var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(); //MVC Servisi projeye tanıtıldı

var app = builder.Build();

app.UseStaticFiles(); //wwwroot klasörüne erişim sağlandı

app.UseRouting(); //Routing işlemleri başlatıldı

//app.MappDefaultControllerRoute(); //Controller/Action/Id? şeklinde bir route oluşturuldu

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();
