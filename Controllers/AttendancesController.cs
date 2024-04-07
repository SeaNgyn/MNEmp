using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Service;

namespace WebFormL1.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeBL _bl;
        private readonly RepositoryService _service;
        public AttendancesController(ApplicationDbContext context, IEmployeeBL bl, RepositoryService service)
        {
            _context = context;
            _bl = bl;
            _service = service;
        }
        public IActionResult Index(string searchString = "")
        {
            var employeesViewModel = _bl.GetModelExportData(searchString);
            ViewBag.exportSearchString = searchString;
            return View(employeesViewModel);
        }
        [HttpPost]
        public IActionResult Index(Dictionary<int, bool> attendanceStatus, DateTime attendanceDate)
        {
            var employees = _context.Employees.ToList();
            foreach(var employee in employees) 
            {
                bool status = attendanceStatus.ContainsKey(employee.Id) ? attendanceStatus[employee.Id] : false;
                Attendance attendance = new Attendance()
                {
                    EmployeeId = employee.Id,
                    AttendanceDate = attendanceDate,
                    Attended = status
                };
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
            }
            return RedirectToAction("Index","Employees");
        }
    }
}
