using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class UserDTO
    {   
        [Required]
        public string? UserName { get; set; } 
        public string FullName { get; set; } = null!;
        public string? Email { get; set; } 
        public string Password { get; set; }  = null!;
    }
}