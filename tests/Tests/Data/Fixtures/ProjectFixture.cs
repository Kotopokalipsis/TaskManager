using System;
using System.Collections.Generic;
using Core.Entities;

namespace Tests.Data.Fixtures
{
    public class ProjectFixture
    {
        public static Project GetFirstProject()
        {
            var project = new Project();
            
            project.ProjectName = "Test Project Name";
            project.PerformerCompany = "Test Performer Company";
            project.CustomerCompany = "Test Customer Company";
            project.Priority = 1;
            
            project.ProjectManager = EmployerFixture.GetFirstEmployer();
            
            project.Performers = new List<ProjectEmployer>();
            project.Performers.Add(ProjectEmployerFixture.GetProjectEmployerInstance(project, EmployerFixture.GetFirstEmployer()));
            project.Performers.Add(ProjectEmployerFixture.GetProjectEmployerInstance(project, EmployerFixture.GetSecondEmployer()));

            project.StartDateTime = new DateTime(2020, 3, 10);
            project.EndDateTime = new DateTime(2020, 5, 10);
            
            return project;
        }
        
        public static Project GetSecondProject()
        {
            var project = new Project();
            
            project.ProjectName = "Second Project Name";
            project.PerformerCompany = "Second Performer Company";
            project.CustomerCompany = "Second Customer Company";
            project.Priority = 3;
            
            project.ProjectManager = EmployerFixture.GetSecondEmployer();
            
            project.Performers = new List<ProjectEmployer>();
            project.Performers.Add(ProjectEmployerFixture.GetProjectEmployerInstance(project, EmployerFixture.GetFirstEmployer()));

            project.StartDateTime = new DateTime(2019, 3, 10);
            project.EndDateTime = new DateTime(2020, 5, 10);
            
            return project;
        }

        public static List<Project> GetAllProjects()
        {
            var projects = new List<Project>();
            
            projects.Add(GetFirstProject());
            projects.Add(GetSecondProject());

            return projects;
        }
    }
}