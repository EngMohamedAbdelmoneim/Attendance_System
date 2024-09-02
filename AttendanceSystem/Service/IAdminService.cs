using AttendanceSystem.Models;

namespace AttendanceSystem.Service
{
    public interface IAdminService
    {

        public List<Admins> GetAll();
        public Admins GetById(int? id);
    }
}
