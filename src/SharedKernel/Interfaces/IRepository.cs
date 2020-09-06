using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
    public interface IRepository<TEntity> where TEntity: BaseEntity
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public void Update(TEntity entity);
        public Task RemoveAsync(TEntity entity);
        public Task RemoveRangeAsync(List<TEntity> entities);
        public void RemoveRange(List<TEntity> entities);
        public List<TEntity> AddRange(List<TEntity> entities);
        public TEntity Add(TEntity entity);
    }
}