using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Service;
using WebFormL1.Utility;
using WebFormL1.ViewModel;
using X.PagedList;

namespace WebFormL1.Controllers
{
    public class CommunesController : BaseController
    {
        private readonly RepositoryService _repos;

        private readonly ICommuneBL _bl;

        public CommunesController(ICommuneBL bl, RepositoryService repos)
        {
            _bl = bl;
            _repos = repos;
        }

        // GET: Districts
        public IActionResult Index(int? page,int ?size,string searchString = "")
        {
            int pageIndex = Validate.ValidatePageNumber(ref page);
            int pageSize = Validate.ValidatePageSize(ref size);
            ViewBag.size = GetPageSizeOptions(size);
            ViewBag.currentSize = size;
            var communesViewModel = _bl.FetchCommunePage(pageIndex, pageSize, searchString);
            ViewData["searchString"] = searchString;

            return View(new StaticPagedList<CommuneViewModel>(communesViewModel.ViewModel!,
                                     pageIndex, pageSize, communesViewModel.TotalPages));
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
        public async Task<IActionResult> Create(CommuneEditModel communeEditModel)
        {
            if (!ModelState.IsValid)
            {
                DropDownList();
                return View(communeEditModel);
            }

            var isSucessCreate = await _bl.AddCommune(communeEditModel);
            if(!isSucessCreate)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the communes.";
                return View(communeEditModel);
            }
            TempData["SuccessMessage"] = "Commune created successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var commune = await _bl.GetCommuneById(id);
            if (commune == null)
            {
                return NotFound();
            }

            var communeEditModel = new CommuneEditModel(commune!.Id, commune.Name!, commune.Level, commune.DistrictId,commune.District!.ProvinceId);
            DropDownList();

            return View(communeEditModel);

        }

        // POST: Districts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommuneEditModel communeEditModel)
        {
            if (id != communeEditModel.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                DropDownList();
                return View(communeEditModel);
            }
            var isSucessEdit = await _bl.EditCommune(communeEditModel);
            if(!isSucessEdit)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the communes.";
                return View(communeEditModel);
            }
            TempData["SuccessMessage"] = "Commune updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var commune = await _bl.GetCommuneById(id);

            if (commune == null)
            {
                return NotFound();
            }

            var communeViewModel = new CommuneViewModel(commune.Id, commune.Name!, commune.District!.Name!,commune.District.Province!.Name, commune.Level);
            return View(communeViewModel);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commune = await _bl.GetCommuneById(id);
            if (commune == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isSucessDelete = await _bl.DeleteCommune(commune);
            if(!isSucessDelete)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the communes.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Commune deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        public void DropDownList()
        {
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Phường", Text = "Phường" },
                new SelectListItem { Value = "Xã", Text = "Xã" },
                new SelectListItem { Value = "Thị trấn", Text = "Thị trấn" }
            };
            ViewBag.Levels = levels;
            ViewData["ProvinceId"] = new SelectList(_repos.GetProvinceDbset(), "Id", "Name");
        }
        public JsonResult GetCommuneByDistrict(int districtId)
        {
            return Json(_bl.GetCommuneListByDistrict(districtId));
        }
    }
}
