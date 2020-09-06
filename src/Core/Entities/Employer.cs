using System.Collections.Generic;
using SharedKernel;

namespace Core.Entities
{
    public class Employer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string Email { get; set; }
        
        public List<ProjectEmployer> Projects { get; set; }
        public List<Project> ProjectManagerOnProjects { get; set; }
        
        public List<Task> TasksAuthor { get; set; }
        public List<Task> TasksPerformer { get; set; }
    }
}