using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using WebFormL1.BusinessLogic;
using WebFormL1.EditModel;
using WebFormL1.Service;
using WebFormL1.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFormL1.Constant;
using WebFormL1.Interface;
using WebFormL1.Utility;
using WebFormL1.Filters;

namespace WebFormL1.Controllers
{
    [ServiceFilter(typeof(SessionAuthorizeFilter))]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeBL _bl;
        private readonly RepositoryService _service;

        public EmployeesController(IEmployeeBL bl, RepositoryService service)
        {
            _bl = bl;
            _service = service;
        }

        // GET: Employees
        public IActionResult Index(int? page, int? size, string searchString = "")
        {
                int pageIndex = Validate.ValidatePageNumber(ref page);
                int pageSize = Validate.ValidatePageSize(ref size);
                ViewBag.size = GetPageSizeOptions(size);
                ViewBag.currentSize = size;
                var employeesViewModel = _bl.FetchEmployeePage(pageIndex, pageSize, searchString); 
                ViewData["searchString"] = searchString;
                return View(new StaticPagedList<EmployeeViewModel>(employeesViewModel.ViewModel!,
                                         pageIndex, pageSize, employeesViewModel.TotalPages));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = await _bl.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _bl.SetEmployeeViewModel(employee);
            return View(employeeViewModel);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            DropDownListView();
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeEditModel employeeEditModel)
        {
            if (!ModelState.IsValid)
            {
                DropDownListView();
                return View(employeeEditModel);
            }

            var isSucessCreate = await _bl.AddEmployee(employeeEditModel);
            if (!isSucessCreate)
            {
                TempData["ErrorMessage"] = "An error occurred while createing the employee.";
                return View(employeeEditModel);
            }
            TempData["SuccessMessage"] = "Employee created successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = await _bl.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeEditModel = _bl.SetEmployeeEditModel(employee);
            DropDownListView();
            return View(employeeEditModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditModel employeeEditModel)
        {
            if (id != employeeEditModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                DropDownListView();
                return View(employeeEditModel);
            }

            var isSucessEdit = await _bl.EditEmployee(employeeEditModel);
            if (!isSucessEdit)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the employee.";
                return View(employeeEditModel);
            }

            TempData["SuccessMessage"] = "Employee updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = await _bl.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _bl.SetEmployeeViewModel(employee);
            return View(employeeViewModel);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _bl.GetEmployeeByIdDelete(id);
            if (employee != null)
            {
                var isSuccessDelete = await _bl.DeleteEmployee(employee);
                if (!isSuccessDelete)
                {
                    TempData["ErrorMessage"] = "An error occurred while deleting the employee.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["SuccessMessage"] = "Employee deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExportExcel(string searchString = "")
        {
            var employeesViewModel = _bl.GetModelExportData(searchString);

            ViewBag.exportSearchString = searchString;
            return View(employeesViewModel);
        }

        // POST: ExportExcel
        [HttpPost]
        public async Task<IActionResult> ExportExcel(List<int> employeeIdList)
        {
            if (employeeIdList == null || employeeIdList.Count == 0)
            {
                TempData["ErrorMessage"] = "Không có thông tin được chọn";
                return RedirectToAction(nameof(ExportExcel));
            }

            var streamData = await _bl.HandleDataToExportExcel(employeeIdList);
            var fileName = "employeeData.xlsx";
            return File(streamData.ToArray()
                , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        public IActionResult ImportExcel()
        {
            return View();
        }

        // POST: ImportExcel
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile formFile)
        {
            if (_bl.IsInvalidFormFile(formFile))
            {
                TempData["ErrorMessage"] = "File trống hoặc sai định dạng";
                return View();
            }

            var importEmployees = _bl.GetListDataImport(formFile);
            if (importEmployees == null)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return View();
            }

            var validateImportData = ValidateImportEmployee(importEmployees);
            if (validateImportData != 0)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return View();
            }

            if (!await _bl.IsSuccessImport(importEmployees))
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return View();
            }

            TempData["SuccessMessage"] = "Nhập dữ liệu từ Excel thành công";
            return RedirectToAction(nameof(Index));
        }
        public void DropDownListView()
        {
            ViewData["JobData"] = new SelectList(_service.GetJobDbset(), "Id", "JobName");
            ViewData["EthnicData"] = new SelectList(_service.GetEthnicDbset(), "Id", "EthnicName");
            ViewData["ProvinceData"] = new SelectList(_service.GetProvinceDbset(), "Id", "Name");
            ViewData["DistrictData"] = new SelectList(_service.GetDistrictDbset(), "Id", "Name");
            ViewData["CommuneData"] = new SelectList(_service.GetCommuneSet(), "Id", "Name");
        }
    }
}
