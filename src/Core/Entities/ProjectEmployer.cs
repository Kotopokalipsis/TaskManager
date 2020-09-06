using SharedKernel;

namespace Core.Entities
{
    public class ProjectEmployer : BaseEntity
    {
        public Project Project { get; set; }
        public Employer Employer { get; set; }
    }
}