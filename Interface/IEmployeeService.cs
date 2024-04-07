using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IEmployeeService
    {
        IQueryable<Employee>? GetAll();
        IQueryable<EmployeeViewModel> GetPagedEmployeeViewModels(int pageIndex, int pageSize,string searchString);
        Task<Employee?> GetEmployeeById(int? id);
        IBaseService<Employee> BaseService();
        IQueryable<EmployeeViewModel> GetEmployeeViewModels(string searchString);
        IQueryable<EmployeeViewModel> GetEmployeeDepartmentViewModels(int derpartmentId, string searchString);
        int Count(string searchString);
        void InsertImportData(List<Employee> employees);
    }
}
