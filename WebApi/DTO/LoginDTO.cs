using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class LoginDTO
    {   
        [Required]
        public string UserName { get; set; }  = null!;
        public string Password { get; set; }  = null!;
    }
}