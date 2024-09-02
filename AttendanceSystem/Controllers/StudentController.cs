using AttendanceSystem.Models;
using AttendanceSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{

	public class StudentController : Controller
    {
        private IServices<Student> studentService;
        private IServices<Department> departmentService;

        public StudentController(IServices<Student> _studentService, IServices<Department> _DepartmentService)
        {
            studentService = _studentService;

            departmentService = _DepartmentService;
        }

        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Student()
        {
            var listS = studentService.GetAll();
            return View(listS);
        }
        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var s = studentService.GetById(id);

            if (s == null || id > s.Id)
            {
                return NotFound();
            }

            return View(s);
        }
        [Authorize(Roles = "Hr,Admin")]
        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                studentService.Add(std);
                return RedirectToAction("Student");
            }
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "Deptid", "DeptName");
            ViewBag.deplist = list;
            return View("AddStudent");
        }
        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Create()
        {
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "Deptid", "DeptName");
            ViewBag.deplist = list;
            return View();
        }
        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Update(int id)
        {
            Student s = studentService.GetById(id);
            if (s == null)
            {
                return NotFound();
            }
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "Deptid", "DeptName");
            ViewBag.deplist = list;
            return View(s);
        }
		[HttpPost]
        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Update(Student std)
        {
            studentService.Update(std);
            return RedirectToAction("Student");
        }
		[Authorize(Roles = "Hr,Admin")]
		public IActionResult Delete(int id)
        {
            studentService.Delete(id);
            return RedirectToAction("Student");
        }

        [Authorize(Roles = "Hr,Admin")]
        public IActionResult StudentAccount()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId != null)
            {
                Student st = studentService.GetById(int.Parse(userId));
                return View(st);
            }
            else
                return RedirectToAction("Logout", "Account");
            
        }

        [HttpPost]
        [Authorize(Roles = "Hr,Admin")]
        public IActionResult Verfied(int id)
        {
            Student st = studentService.GetById(id);
            st.IsVerified = !st.IsVerified;
            studentService.Update(st);
            return  RedirectToAction("Student", "Student");
        }
    


    }
}
