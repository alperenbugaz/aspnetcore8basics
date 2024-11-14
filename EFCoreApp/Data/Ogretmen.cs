using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Ogretmen
    {   
        [Key]
        public int OgretmenId { get; set; }

        [Required (ErrorMessage = "Ad alanı boş geçilemez")] 
        public string? Ad { get; set; }

        [Required (ErrorMessage = "Soyad alanı boş geçilemez")]
        public string? Soyad { get; set; }

        [Required (ErrorMessage = "Telefon alanı boş geçilemez")]
        public string? Telefon { get; set; }

        [Required (ErrorMessage = "Email alanı boş geçilemez")]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BaslamaTarihi { get; set; }

        public ICollection<Kurs>? Kurslar { get; set; } = null!;

        public string AdSoyad { get { return this.Ad + " " + this.Soyad; } }
    }
}