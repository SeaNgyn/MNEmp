using WebFormL1.EditModel;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IDistrictBL
    {
        Paging<DistrictViewModel> FetchDistrictPage(int pageIndex, int pageSize, string searchString);
        Task<bool> AddDistrict(DistrictEditModel districtEditModel);
        Task<bool> EditDistrict(DistrictEditModel districtEditModel);
        Task<bool> DeleteDistrict(District district);
        Task<District?> GetDistrictById(int? id);
        List<District> GetDistrictListByProvince(int provinceId);
    }
}
