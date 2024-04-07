using Microsoft.EntityFrameworkCore.Query;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface ICommuneService
    {
        IQueryable<CommuneViewModel> GetPagedCommuneViewModels(int pageIndex, int pageSize,string searchString);
        Task<Commune?> GetCommuneById(int? id);
        IBaseService<Commune> BaseService();
        List<Commune> GetCommuneByDistrict(int districtId);
        int Count(string searchString);
    }
}
