using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.BusinessLogic
{
    public class ProvinceBL : IProvinceBL
    {
        private readonly IProvinceService _service;
        public ProvinceBL(IProvinceService service)
        {
            _service = service;
        }
        public Paging<ProvinceViewModel> FetchProvincePage(int pageIndex, int pageSize,string searchString)
        {
            int totalPages =  _service.Count(searchString);
            var provinceViewModel = _service.GetPagedProvinceViewModels(pageIndex, pageSize, searchString).ToList();

            return new Paging<ProvinceViewModel>(totalPages, provinceViewModel);
        }

        public async Task<bool> AddProvince(ProvinceEditModel provinceEditModel)
        {
            try
            {
                var province = new Province(provinceEditModel.Id, provinceEditModel.Name,provinceEditModel.Level);

                _service.BaseService().Insert(province);
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

        public async Task<bool> EditProvince(ProvinceEditModel provinceEditModel)
        {
            try
            {
                var province = new Province(provinceEditModel.Id, provinceEditModel.Name,provinceEditModel.Level);

                _service.BaseService().Update(province);
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

        public async Task<bool> DeleteProvince(Province province)
        {
            try
            {
                _service.BaseService().Delete(province);
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
        public async Task<Province?> GetProvinceById(int? id)
        {
            return await _service.GetProvinceById(id);
        }
    }
}

