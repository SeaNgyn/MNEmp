using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IDegreeService
    {
        IQueryable<Degree>? GetAll(int employeeId);
        IQueryable<DegreeViewModel> GetPagedDegreeViewModels(IQueryable<Degree> degrees);
        Task<Degree?> GetDegreeById(int? id);
        int GetDegreeCountForEmployee(int employeeId);
        IBaseService<Degree> BaseService();
        Task<Employee?> GetEmployeeById(int? id);
    }
}
