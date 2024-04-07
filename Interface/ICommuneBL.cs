using WebFormL1.EditModel;
using WebFormL1.Models;
using WebFormL1.Utility;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface ICommuneBL
    {
        Paging<CommuneViewModel> FetchCommunePage(int pageIndex, int pageSize,string searchString);
        Task<bool> AddCommune(CommuneEditModel communeEditModel);
        Task<bool> EditCommune(CommuneEditModel communeEditModel);
        Task<bool> DeleteCommune(Commune commune);
        Task<Commune?> GetCommuneById(int? id);
        List<Commune> GetCommuneListByDistrict(int districtId);
    }
}
