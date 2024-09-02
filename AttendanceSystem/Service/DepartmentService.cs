using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public class DepartmentService : IServices<Department>
    {
		AttendanceSystemContext db;

        public DepartmentService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
			var d = db.Students.SingleOrDefault(d => d.Id == id);

			db.Remove(d);
            db.SaveChanges();
        }

        public List<Department> GetAll()
        {
            return db.Departments.ToList();
        }

        public Department GetById(int? id)
        {
            return (Department)db.Departments.SingleOrDefault(s => s.Deptid == id);
        }

        public void Update(Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }


    }
}
