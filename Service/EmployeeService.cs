using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFormL1.BusinessLogic;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IBaseService<Employee> BaseService()
        {
            return new BaseService<Employee>(_context);
        }

        public int Count(string searchString)
        {
            return GetEmployeesBySearchString(searchString).Count();
        }

        public IQueryable<Employee>? GetAll()
        {
            return _context.Employees
                                .Include(e => e.Ethnic)
                                .Include(e => e.Job)
                                .Include(e => e.Province)
                                    .ThenInclude(p => p!.Districts)
                                .Include(e => e.District)
                                    .ThenInclude(d => d!.Communes)
                                .Include(e => e.Commune)
                                .Include(e => e.Degrees)
                                .AsNoTracking();
        }
        public async Task<Employee?> GetEmployeeById(int? id)
        {
            return await GetAll()!.FirstOrDefaultAsync(e => e.Id == id);
        }
        public IQueryable<Employee> GetEmployeesBySearchStringAndDepartment(int departmentId,string searchString)
        {
            return _context.Employees.Where(e =>( e.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Ethnic!.EthnicName!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Job!.JobName!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Province!.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.District!.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Commune!.Name!.ToLower().Contains(searchString.Trim().ToLower())) && e.DepartmentId!= departmentId);
        }
        public IQueryable<Employee> GetEmployeesBySearchString(string searchString)
        {
            return _context.Employees.Where(e => e.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Ethnic!.EthnicName!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Job!.JobName!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Province!.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.District!.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                            e.Commune!.Name!.ToLower().Contains(searchString.Trim().ToLower()));
        }
        public IQueryable<EmployeeViewModel> GetPagedEmployeeViewModels(int pageIndex, int pageSize,string searchString)
        {
            return GetEmployeesBySearchString(searchString).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(employee => new EmployeeViewModel(
                    employee.Id, employee.Name, employee.DateOfBirth,
                     employee.Ethnic!.EthnicName!, employee.Job!.JobName!,
                    employee.IdentityCardNumber, employee.PhoneNumber, employee.Province!.Name,
                    employee.District!.Name, employee.Commune!.Name,  employee.Degrees!.Count,_context.Attendances.Where(a=>a.Attended==true && a.EmployeeId == employee.Id).Count(), _context.Attendances.Where(a => a.EmployeeId == employee.Id).Count()));
        }
        public IQueryable<EmployeeViewModel> GetEmployeeViewModels(string searchString)
        {
            return GetEmployeesBySearchString(searchString)
                .Select(employee => new EmployeeViewModel(
                    employee.Id, employee.Name, employee.DateOfBirth,
                     employee.Ethnic!.EthnicName!, employee.Job!.JobName!,
                    employee.IdentityCardNumber, employee.PhoneNumber, employee.Province!.Name,
                    employee.District!.Name, employee.Commune!.Name, employee.Degrees!.Count));
        }
        public IQueryable<EmployeeViewModel> GetEmployeeDepartmentViewModels(int departmentId,string searchString)
        {
            return GetEmployeesBySearchStringAndDepartment(departmentId,searchString)
                .Select(employee => new EmployeeViewModel(
                    employee.Id, employee.Name, employee.DateOfBirth,
                     employee.Ethnic!.EthnicName!, employee.Job!.JobName!,
                    employee.IdentityCardNumber, employee.PhoneNumber, employee.Province!.Name,
                    employee.District!.Name, employee.Commune!.Name, employee.Degrees!.Count));
        }

        public void InsertImportData(List<Employee> employees)
        {
            _context.Employees.AddRange(employees);
        }
    }
}
