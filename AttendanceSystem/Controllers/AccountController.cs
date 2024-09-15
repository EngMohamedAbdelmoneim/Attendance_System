using AttendanceSystem.Models;
using AttendanceSystem.Service;
using AttendanceSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Security.Claims;
namespace AttendanceSystem.Controllers
{
    public class AccountController : Controller
    {
        private IServices<Student> _serviceStudent;
        private IServices<Department> _serviceDepartment;
        private IServices<Intakes> _serviceIntakes;
        private IServices<Programs> _servicePrograms;
        private IServices<Attendance> _serviceAttendance;
        private IServices<Hr> _serviceHr;
        private IAdminService _serviceAdmin;
        public AccountController(IServices<Student> userService, IServices<Intakes> serviceIntakes, IServices<Programs> servicePrograms, IServices<Attendance> serviceAttendance, IServices<Hr> serviceHr, IServices<Department> serviceDepartment, IAdminService adminService)
        {

            _serviceStudent = userService;
            _serviceDepartment = serviceDepartment;
            _serviceIntakes = serviceIntakes;
            _servicePrograms = servicePrograms;
            _serviceAttendance = serviceAttendance;
            _serviceHr = serviceHr;
            _serviceAdmin = adminService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
          User? student = _serviceStudent.GetAll().FirstOrDefault(u => u.Email== model.Email &&   u.Password == model.Password);
          User? hr = _serviceHr.GetAll().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
          Admins? Admin = _serviceAdmin.GetAll().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);


            if (ModelState.IsValid)
            {
                if (Admin != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Sid, Admin.Id.ToString());
                    Claim c2 = new Claim(ClaimTypes.Email, model.Email);
                    Claim c3 = new Claim(ClaimTypes.Role, "Admin");

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);
                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cp);
                    return RedirectToAction("AdminAccount", "Admin");
                }
                    if (student != null)
                {
                    bool IsPresentToday = _serviceAttendance.GetAll().Exists(u => u.StdId == student.Id && u.Date == DateTime.Today);
                    if (!IsPresentToday) {
                        _serviceAttendance.Add(
                              new Attendance
                              {
                                  StdId = student.Id,
                                  Date = DateTime.Today,
                                  IsPresent = true,
                              }
                            );
                    }

                    Claim c1 = new Claim(ClaimTypes.Sid, student.Id.ToString());
                    Claim c2 = new Claim(ClaimTypes.Email, model.Email);
                    Claim c3 = new Claim(ClaimTypes.Role, student.Role);

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);
                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cp);
                    return RedirectToAction("StudentAccount", "Student");
                }
                if (hr != null)
                {

                    Claim c1 = new Claim(ClaimTypes.Sid, hr.Id.ToString());
                    Claim c2 = new Claim(ClaimTypes.Email, model.Email);
                    Claim c3 = new Claim(ClaimTypes.Role, hr.Role);

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);
                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cp);
                    return RedirectToAction("Student", "Student");
   
                }
                else
                {
      
                    return RedirectToAction("Error", "Account");
                }
            }

            return View("Login");
        }

        public IActionResult Register()
        {

            List<Department> Departments = _serviceDepartment.GetAll();
            List<Intakes> Intakes = _serviceIntakes.GetAll();
            List<Programs> Programs = _servicePrograms.GetAll();
            ViewBag.Departments = new SelectList(Departments, "Deptid", "DeptName");
            ViewBag.Intakes = new SelectList(Intakes, "IntakeId", "IntakeName");
            ViewBag.Programs = new SelectList(Programs, "ProgramId", "ProgName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string FileExtension = model.Photo.FileName.Split('.').Last();
                var randomId = "img-" + Guid.NewGuid().ToString();
                string FilePath = $"wwwroot/images/{randomId}.{FileExtension}";

                _serviceStudent.Add(new Student()
                {
                    Name = model.Username,
                    Email = model.Email,
                    Age = model.Age,
                    Password = model.Password,
                    DepartmentId = model.Department,
                    IntakeId = model.Intake,
                    ProgramId = model.Program,
                    IsVerified = false,
                    Role = "Student",
                    PhotoPath = FilePath,

                });

                using (FileStream st = new FileStream(FilePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(st);
                }
                return RedirectToAction("Login", "Account");

            }
            List<Department> Departments = _serviceDepartment.GetAll();
            List<Intakes> Intakes = _serviceIntakes.GetAll();
            List<Programs> Programs = _servicePrograms.GetAll();
            ViewBag.Departments = new SelectList(Departments, "Deptid", "DeptName");
            ViewBag.Intakes = new SelectList(Intakes, "IntakeId", "IntakeName");
            ViewBag.Programs = new SelectList(Programs, "ProgramId", "ProgName");
            return View("Register");
        }


        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
