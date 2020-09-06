using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using SharedKernel.Interfaces;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        public Task<List<Employer>> GetListAsync();
        public Task<List<Employer>> Search(string query);
        public Task<Employer> GetByIdAsync(int id, bool detachIt = false);
        public Employer GetById(int id);
    }
}