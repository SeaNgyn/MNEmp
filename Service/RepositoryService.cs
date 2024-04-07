using Microsoft.EntityFrameworkCore;
using WebFormL1.DataAccess.Data;
using WebFormL1.Models;

namespace WebFormL1.Service
{
    public class RepositoryService
    {
        readonly private ApplicationDbContext _context;
        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public DbSet<Province> GetProvinceDbset()
        {
            return _context.Provinces;
        }
        public DbSet<District> GetDistrictDbset()
        {
            return _context.Districts;
        }
        public DbSet<Commune> GetCommuneSet()
        {
            return _context.Communes;
        }
        public DbSet<Job> GetJobDbset()
        {
            return _context.Jobs;
        }
        public DbSet<Ethnic> GetEthnicDbset()
        {
            return _context.Ethnices;
        }
    }
}
