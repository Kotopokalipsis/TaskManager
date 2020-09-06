using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class ProjectEmployerRepository : BaseRepository<ProjectEmployer>, IProjectEmployerRepository
    {
        public ProjectEmployerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}