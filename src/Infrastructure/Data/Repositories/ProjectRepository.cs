using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<string, Func<List<string>, IQueryable<Project>, IQueryable<Project>>> _searchOperation;
        
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _searchOperation = new Dictionary<string, Func<List<string>, IQueryable<Project>, IQueryable<Project>>>()
            {
                { "projectName", SearchByProjectNameField },
                { "priority", SearchByPriorityField },
                { "startDateTime", SearchByStartDateTimeField },
                { "endDateTime", SearchByEndDateTimeField },
            };
        }

        public Task<List<Project>> GetListAsync()
        {
            return 
                _dbContext.Set<Project>()
                    .Include(p => p.ProjectManager)
                    .Include(p => p.Performers)
                        .ThenInclude(e => e.Employer)
                    .ToListAsync()
                ;
        }

        public Task<Project> GetByIdAsync(int id, bool detachIt = false)
        {
            var project =
                _dbContext.Set<Project>()
                    .Where(p => p.Id == id)
                    .Include(p => p.ProjectManager)
                    .Include(p => p.Performers)
                        .ThenInclude(e => e.Employer)
                    .SingleOrDefaultAsync()
                ;
            
            if (detachIt) {
                _dbContext.Entry(project.Result).State = EntityState.Detached;
            }

            return project;
        }

        public List<Project> Search(Dictionary<string, List<string>> searchDictionary)
        {
            var context = _dbContext.Set<Project>().AsQueryable();
            
            foreach (var query in searchDictionary)
            {
                context = _searchOperation[query.Key](query.Value, context);
            }

            return 
                context
                    .Include(p => p.ProjectManager)
                    .Include(p => p.Performers)
                        .ThenInclude(e => e.Employer)
                    .ToList();
        }

        private IQueryable<Project> SearchByPriorityField(List<string> values, IQueryable<Project> dbSet)
        {
            var predicate = PredicateBuilder.New<Project>();
            
            foreach (var value in values)
            {
                if (int.TryParse(value, out int intValue))
                {
                    predicate = predicate.Or(p => p.Priority.Equals(intValue));
                }
            }

            return dbSet.Where(predicate);
        }
        
        private IQueryable<Project> SearchByProjectNameField(List<string> values, IQueryable<Project> dbSet)
        {
            var predicate = PredicateBuilder.New<Project>();
            
            foreach (var value in values)
            {
                predicate = predicate.Or(p => p.ProjectName.Contains(value));
            }

            return dbSet.Where(predicate);
        }
        
        private IQueryable<Project> SearchByStartDateTimeField(List<string> values, IQueryable<Project> dbSet)
        {
            var predicate = PredicateBuilder.New<Project>();
            
            foreach (var value in values)
            {
                predicate = predicate.Or(p => p.StartDateTime <= Convert.ToDateTime(value));
            }

            return dbSet.Where(predicate);
        }
        
        private IQueryable<Project> SearchByEndDateTimeField(List<string> values, IQueryable<Project> dbSet)
        {
            var predicate = PredicateBuilder.New<Project>();
            
            foreach (var value in values)
            {
                predicate = predicate.Or(p => p.EndDateTime >= Convert.ToDateTime(value));
            }

            return dbSet.Where(predicate);
        }
    }
}