using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        TEntity Find(string key);
        TEntity Find(int key);
    }
}