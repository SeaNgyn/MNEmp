using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFormL1.BusinessLogic;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Service;

namespace WebFormL1.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: DepartmentsControllercs
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeBL _bl;
        private readonly RepositoryService _service;
        public DepartmentsController(ApplicationDbContext context, IEmployeeBL bl, RepositoryService service)
        {
            _context = context;
            _bl = bl;
            _service = service; 
        }
        public IActionResult Index()
        {
            var department = _context.Departments.ToList();
            return View(department);
        }
        [HttpGet]
        public IActionResult AddEmployees(int id,string searchString = "")
        {
            var employeesViewModel = _bl.GetDepartmentData(id,searchString);
            ViewBag.Employees = _context.Employees.Where(e=>e.DepartmentId == id).ToList();
            ViewBag.Name = _context.Departments.FirstOrDefault(d => d.DepartmentId == id).DepartmentName;
            ViewBag.exportSearchString = searchString;
            return View(employeesViewModel);
        }
        [HttpPost]
        public IActionResult AddEmployees(int id, List<int> employeeIdList)
        {
            if (employeeIdList == null || employeeIdList.Count == 0)
            {
                TempData["ErrorMessage"] = "Không có thông tin được chọn";
                return RedirectToAction(nameof(AddEmployees));
            }
            foreach(int employeeId in employeeIdList)
            {
                Employee employee = _context.Employees.FirstOrDefault(e=>e.Id== employeeId);
                employee.DepartmentId = id;
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(AddEmployees));
        }
        public IActionResult Remove(int employeeId,int departmentId)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            employee.DepartmentId = null;
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(AddEmployees), new { id = departmentId,searchString = "" }) ;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentsControllercs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentsControllercs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentsControllercs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentsControllercs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
