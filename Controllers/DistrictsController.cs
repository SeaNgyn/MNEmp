using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFormL1.BusinessLogic;
using WebFormL1.EditModel;
using WebFormL1.ViewModel;
using WebFormL1.Service;
using X.PagedList;
using WebFormL1.Interface;
using WebFormL1.Constant;
using WebFormL1.Utility;

namespace WebFormL1.Controllers
{
    public class DistrictsController : BaseController
    {
        private readonly RepositoryService _repos;

        private readonly IDistrictBL _bl;

        public DistrictsController(IDistrictBL bl, RepositoryService repos)
        {
            _bl = bl;
            _repos = repos;
        }

        // GET: Districts
        public IActionResult Index(int? page,int? size,string searchString = "")
        {
            int pageIndex = Validate.ValidatePageNumber(ref page);
            int pageSize = Validate.ValidatePageSize(ref size);
            ViewBag.size = GetPageSizeOptions(size);
            ViewBag.currentSize = size;
            var districtsViewModel = _bl.FetchDistrictPage(pageIndex, pageSize,searchString);
            ViewData["searchString"] = searchString;

            return View(new StaticPagedList<DistrictViewModel>(districtsViewModel.ViewModel!,
                                     pageIndex, pageSize, districtsViewModel.TotalPages));
        }


        // GET: Districts/Create
        public IActionResult Create()
        {
            DropDownList();
            return View();
        }

        // POST: Districts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level,ProvinceId")] DistrictEditModel districtEditModel)
        {
            if (!ModelState.IsValid)
            {
                DropDownList();
                return View(districtEditModel);
            }

            var isSuccessCreate = await _bl.AddDistrict(districtEditModel);
            if(!isSuccessCreate)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the districts.";
                return View(districtEditModel);
            }
            TempData["SuccessMessage"] = "District created successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var district = await _bl.GetDistrictById(id);

            if (district == null)
            {
                return NotFound();
            }

            var districtEditModel = new DistrictEditModel(district.Id, district.Name, district.Level, district.ProvinceId);
            DropDownList();

            return View(districtEditModel);

        }

        // POST: Districts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DistrictEditModel districtEditModel)
        {
            if (id != districtEditModel.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                DropDownList();
                return View(districtEditModel);
            }
            var isSuccessEdit = await _bl.EditDistrict(districtEditModel);
            if(!isSuccessEdit) 
            {
                TempData["ErrorMessage"] = "An error occurred while updating the district.";
                return View(districtEditModel);
            }
            TempData["SuccessMessage"] = "District updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var district = await _bl.GetDistrictById(id);

            if (district == null)
            {
                return NotFound();
            }

            var districtViewModel = new DistrictViewModel(district.Id, district.Name, district.Province!.Name, district.Level);
            return View(districtViewModel);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var district = await _bl.GetDistrictById(id);
            if (district == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isSuccessDelete = await _bl.DeleteDistrict(district);
            if(!isSuccessDelete)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the district.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "District deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        public void DropDownList()
        {
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Huyện", Text = "Huyện" },
                new SelectListItem { Value = "Thành Phố", Text = "Thành Phố" },
                new SelectListItem { Value = "Quận", Text = "Quận" }
            };
            ViewBag.Levels = levels;
            ViewData["ProvinceId"] = new SelectList(_repos.GetProvinceDbset(), "Id", "Name");
        }
        public JsonResult GetDistrictByProvince(int provinceId)
        {
            return Json(_bl.GetDistrictListByProvince(provinceId));
        }
    }


}
