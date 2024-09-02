using AttendanceSystem.Models;
using AttendanceSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService serviceAdmin;


        public AdminController(IAdminService _serviceAdmin)
        {
            serviceAdmin = _serviceAdmin;
        }

        public IActionResult AdminAccount()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId != null)
            {
                Admins Admin = serviceAdmin.GetById(int.Parse(userId));
                return View(Admin);

            }
            else
                return RedirectToAction("Logout", "Account");

        }
    }
}
