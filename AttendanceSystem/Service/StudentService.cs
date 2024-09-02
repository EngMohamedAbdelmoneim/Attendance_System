using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;
 

namespace AttendanceSystem.Service
{
    public class StudentService : IServices<Student>
    {
        AttendanceSystemContext db;

        public StudentService (AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var st = db.Students.SingleOrDefault(s => s.Id == id);
            db.Students.Remove(st);
            db.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return db.Students.Include(a => a.DepartmentNavigation).Include(d => d.Intake)
                .Include(d => d.Program)
                .Include(a => a.attendances).ToList();
        }

        public Student GetById(int? id)
        {
            return (Student)db.Students
                .Include(d => d.DepartmentNavigation)
                .Include(d => d.Intake)
                .Include(d => d.Program)
                .Include(a =>a.attendances)
                .SingleOrDefault(s => s.Id == id);
        }

        public void Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
         }
    }
}
