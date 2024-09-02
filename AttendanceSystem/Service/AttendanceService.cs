using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Service
{
    public class AttendanceService : IServices<Attendance>
    {
        AttendanceSystemContext db;

        public AttendanceService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Attendance m)
        {
            db.Attendance.Add(m);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Attendance> GetAll()
        {
            return db.Attendance.ToList();
        }

        public List<Attendance> GetAllByDate(DateTime date)
        {
            return db.Attendance.Where(a => a.Date == date).ToList();
        }

        public List<Attendance> GetById(int? id)
        {
            return db.Attendance.Where(a => a.StdId == id).ToList();
        }

        public void Update(Attendance m)
        {
            throw new NotImplementedException();
        }

        Attendance IServices<Attendance>.GetById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
