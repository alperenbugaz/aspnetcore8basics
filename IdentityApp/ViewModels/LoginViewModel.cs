using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class LoginViewModel
{   
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; } = true;
}
