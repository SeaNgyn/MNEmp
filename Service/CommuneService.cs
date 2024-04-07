using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NPOI.OpenXmlFormats.Dml;
using WebFormL1.BusinessLogic;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Service
{
    public class CommuneService : ICommuneService
    {
        private readonly ApplicationDbContext _context;
        public CommuneService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IBaseService<Commune> BaseService()
        {
            return new BaseService<Commune>(_context);
        }

        public int Count(string searchString)
        {
            return GetCommunesBySearchString(searchString).Count();
        }

        public List<Commune> GetCommuneByDistrict(int districtId)
        {
            return _context.Communes.Where(d => d.DistrictId == districtId).ToList();
        }
        public async Task<Commune?> GetCommuneById(int? id)
        {
            return await _context.Communes.Include(w=>w.District)
                    .ThenInclude(d => d!.Province)
                    .FirstOrDefaultAsync(d => d.Id == id);
        }
        public IQueryable<Commune> GetCommunesBySearchString(string searchString)
        {
            return _context.Communes.Where(w => w.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                                w.District!.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                                w.District!.Province!.Name!.ToLower().Contains(searchString.Trim().ToLower()));
        }
        public IQueryable<CommuneViewModel> GetPagedCommuneViewModels(int pageIndex, int pageSize,string searchString)
        {
            return GetCommunesBySearchString(searchString).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(commune => new CommuneViewModel(commune.Id, commune.Name!, commune.District!.Name!,commune.District.Province!.Name, commune.Level));
        }
        
    }
}
