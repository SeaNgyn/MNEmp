using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using System.Data;
using System.Globalization;
using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Service;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.BusinessLogic
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeService _service;
        private readonly IBaseService<Employee> _baseService;
        public EmployeeBL(IEmployeeService service,IBaseService<Employee> baseService)
        {
            _service = service;
            _baseService = baseService;
        }
        public Paging<EmployeeViewModel> FetchEmployeePage(int pageIndex, int pageSize,string searchString)
        {
            int totalPages = _service.Count(searchString);
            var employeesViewModel = _service.GetPagedEmployeeViewModels(pageIndex, pageSize,searchString).ToList();

            return new Paging<EmployeeViewModel>(totalPages, employeesViewModel);
        }
        public async Task<Employee?> GetEmployeeById(int? id)
        {
            return await _service.GetEmployeeById(id);
        }

        public async Task<Employee?> GetEmployeeByIdDelete(int? id)
        {
            return await _service.BaseService().GetDbSet()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<bool> AddEmployee(EmployeeEditModel employeeEditModel)
        {
            try
            {
                var employee = new Employee(employeeEditModel.Id, employeeEditModel.Name, employeeEditModel.DateOfBirth,
                     employeeEditModel.EthnicId, employeeEditModel.JobId,
                    employeeEditModel.IdentifyCardNumber, employeeEditModel.PhoneNumber, employeeEditModel.ProvinceId,
                    employeeEditModel.DistrictId, employeeEditModel.CommuneId);

                _service.BaseService().Insert(employee);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public EmployeeEditModel SetEmployeeEditModel(Employee employee)
        {
            return new EmployeeEditModel(employee.Id, employee.Name, employee.DateOfBirth,
                     employee.EthnicId, employee.JobId, employee.IdentityCardNumber,
                    employee.PhoneNumber, employee.ProvinceId, employee.DistrictId, employee.CommuneId);
        }

        public async Task<bool> EditEmployee(EmployeeEditModel employeeEditModel)
        {
            try
            {
                var employee = new Employee(employeeEditModel.Id, employeeEditModel.Name, employeeEditModel.DateOfBirth,
                                           employeeEditModel.JobId, employeeEditModel.EthnicId,
                                           employeeEditModel.IdentifyCardNumber, employeeEditModel.PhoneNumber, employeeEditModel.ProvinceId,
                                           employeeEditModel.DistrictId, employeeEditModel.CommuneId);

                _service.BaseService().Update(employee);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public EmployeeViewModel SetEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel(
                    employee.Id, employee.Name, employee.DateOfBirth,
                     employee.Ethnic!.EthnicName!, employee.Job!.JobName!,
                    employee.IdentityCardNumber, employee.PhoneNumber, employee.Province!.Name,
                    employee.District!.Name, employee.Commune!.Name, employee.Degrees!.Count);
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            try
            {
                _service.BaseService().Delete(employee);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsUniqueIdentifyCardNumber(string? identifyCardNumber, int currentId)
        {
            if (String.IsNullOrEmpty(identifyCardNumber))
            {
                return true;
            }
            var employee = _service.BaseService().GetDbSet()
                                .FirstOrDefault(e => e.IdentityCardNumber == identifyCardNumber && e.Id != currentId);
            if (employee != null)
            {
                return false;
            }
            return true;
        }
        public bool IsUniquePhoneNumber(string? phoneNumber, int currentId)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                return true;
            }
            var employee = _service.BaseService().GetDbSet()
                                .FirstOrDefault(e => e.PhoneNumber == phoneNumber && e.Id != currentId);
            if (employee != null)
            {
                return false;
            }
            return true;
        }

        public List<EmployeeViewModel> GetModelExportData(string searchString)
        {
            var employeesViewModel = _service.GetEmployeeViewModels(searchString).ToList();

            return employeesViewModel;
        }
        public List<EmployeeViewModel> GetDepartmentData(int departmentId, string searchString)
        {
            var employeesViewModel = _service.GetEmployeeDepartmentViewModels(departmentId,searchString).ToList();

            return employeesViewModel;
        }
        public async Task<List<EmployeeViewModel>> GetSelectedEmployees(List<int> employeeIdList)
        {
            var selectedEmployee = new List<EmployeeViewModel>();
            foreach (var id in employeeIdList)
            {
                var employee = await _service.GetEmployeeById(id);
                var employeeViewModel = SetEmployeeViewModel(employee!);
                selectedEmployee.Add(employeeViewModel!);
            }
            return selectedEmployee;
        }
        public DataTable GetTableDataExportExcel(List<EmployeeViewModel> selectedEmployee)
        {
            var dt = new DataTable()
            {
                TableName = "EmployeeData"
            };

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(DateTime));
            dt.Columns.Add("EthnicityName", typeof(string));
            dt.Columns.Add("JobName", typeof(string));
            dt.Columns.Add("IdentityCardNumber", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("ProvinceName", typeof(string));
            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("CommuneName", typeof(string));
            dt.Columns.Add("NumberOfDegrees", typeof(int));

            foreach (var employee in selectedEmployee)
            {
                dt.Rows.Add(employee.Id, employee.Name, employee.DateOfBirth,
                    employee.EthnicityName, employee.JobName, employee.IdentityCardNumber,
                    employee.PhoneNumber, employee.ProvinceName, employee.DistrictName, employee.CommuneName,employee.NumberOfDegrees);
            }
            dt.AcceptChanges();
            return dt;
        }
        public async Task<MemoryStream> HandleDataToExportExcel(List<int> employeeIdList)
        {
            var selectedEmployee = await GetSelectedEmployees(employeeIdList);

            var dataTable = GetTableDataExportExcel(selectedEmployee);

            using var wb = new XLWorkbook();
            wb.Worksheets.Add(dataTable);

            var stream = new MemoryStream();
            wb.SaveAs(stream);

            return stream;
        }
        public async Task<bool> IsSuccessImport(List<Employee> importEmployees)
        {
            try
            {
                _service.InsertImportData(importEmployees);
                await _baseService.SaveChangeAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Employee>? GetListDataImport(IFormFile formFile)
        {
            try
            {
                var dt = GetTableDataImportExcel(formFile);
                var importEmployees = new List<Employee>();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var employee = new Employee
                    {
                        Name = dt.Rows[i]["Name"]?.ToString(),
                        DateOfBirth = DateTime.Parse(dt.Rows[i]["DateOfBirth"].ToString()!, new CultureInfo("vi-VN")),
                        EthnicId = dt.Rows[i]["EthnicId"]?.ToString() == null ? 0 : Convert.ToInt32(dt.Rows[i]["EthnicId"]),
                        JobId = String.IsNullOrEmpty(dt.Rows[i]["JobId"]?.ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["JobId"]),
                        IdentityCardNumber = dt.Rows[i]["IdentityCardNumber"]?.ToString(),
                        PhoneNumber = dt.Rows[i]["PhoneNumber"]?.ToString(),
                        ProvinceId = String.IsNullOrEmpty(dt.Rows[i]["ProvinceId"]?.ToString()) ? null : Convert.ToInt32(dt.Rows[i]["ProvinceId"]),
                        DistrictId = String.IsNullOrEmpty(dt.Rows[i]["DistrictId"]?.ToString()) ? null : Convert.ToInt32(dt.Rows[i]["DistrictId"]),
                        CommuneId = String.IsNullOrEmpty(dt.Rows[i]["CommuneId"]?.ToString()) ? null : Convert.ToInt32(dt.Rows[i]["CommuneId"])
                    };

                    importEmployees.Add(employee);
                }

                return importEmployees;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetTableDataImportExcel(IFormFile formFile)
        {
            var dt = new DataTable();
            using var stream = new MemoryStream();

            formFile.CopyTo(stream);

            using var workBook = new XLWorkbook(stream);
            var workSheet = workBook.Worksheet(1);
            var firstRow = true;

            foreach (var row in workSheet.Rows())
            {
                if (firstRow)
                {
                    foreach (var cell in row.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }
                    firstRow = false;
                }
                else
                {
                    dt.Rows.Add();
                    var i = 0;
                    foreach (var cell in row.Cells(false))
                    {
                        dt.Rows[^1][i] = cell.Value.ToString();
                        i++;
                    }
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        public bool IsInvalidFormFile(IFormFile formFile)
        {
            return formFile == null || formFile.Length == 0
                || (Path.GetExtension(formFile.FileName).ToLower() != ".xlsx"
                && Path.GetExtension(formFile.FileName).ToLower() != ".xlsm");
        }
    }
}
