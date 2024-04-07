using WebFormL1.EditModel;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IProvinceBL
    {
        Paging<ProvinceViewModel> FetchProvincePage(int pageIndex, int pageSize,string searchString);
        Task<bool> AddProvince(ProvinceEditModel provinceEditModel);
        Task<bool> EditProvince(ProvinceEditModel provinceEditModel);
        Task<bool> DeleteProvince(Province province);
        Task<Province?> GetProvinceById(int? id);
    }
}
