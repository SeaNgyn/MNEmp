using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFormL1.BusinessLogic;
using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Service;
using WebFormL1.ViewModel;

namespace WebFormL1.Controllers
{
    public class DegreesController : Controller
    {
        readonly int MaxNumberOfDegrees = 3;
        private readonly RepositoryService _service;
        private readonly IDegreeBL _bl;
        public DegreesController(RepositoryService service, IDegreeBL bl)
        {
            _service = service;
            _bl = bl;
        }

        // GET: Degrees
        public async Task<IActionResult> Index(int employeeId)
        {
            var degreesViewModel = _bl.FetchDegree(employeeId).ToList();
            await ViewEmployeeData(employeeId);
            return View(degreesViewModel);
        }

        // GET: Degrees/Create
        public IActionResult Create(int employeeId)
        {
            var numberOfDegrees = _bl.GetNumberOfDegrees(employeeId);
            if (numberOfDegrees >= MaxNumberOfDegrees)
            {
                TempData["ErrorMessage"] = "Number of degrees is max.";
                return RedirectToAction(nameof(Index), new { employeeId });
            }
            DropDownList(employeeId);
            return View();
        }

        // POST: Degrees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int employeeId, DegreeEditModel degreeEditModel)
        {
            if (!ModelState.IsValid)
            {
                DropDownList(employeeId);
                return View(degreeEditModel);
            }

            var isSucessCreate = await _bl.AddDegree(degreeEditModel);
            if(!isSucessCreate)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the degree.";
                return View(degreeEditModel);
            }
            TempData["SuccessMessage"] = "Degree created successfully";
            return RedirectToAction(nameof(Index), new { employeeId });
        }

        // GET: Degrees/Edit/5
        public async Task<IActionResult> Edit(int? id, int employeeId)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var degree = await _bl.GetDegreeById(id);
            if (degree == null)
            {
                return NotFound();
            }

            var degreeEditModel = _bl.SetDegreeEditModel(degree);
            DropDownList(employeeId);
            return View(degreeEditModel);
        }

        // POST: Degrees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int employeeId, DegreeEditModel degreeEditModel)
        {
            if (id != degreeEditModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                DropDownList(employeeId);
                return View(degreeEditModel);
            }

            var isSucessEdit = await _bl.EditDegree(degreeEditModel);
            if(!isSucessEdit)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the degree.";
                return View(degreeEditModel);
            }
            TempData["SuccessMessage"] = "Degree updated successfully";
            return RedirectToAction(nameof(Index), new { employeeId = degreeEditModel.EmployeeId });
        }

        // GET: Degrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var degree = await _bl.GetDegreeById(id);

            if (degree == null)
            {
                return NotFound();
            }

            var degreeViewModel = _bl.SetDegreeViewModelDelete(degree);
            return View(degreeViewModel);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degree = await _bl.GetDegreeById(id);
            if (degree != null)
            {
                var isSucessDelete = await _bl.DeleteDegree(degree);
                if (!isSucessDelete)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the degree.";
                    return RedirectToAction(nameof(Index), new { employeeId = degree!.EmployeeId });
                }
            }
            TempData["SuccessMessage"] = "Degree deleted successfully";
            return RedirectToAction(nameof(Index), new { employeeId = degree!.EmployeeId });
        }

        public void DropDownList(int employeeId)
        {
            ViewData["EmployeeId"] = employeeId;
            ViewData["ProvinceId"] = new SelectList(_service.GetProvinceDbset(), "Id", "Name");
        }

        public async Task ViewEmployeeData(int employeeId)
        {
            var employee = await _bl.GetEmployeeById(employeeId);
            ViewData["employeeId"] = employeeId;
            ViewData["employeeName"] = employee!.Name;
            ViewData["numberOfDegrees"] = employee.Degrees!.Count;
        }
    }
}
