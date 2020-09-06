using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using TaskEntity = Core.Entities.Task;

namespace Infrastructure.Data.Repositories
{
    public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<string, Func<List<string>, IQueryable<TaskEntity>, IQueryable<TaskEntity>>> _searchOperation;
        
        public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _searchOperation = new Dictionary<string, Func<List<string>, IQueryable<TaskEntity>, IQueryable<TaskEntity>>>()
            {
                { "taskName", SearchByTaskNameField },
                { "status", SearchByStatusStateField },
            };
        }

        public Task<List<TaskEntity>> GetListAsync()
        {
            return 
                _dbContext.Set<TaskEntity>()
                    .Include(e => e.Author)
                    .Include(e => e.Performer)
                    .ToListAsync()
                ;
        }
        
        public Task<TaskEntity> GetByIdAsync(int id, bool detachIt = false)
        {
            var task =
                    _dbContext.Set<TaskEntity>()
                        .Where(e => e.Id == id)
                        .Include(e => e.Author)
                        .Include(e => e.Performer)
                        .SingleOrDefaultAsync()
                ;
            
            if (detachIt) {
                _dbContext.Entry(task.Result).State = EntityState.Detached;
            }
            
            return task;
        }
        
        public List<TaskEntity> Search(Dictionary<string, List<string>> searchDictionary)
        {
            var context = _dbContext.Set<TaskEntity>().AsQueryable();
            
            foreach (var query in searchDictionary)
            {
                context = _searchOperation[query.Key](query.Value, context);
            }

            return 
                context
                    .Include(p => p.Author)
                    .Include(p => p.Performer)
                    .ToList();
        }

        private IQueryable<TaskEntity> SearchByTaskNameField(List<string> values, IQueryable<TaskEntity> dbSet)
        {
            var predicate = PredicateBuilder.New<TaskEntity>();
            
            foreach (var value in values)
            {
                predicate = predicate.Or(p => p.Name.Contains(value));
            }

            return dbSet.Where(predicate);
        }
        
        private IQueryable<TaskEntity> SearchByStatusStateField(List<string> values, IQueryable<TaskEntity> dbSet)
        {
            var predicate = PredicateBuilder.New<TaskEntity>();

            foreach (var value in values)
            {
                if (int.TryParse(value, out int intValue))
                {
                    predicate = predicate.Or(p => p.Status == (TaskEntity.StatusStates)intValue);
                }
            }

            return dbSet.Where(predicate);
        }
    }
}