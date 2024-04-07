using Microsoft.EntityFrameworkCore;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Service
{
    public class ProvinceService : IProvinceService
    {
        private readonly ApplicationDbContext _context;
        public ProvinceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Province> GetBySearchString(string searchString) 
        {
            return _context.Provinces.Where(p => p.Name!.ToLower().Contains(searchString.Trim().ToLower()));
        }
        public IQueryable<ProvinceViewModel> GetPagedProvinceViewModels(int pageIndex, int pageSize, string searchString)
        {
            return GetBySearchString(searchString).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(province => new ProvinceViewModel(province.Id, province.Name, province.Level));
        }
        public async Task<Province?> GetProvinceById(int? id)
        {
            return await _context.Provinces.FirstOrDefaultAsync(p => p.Id == id);
        }
        public IBaseService<Province> BaseService()
        {
            return new BaseService<Province>(_context);
        }
        public int Count(string searchString)
        {
            return GetBySearchString(searchString).Count();
        }
    }
}

