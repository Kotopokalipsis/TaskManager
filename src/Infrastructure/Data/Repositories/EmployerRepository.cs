using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Data.Repositories
{
    public class EmployerRepository : BaseRepository<Employer>, IEmployerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public EmployerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Employer>> GetListAsync()
        {
            return 
                _dbContext.Set<Employer>()
                    .Include(e => e.Projects)
                        .ThenInclude(p => p.Project)
                    .ToListAsync()
                ;
        }

        public Task<List<Employer>> Search(string query)
        {
            return 
                _dbContext.Set<Employer>()
                    .Include(e => e.Projects)
                        .ThenInclude(p => p.Project)
                    .Where(e => 
                        e.FirstName.Contains(query) ||
                        e.LastName.Contains(query) ||
                        e.PatronymicName.Contains(query))
                    .ToListAsync()
                ;
        }

        public Task<Employer> GetByIdAsync(int id, bool detachIt = false)
        {
            var employer =
                _dbContext.Set<Employer>()
                    .Where(e => e.Id == id)
                    .Include(e => e.TasksAuthor)
                    .Include(e => e.TasksPerformer)
                    .Include(e => e.Projects)
                        .ThenInclude(p => p.Project)
                    .SingleOrDefaultAsync()
                ;
            
            if (detachIt) {
                _dbContext.Entry(employer.Result).State = EntityState.Detached;
            }
            
            return employer;
        }
        
        public Employer GetById(int id)
        {
            return 
                _dbContext.Set<Employer>()
                    .SingleOrDefault(e => e.Id == id);
        }
    }
}