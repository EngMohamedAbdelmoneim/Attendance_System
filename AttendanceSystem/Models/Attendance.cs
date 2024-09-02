using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        [ForeignKey("Student")]
        public int StdId { get; set; }

        public virtual Student? Student { get; set; }
    }
}
