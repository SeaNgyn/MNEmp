using Microsoft.EntityFrameworkCore.Query;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IDistrictService
    {
        IQueryable<DistrictViewModel> GetPagedDistrictViewModels(int pageIndex, int pageSize,string searchString);
        Task<District?> GetDistrictById(int? id);
        IBaseService<District> BaseService();
        List<District> GetDistrictsByProvince(int provinceId);
        int Count(string searchString);
    }
}
