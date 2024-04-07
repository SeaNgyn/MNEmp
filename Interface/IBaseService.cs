using Microsoft.EntityFrameworkCore;

namespace WebFormL1.Interface
{
        public interface IBaseService<T> where T : class
        {
            DbSet<T> GetDbSet();
            void Insert(T entity);
            void Update(T entity);
            void Delete(T entity);
            Task<int> SaveChangeAsync();
            int Count();
        }
}
