using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FormsApp.Models
{   
    public class Product
    {   
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }  = string.Empty;
        [Required(ErrorMessage = "Please enter a price")]
        [Range(0, 100000, ErrorMessage = "Please enter a valid price")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Image")]
        public string? imageFile { get; set; } = string.Empty;

        [Display(Name = "Is Available")]

        public bool IsAvailable { get; set; }
        [Display(Name = "Category Id")]
        [Required]
        public int? CategoryId { get; set; }
    }
}
