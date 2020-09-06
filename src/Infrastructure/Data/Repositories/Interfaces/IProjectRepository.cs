using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using SharedKernel.Interfaces;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        public Task<List<Project>> GetListAsync();
        public Task<Project> GetByIdAsync(int id, bool detachIt = false);

        public List<Project> Search(Dictionary<string, List<string>> searchQuery);
    }
}