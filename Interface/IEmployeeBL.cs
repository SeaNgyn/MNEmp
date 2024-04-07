using System.Data;
using WebFormL1.EditModel;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IEmployeeBL
    {
        Paging<EmployeeViewModel> FetchEmployeePage(int pageIndex, int pageSize,string searchString);
        Task<Employee?> GetEmployeeById(int? id);
        Task<Employee?> GetEmployeeByIdDelete(int? id);
        Task<bool> AddEmployee(EmployeeEditModel employeeEditModel);
        EmployeeEditModel SetEmployeeEditModel(Employee employee);
        Task<bool> EditEmployee(EmployeeEditModel employeeEditModel);
        EmployeeViewModel SetEmployeeViewModel(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);
        bool IsUniqueIdentifyCardNumber(string? identifyCardNumber, int currentId);
        bool IsUniquePhoneNumber(string? phoneNumber, int currentId);
        List<EmployeeViewModel> GetModelExportData(string searchString);
        List<EmployeeViewModel> GetDepartmentData(int departmentId, string searchString);
        Task<MemoryStream> HandleDataToExportExcel(List<int> employeeIdList);
        Task<bool> IsSuccessImport(List<Employee> importEmployees);
        List<Employee>? GetListDataImport(IFormFile formFile);
        DataTable GetTableDataImportExcel(IFormFile formFile);
        bool IsInvalidFormFile(IFormFile formFile);
    }
}
