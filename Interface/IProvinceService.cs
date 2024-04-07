using Microsoft.EntityFrameworkCore;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IProvinceService
    {
        IQueryable<ProvinceViewModel> GetPagedProvinceViewModels( int pageIndex, int pageSize,string searchString);
        Task<Province?> GetProvinceById(int? id);
        int Count(string searchString);
        IBaseService<Province> BaseService();
    }
}
