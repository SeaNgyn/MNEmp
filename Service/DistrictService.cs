using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Service
{
    public class DistrictService : IDistrictService
    {
        private readonly ApplicationDbContext _context;
        public DistrictService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<District> GetDistrictsBySearchString(string searchString)
        {
            return _context.Districts.Include(d => d.Province).Where(d => d.Name!.ToLower().Contains(searchString.Trim().ToLower()) ||
                                                d.Province!.Name!.ToLower().Contains(searchString.Trim().ToLower()));
        }
        public IQueryable<DistrictViewModel> GetPagedDistrictViewModels( int pageIndex, int pageSize,string searchString)
        {
            return GetDistrictsBySearchString(searchString).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(district => new DistrictViewModel(district.Id, district.Name, district.Province!.Name,district.Level));
        }
        public async Task<District?> GetDistrictById(int? id)
        {
            return await _context.Districts
                    .Include(d => d.Province)
                    .FirstOrDefaultAsync(d => d.Id == id);
        }
        public IBaseService<District> BaseService()
        {
            return new BaseService<District>(_context);
        }

        public List<District> GetDistrictsByProvince(int provinceId)
        {
            return _context.Districts.Where( d=> d.ProvinceId == provinceId).ToList();   
        }

        public int Count(string searchString)
        {
            return GetDistrictsBySearchString(searchString).Count();
        }
    }
}

