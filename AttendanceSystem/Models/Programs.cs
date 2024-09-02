using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class Programs
    {
        [Key]
        public int ProgramId { get; set; }
        public string ProgName { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}
