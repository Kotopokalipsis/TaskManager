using Core.Entities;

namespace Tests.Data.Fixtures
{
    public class ProjectEmployerFixture
    {
        public static ProjectEmployer GetProjectEmployerInstance(Project project, Employer employer)
        {
            var projectEmployer = new ProjectEmployer();
            
            projectEmployer.Employer = employer;
            projectEmployer.Project = project;

            return projectEmployer;
        }
    }
}