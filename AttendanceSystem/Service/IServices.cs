using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public interface IServices<M>
    {
        public List<M> GetAll();
        public M GetById(int? id);


        public void Add(M m);


        public void Update(M m);


        public void Delete(int id);
    }
}
