using Microsoft.EntityFrameworkCore;
using WebFormL1.DataAccess.Data;
using WebFormL1.Interface;

namespace WebFormL1.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Count()
        {
            return GetDbSet().Count();
        }
        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        public void Insert(T entity)
        {
            _context.Add(entity);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
