using System.Collections.Generic;
using Core.DTO.Employees;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;

namespace Web.Models
{
    public class ProjectModel : IProjectModel
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IProjectEmployerRepository _projectEmployerRepository;

        public ProjectModel(
            IProjectRepository projectRepository,
            IEmployerRepository employerRepository,
            IProjectEmployerRepository projectEmployerRepository
        )
        {
            _projectRepository = projectRepository;
            _employerRepository = employerRepository;
            _projectEmployerRepository = projectEmployerRepository;
        }
        
        public void UpdatePerformersByListEmployerIdDto(Project project, IEnumerable<EmployerIdDTO> employerIdDtos)
        {
            var performers = new List<ProjectEmployer>();

            foreach (var performerId in employerIdDtos)
            {
                var projectEmployer = new ProjectEmployer();
                
                projectEmployer.Employer = _employerRepository.GetById(performerId.Id);
                projectEmployer.Project = project;
                
                performers.Add(projectEmployer);
            }
            
            project.Performers = performers;
        }
    }
}