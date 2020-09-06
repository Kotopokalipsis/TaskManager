using System.Collections.Generic;
using System.Threading.Tasks;
using SharedKernel.Interfaces;
using TaskEntity = Core.Entities.Task;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        public Task<List<TaskEntity>> GetListAsync();
        public Task<TaskEntity> GetByIdAsync(int id, bool detachIt = false);
        public List<TaskEntity> Search(Dictionary<string, List<string>> searchDictionary);
    }
}