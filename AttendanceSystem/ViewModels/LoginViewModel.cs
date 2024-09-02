using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
