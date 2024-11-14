using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class KursKayit
    {   
        [Key]
        public int KursKayitId { get; set; }
        public int OgrenciId { get; set; }
        public int KursId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;
        public Kurs Kurs { get; set; } = null!;
        public DateTime KayitTarihi { get; set; }
    }
}