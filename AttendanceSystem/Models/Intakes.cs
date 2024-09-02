using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class Intakes
    {
        [Key]
        public int IntakeId { get; set; }
        public string IntakeName { get; set; }
        public string? Year { get; set; }

       
        public virtual ICollection<Student>? Students { get; set; }
    }
}
