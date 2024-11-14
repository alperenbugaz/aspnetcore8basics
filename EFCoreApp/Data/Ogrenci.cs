using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Ogrenci
    {   
        [Key]
        public int OgrenciId { get; set; } // id => primary key

        [Required (ErrorMessage = "Öğrenci adı boş geçilemez")]
        [StringLength(50, ErrorMessage = "Öğrenci adı en fazla 50 karakter olabilir")]
        public string? OgrenciAd { get; set; }

        [Required (ErrorMessage = "Öğrenci soyadı boş geçilemez")]
        [StringLength(50, ErrorMessage = "Öğrenci soyadı en fazla 50 karakter olabilir")]
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad 
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
            
            
        }


        [Required (ErrorMessage = "Öğrenci e-mail boş geçilemez")]
        [StringLength(50, ErrorMessage = "Öğrenci e-mail en fazla 50 karakter olabilir")]
        public string? EMail { get; set; }

        [Required (ErrorMessage = "Öğrenci telefon boş geçilemez")]
        [StringLength(50, ErrorMessage = "Öğrenci telefon en fazla 50 karakter olabilir")]
        public string? Telefon { get; set; }

        public ICollection<KursKayit>? KursKayitlari { get; set; } 

        
    }
}