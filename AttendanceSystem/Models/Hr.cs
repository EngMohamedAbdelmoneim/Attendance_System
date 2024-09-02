using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class Hr : User
    {
        public List<Student>? Students { get; set; } = new List<Student>();

    }
}
