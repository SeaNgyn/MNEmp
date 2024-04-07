using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFormL1.BusinessLogic;
using WebFormL1.EditModel;
using WebFormL1.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using WebFormL1.Constant;
using WebFormL1.Interface;
using WebFormL1.Utility;

namespace WebFormL1.Controllers
{
    public class ProvincesController : BaseController
    {
        private readonly IProvinceBL _bl;

        public ProvincesController(IProvinceBL bl)
        {
            _bl = bl;
        }


        // GET: Provinces
        public IActionResult Index(int? page,int? size,string searchString = "")
        {
            int pageIndex = Validate.ValidatePageNumber(ref page);
            int pageSize = Validate.ValidatePageSize(ref size);
            ViewBag.size = GetPageSizeOptions(size);
            ViewBag.currentSize = size;
            var provincesViewModel = _bl.FetchProvincePage(pageIndex, pageSize, searchString);
            ViewData["searchString"] = searchString;
            return View(new StaticPagedList<ProvinceViewModel>(provincesViewModel.ViewModel!,
                                     pageIndex, pageSize, provincesViewModel.TotalPages));
        }
        // GET: Provinces/Create
        public IActionResult Create()
        {
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Thành phố trung ương", Text = "Thành phố trung ương" },
                new SelectListItem { Value = "Tỉnh", Text = "Tỉnh" }
            };
            ViewBag.Levels = levels;
            return View();
        }

        // POST: Provinces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level")] ProvinceEditModel provinceEditModel)
        {
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Thành phố trung ương", Text = "Thành phố trung ương" },
                new SelectListItem { Value = "Tỉnh", Text = "Tỉnh" }
            };
            if (!ModelState.IsValid)
            {
                ViewBag.Levels = levels;
                return View(provinceEditModel);
            }

            var isSucessCreating = await _bl.AddProvince(provinceEditModel);
            if(!isSucessCreating)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the province.";
                return View(provinceEditModel);
            }    


            TempData["SuccessMessage"] = "Province created successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var province = await _bl.GetProvinceById(id);

            if (province == null)
            {
                return NotFound();
            }
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Thành phố trung ương", Text = "Thành phố trung ương" },
                new SelectListItem { Value = "Tỉnh", Text = "Tỉnh" }
            };
            ViewBag.Levels = levels;
            var provinceEditModel = new ProvinceEditModel(province.Id, province.Name, province.Level);
            return View(provinceEditModel);
        }

        // POST: Provinces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProvinceEditModel provinceEditModel)
        {
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Thành phố trung ương", Text = "Thành phố trung ương" },
                new SelectListItem { Value = "Tỉnh", Text = "Tỉnh" }
            }; 
            if (id != provinceEditModel.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Levels = levels;
                return View(provinceEditModel);
            }

            var isSucessEdit = await _bl.EditProvince(provinceEditModel);
            if(!isSucessEdit)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the province.";
                return View(provinceEditModel);
            }    
            TempData["SuccessMessage"] = "Province created successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var province = await _bl.GetProvinceById(id);

            if (province == null)
            {
                return NotFound();
            }

            var provinceViewModel = new ProvinceViewModel(province.Id, province.Name, province.Level);
            return View(provinceViewModel);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _bl.GetProvinceById(id);
            if (province == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isSucessDelete = await _bl.DeleteProvince(province);
            if(!isSucessDelete)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the province.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Province deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
