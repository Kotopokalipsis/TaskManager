using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO.Employees;
using Core.DTO.Projects;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Models;
using Web.Resources.Employees;
using Web.Resources.Projects;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IProjectEmployerRepository _projectEmployerRepository;
        private readonly IProjectModel _projectModel;

        public ProjectsController(
            IMapper mapper,
            IProjectRepository projectRepository,
            IEmployerRepository employerRepository,
            IProjectEmployerRepository projectEmployerRepository,
            IProjectModel projectModel
            )
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _employerRepository = employerRepository;
            _projectEmployerRepository = projectEmployerRepository;
            _projectModel = projectModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] ProjectDTO projectDto)
        {
            var project = _mapper.Map<ProjectDTO, Project>(projectDto);
            
            Employer projectManager = null;
            if (projectDto.ProjectManagerDtoId != null)
                projectManager = await _employerRepository.GetByIdAsync(projectDto.ProjectManagerDtoId.Id);

            project.ProjectManager = projectManager;
            
            List<EmployerIdDTO> performersIds = new List<EmployerIdDTO>();
            if (projectDto.PerformersIds.Count != 0)
                performersIds = projectDto.PerformersIds;
            
            _projectModel.UpdatePerformersByListEmployerIdDto(project, performersIds);

            await _projectRepository.AddAsync(project);
            
            var mappedProjectResource = _mapper.Map<Project, ProjectResource>(project);
            
            return Json(mappedProjectResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var projects = await _projectRepository.GetListAsync();
            var mappedProjectResource = _mapper.Map<List<Project>, List<ProjectResource>>(projects);
            
            return Json(mappedProjectResource);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            var mappedProjectResource = _mapper.Map<Project, ProjectResource>(project);
            
            return Json(mappedProjectResource);
        }
        
        [HttpPatch]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectDTO projectDto)
        {
            var sourceProject = await _projectRepository.GetByIdAsync(id, true);
            if (sourceProject == null) return NotFound();

            await _projectEmployerRepository.RemoveRangeAsync(sourceProject.Performers);

            sourceProject = _mapper.Map(projectDto, sourceProject);

            Employer projectManager = null;
            if (projectDto.ProjectManagerDtoId != null)
                projectManager = await _employerRepository.GetByIdAsync(projectDto.ProjectManagerDtoId.Id);
            
            _projectRepository.Update(sourceProject);
            
            var performersIds = new List<EmployerIdDTO>();
            if (projectDto.PerformersIds.Count != 0)
                performersIds = projectDto.PerformersIds;
            
            sourceProject.ProjectManager = projectManager;
            _projectModel.UpdatePerformersByListEmployerIdDto(sourceProject, performersIds);

            await _projectRepository.UpdateAsync(sourceProject);
            return Json(Ok());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            await _projectEmployerRepository.RemoveRangeAsync(project.Performers);
            await _projectRepository.RemoveAsync(project);
            
            return Json(Ok());
        }
        
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] Dictionary<string, List<string>> query)
        {
            var projects = new List<Project>();
            
            try
            {
                projects = _projectRepository.Search(query);
            }
            catch (KeyNotFoundException e)
            {
                return NoContent();
            }

            var mappedProjectResources = _mapper.Map<List<Project>, List<ProjectResource>>(projects);

            return Json(mappedProjectResources);
        }
    }
}
