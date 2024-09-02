
using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public class AdminService : IAdminService
    {
        AttendanceSystemContext db;

        public AdminService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public List<Admins> GetAll()
        {
            return db.Admins.ToList();
        }

        public Admins GetById(int? id)
        {
            return (Admins)db.Admins.SingleOrDefault(a => a.Id == id);
        }
    }
}
