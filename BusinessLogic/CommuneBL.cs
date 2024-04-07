using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.BusinessLogic
{
    public class CommuneBL : ICommuneBL
    {
        private readonly ICommuneService _service;
        public CommuneBL(ICommuneService service)
        {
            _service = service;
        }
        public Paging<CommuneViewModel> FetchCommunePage(int pageIndex, int pageSize,string searchString)
        {
            int totalPages = _service.Count(searchString);
            var communesViewModel = _service.GetPagedCommuneViewModels(pageIndex, pageSize,searchString).ToList();

            return new Paging<CommuneViewModel>(totalPages, communesViewModel);
        }
        public async Task<bool> AddCommune(CommuneEditModel communeEditModel)
        {
            try
            {
                var commune = new Commune(communeEditModel.Id, communeEditModel.Name!, communeEditModel.Level!, communeEditModel.DistrictId);

                _service.BaseService().Insert(commune);
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
        public async Task<bool> EditCommune(CommuneEditModel communeEditModel)
        {
            try
            {
                var commune = new Commune(communeEditModel.Id, communeEditModel.Name!, communeEditModel.Level!, communeEditModel.DistrictId);

                _service.BaseService().Update(commune);
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
        public async Task<bool> DeleteCommune(Commune commune)
        {
            try
            {
                _service.BaseService().Delete(commune);
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
        public async Task<Commune?> GetCommuneById(int? id)
        {
            return await _service.GetCommuneById(id);
        }
        public List<Commune> GetCommuneListByDistrict(int districtId)
        {
            return _service.GetCommuneByDistrict(districtId);
        }
    }
}
