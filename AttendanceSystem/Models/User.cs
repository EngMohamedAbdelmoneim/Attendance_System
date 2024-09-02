using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(10, 30)]
        public int? Age { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string? PhotoPath { get; set; }

        public string Role { get; set; }

        [ForeignKey("Programs")]
        public int? ProgramId { get; set; }

        [ForeignKey("Intakes")]
        public int? IntakeId { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public virtual Department DepartmentNavigation { get; set; }

        public virtual Intakes Intake { get; set; }

        public virtual Programs Program { get; set; }
    }
}
