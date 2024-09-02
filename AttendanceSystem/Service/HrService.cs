
using AttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Service
{
    public class HrService : IServices<Hr>
    {
        AttendanceSystemContext db;

        public HrService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Hr hr)
        {
            db.Hr.Add(hr);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var hr = db.Hr.SingleOrDefault(s => s.Id == id);
            db.Hr.Remove(hr);
            db.SaveChanges();
        }

        public List<Hr> GetAll()
        {
            return db.Hr.Include(d => d.DepartmentNavigation)
                .Include(d => d.Intake)
                .Include(d => d.Program).ToList();
        }

        public Hr GetById(int? id)
        {
            return (Hr)db.Hr.Include(d => d.DepartmentNavigation).Include(d => d.Intake).Include(d => d.Program).SingleOrDefault(s => s.Id == id);
        }

        public void Update(Hr hr)
        {
            db.Hr.Update(hr);
            db.SaveChanges();
        }
    }
}
