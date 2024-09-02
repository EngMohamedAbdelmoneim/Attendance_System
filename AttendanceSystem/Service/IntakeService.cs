using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public class IntakeService : IServices<Intakes>
    {
        AttendanceSystemContext db;

        public IntakeService(AttendanceSystemContext _db)
        {
            db = _db;
        }
        public void Add(Intakes m)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Intakes> GetAll()
        {
            return db.Intakes.ToList();
        }

        public Intakes GetById(int? id)
        {
            return (Intakes)db.Intakes.SingleOrDefault(i =>i.IntakeId == id);
        }

        public void Update(Intakes m)
        {
            throw new NotImplementedException();
        }
    }
}
