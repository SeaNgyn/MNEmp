using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.BusinessLogic
{
    public class DistrictBL : IDistrictBL
    {
        private readonly IDistrictService _service;
        public DistrictBL(IDistrictService service)
        {
            _service = service;
        }
        public Paging<DistrictViewModel> FetchDistrictPage(int pageIndex, int pageSize,string searchString)
        {
            int totalPages = _service.Count(searchString);
            var districtsViewModel = _service.GetPagedDistrictViewModels( pageIndex, pageSize,searchString).ToList();

            return new Paging<DistrictViewModel>(totalPages, districtsViewModel);
        }
        public async Task<bool> AddDistrict(DistrictEditModel districtEditModel)
        {
            try
            {
                var district = new District(districtEditModel.Id, districtEditModel.Name,districtEditModel.Level, districtEditModel.ProvinceId);

                _service.BaseService().Insert(district);
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
        public async Task<bool> EditDistrict(DistrictEditModel districtEditModel)
        {
            try
            {
                var district = new District(districtEditModel.Id, districtEditModel.Name,districtEditModel.Level, districtEditModel.ProvinceId);

                _service.BaseService().Update(district);
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
        public async Task<bool> DeleteDistrict(District district)
        {
            try
            {
                _service.BaseService().Delete(district);
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
        public async Task<District?> GetDistrictById(int? id)
        {
            return await _service.GetDistrictById(id);
        }
        public List<District> GetDistrictListByProvince(int provinceId)
        {
            return _service.GetDistrictsByProvince(provinceId);
        }
    }
}

