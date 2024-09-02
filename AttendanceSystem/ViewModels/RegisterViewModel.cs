using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Password { get; set; }
        public string? Role { get; set; }
        public IFormFile Photo {  get; set; }
        public int Department { get; set; }

        public int Program { get; set; }

        public int Intake { get; set; }

    }
}
