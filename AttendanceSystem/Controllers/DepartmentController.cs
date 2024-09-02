using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;
using AttendanceSystem.Service;
using Microsoft.AspNetCore.Authorization;
namespace AttendanceSystem.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
		private IServices<Student> studentService;
		private IServices<Department> departmentService;

		public DepartmentController(IServices<Student> _studentService, IServices<Department> _DepartmentService)
		{
			studentService = _studentService;

			departmentService = _DepartmentService;
		}


		public IActionResult Department()
        {
            List<Department> listD = departmentService.GetAll();
            return View(listD);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();


            Department d =departmentService.GetById(id);
            if (d == null || id > d.Deptid)
            {
                return NotFound();
            }
            return View(d);
        }
        public IActionResult Create(Department dept)
        {
            departmentService.Add(dept);
       
            return RedirectToAction("Department");
        }
        [HttpPost]
        public IActionResult Create()
        {
            var listS = departmentService.GetAll();

			SelectList list = new SelectList(listS, "DeptName", "DeptName");
            ViewBag.deplist = list;
            return View();
        }
		[HttpPost]
		public IActionResult Update(int id, Department dept)
        {
			departmentService.Update(dept);

            return RedirectToAction("Department");
        }
        public IActionResult Update(int id)
        {
            Department d = departmentService.GetById(id);
            if (d == null)
            {
                return NotFound();
            }
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "DeptName", "DeptName");
            ViewBag.deplist = list;
            return View(d);
        }

        public IActionResult Delete(int id)
        {
            var st = studentService.GetAll().Where(a => a.DepartmentId == id);
            foreach (var s in st)
            {

                s.DepartmentId = null;
            }

            departmentService.Delete(id);

          
            return RedirectToAction("Department");
        }




  

	}   
}
