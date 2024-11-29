using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola tekrarı alanı gereklidir")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}