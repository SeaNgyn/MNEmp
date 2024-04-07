using Microsoft.EntityFrameworkCore;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Service
{
    public class DegreeService : IDegreeService
    {
        private readonly ApplicationDbContext _context;
        public DegreeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IBaseService<Degree> BaseService()
        {
            return new BaseService<Degree>(_context);
        }

        public IQueryable<Degree>? GetAll(int employeeId)
        {
            return _context.Degrees.Include(d => d.Employee)
                                         .Include(d => d.Province)
                                         .Where(d => d.EmployeeId.Equals(employeeId));
        }

        public int GetDegreeCountForEmployee(int employeeId)
        {
            return _context.Degrees
                    .Include(d => d.Employee)
                    .Where(d => d.EmployeeId.Equals(employeeId))
                    .Count();
        }

        public async Task<Degree?> GetDegreeById(int? id)
        {
            return await _context.Degrees
                    .Include(d => d.Employee)
                    .Include(d => d.Province)
                    .FirstOrDefaultAsync(m => m.ID == id);
        }

        public IQueryable<DegreeViewModel> GetPagedDegreeViewModels(IQueryable<Degree> degrees)
        {
            return degrees.Select(degree => new DegreeViewModel(degree.ID, degree.Name!, degree.DateOfIssue,
                                           degree.Province!.Name!, degree.ExpirationDate,
                                           degree.Employee!.Name!, degree.EmployeeId));
        }
        public async Task<Employee?> GetEmployeeById(int? id)
        {
            return await _context.Employees.Include(e => e.Degrees).FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
