using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public class ProgramService : IServices<Programs>
    {

        AttendanceSystemContext db;

        public ProgramService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Programs m)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Programs> GetAll()
        {
            return db.Programs.ToList();
        }

        public Programs GetById(int? id)
        {
            return (Programs)db.Programs.SingleOrDefault(i => i.ProgramId == id);
        }

        public void Update(Programs m)
        {
            throw new NotImplementedException();
        }
    }
}
