using EFCoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Data
{
    public class DataContext : DbContext
    {   
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<KursKayit> KursKayitlari { get; set; }
        
        //Code First = Veritabanı oluşturulurken veritabanı tabloları oluşturulurken kullanılacak ayarlar
        //Database First = Veritabanı oluşturulmuş ve tabloları oluşturulmuş bir veritabanı ile çalışırken kullanılacak ayarlar
        public DbSet<Ogretmen> Ogretmenler { get; set; }

    }
}