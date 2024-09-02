using AttendanceSystem.Models;
using AttendanceSystem.Service;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttendanceSystem.Controllers
{
    public class HrController : Controller
    {
        private IServices<Hr> HrService;
        private IServices<Department> departmentService;
        private IServices<Student> studentService;
        private readonly IConverter _converter;

    
        public HrController(IServices<Hr> _HrService, IServices<Department> _DepartmentService, IConverter converter, IServices<Student> _stuudentService)
        {
             HrService = _HrService;
             departmentService = _DepartmentService;
            _converter = converter;
            studentService = _stuudentService;
        }

        public IActionResult HrAccount()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId != null)
            {
                Hr hr = HrService.GetById(int.Parse(userId));
                return View(hr);

            }
            else
                return RedirectToAction("Logout", "Account");
            
        }

        public IActionResult Hr()
        {
            var listS = HrService.GetAll();
            return View(listS);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var s = HrService.GetById(id);

            if (s == null || id > s.Id)
            {
                return NotFound();
            }

            return View(s);
        }
        [HttpPost]
        public IActionResult Create(Hr hr)
        {
            if (ModelState.IsValid)
            {
                HrService.Add(hr);
                return RedirectToAction("Hrs");
            }
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "Deptid", "DeptName");
            ViewBag.deplist = list;
            return View("AddHr");
        }
        public IActionResult Create()
        {
            var listS = departmentService.GetAll();
            SelectList list = new SelectList(listS, "Deptid", "DeptName");
            ViewBag.deplist = list;
            return View();
        }
        public IActionResult Update(int id)
        {
            Hr s = HrService.GetById(id);
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
        public IActionResult Update(Hr Hr)
        {
            HrService.Update(Hr);
            return RedirectToAction("Hr");
        }
        public IActionResult Delete(int id)
        {
            HrService.Delete(id);
            return RedirectToAction("Hr");
        }


        [HttpPost]
        public IActionResult ConvertCurrentHtmlToPdf([FromForm] string fullHtmlContent)
        {

            // Define the HTML to PDF document
            var pdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = fullHtmlContent, // Use the captured HTML content
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = "/lib/bootstrap/dist/css/bootstrap.min.css", }

                    }
                }
            };

            // Convert HTML content to PDF
            byte[] pdf = _converter.Convert(pdfDocument);

            // Return the PDF file as a downloadable file
            return File(pdf, "application/pdf", "CurrentPage.pdf");
        }

        public IActionResult ImportExcel()
        {
            return View();
        } 

		[HttpPost]
		public async Task<IActionResult> ImportExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				ModelState.AddModelError("File", "Please upload a file.");
				return View();
			}
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
					var rowCount = worksheet.Dimension.Rows;

					for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
					{
						var student = new Student
						{
							IsVerified = worksheet.Cells[row, 1].Value?.ToString() == "1",
                            HrId = int.Parse(worksheet.Cells[row, 2].Value?.ToString() ?? "0"),
							Name = worksheet.Cells[row, 3].Value?.ToString(),
							Age = int.Parse(worksheet.Cells[row, 4].Value?.ToString() ?? "0"),
							Email = worksheet.Cells[row, 5].Value?.ToString(),
							Password = worksheet.Cells[row, 6].Value?.ToString(),
							PhotoPath = worksheet.Cells[row, 7].Value?.ToString(),
							Role = worksheet.Cells[row, 8].Value?.ToString(),
							ProgramId = int.Parse(worksheet.Cells[row, 9].Value?.ToString() ?? "0"),
							IntakeId = int.Parse(worksheet.Cells[row, 10].Value?.ToString() ?? "0"),
							DepartmentId = int.Parse(worksheet.Cells[row, 11].Value?.ToString() ?? "0")
						};

                        studentService.Add(student);
					}
				}
			}

			return RedirectToAction("HrAccount","Hr");
		}
	}
}
  
 
