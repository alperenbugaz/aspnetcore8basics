using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Kurs
    {   
        [Key]
        public int? KursId { get; set; }

        [Required (ErrorMessage = "Kurs adı boş geçilemez")]
        [StringLength(50, ErrorMessage = "Kurs başlığı en fazla 50 karakter olabilir")]
        public string? Baslik { get; set; }

        [Required (ErrorMessage = "Kurs açıklaması boş geçilemez")]
        [StringLength(500, ErrorMessage = "Kurs açıklaması en fazla 500 karakter olabilir")]
        public string? KursAciklama { get; set; }

        [Required (ErrorMessage = "Kurs ücreti boş geçilemez")]
        [Range(0, 1000, ErrorMessage = "Kurs ücreti 0 ile 1000 arasında olmalıdır")]

        public decimal? KursUcret { get; set; }

        public int? OgretmenId { get; set; }
        public Ogretmen? Ogretmen { get; set; }
        public ICollection<KursKayit>? KursKayitlari { get; set; }
    }
}