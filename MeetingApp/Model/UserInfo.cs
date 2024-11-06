using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Model
{
    public class UserInfo
    {   
        public int Id{ get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Mail")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your WillAttend")]
        public bool WillAttend { get; set; }
    }
}