using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Find(string key)
        {
            var entity = _context.Set<TEntity>().Find(key);

            if(entity != null)
            {
                _context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                
                return entity;
            }
            else
            {
                throw null;
            }
        }

        public TEntity Find(int key)
        {
            var entity = _context.Set<TEntity>().Find(key);

            if(entity != null)
            {
                _context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                
                return entity;
            }
            else
            {
                throw null;
            }
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.AddAsync<TEntity>(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}