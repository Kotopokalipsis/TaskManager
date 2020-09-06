using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO.Employees;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Resources.Employees;
using Task = Core.Entities.Task;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployerRepository _employerRepository;
        private readonly IProjectEmployerRepository _projectEmployerRepository;
        private readonly ITaskRepository _taskRepository;

        public EmployeesController(
            IMapper mapper,
            IEmployerRepository employerRepository,
            IProjectEmployerRepository projectEmployerRepository,
            ITaskRepository taskRepository
            )
        {
            _mapper = mapper;
            _employerRepository = employerRepository;
            _projectEmployerRepository = projectEmployerRepository;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] EmployerDTO employerDto)
        {
            var employer = _mapper.Map<EmployerDTO, Employer>(employerDto);
            await _employerRepository.AddAsync(employer);

            var employerResource = _mapper.Map<Employer, EmployerResource>(employer);
            return Json(employerResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var employees = await _employerRepository.GetListAsync();
            var mappedEmployerResources = _mapper.Map<List<Employer>, List<EmployerResource>>(employees);
            
            return Json(mappedEmployerResources);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employer = await _employerRepository.GetByIdAsync(id);
            if (employer == null) return NotFound();
            
            var mappedEmployerResource = _mapper.Map<Employer, EmployerResource>(employer);
            
            return Json(mappedEmployerResource);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var employees = await _employerRepository.Search(query);
            var mappedEmployerResources = _mapper.Map<List<Employer>, List<EmployerResource>>(employees);

            return Json(mappedEmployerResources);
        }
        
        [HttpPatch]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, [FromBody] EmployerDTO employerDto)
        {
            var sourceEmployer = await _employerRepository.GetByIdAsync(id, true);
            if (sourceEmployer == null) return NotFound();
            
            var entryEmployer = _mapper.Map<EmployerDTO, Employer>(employerDto);
            entryEmployer.Id = sourceEmployer.Id;
            entryEmployer.Projects = sourceEmployer.Projects;
            
            var resultEmployer = _mapper.Map(entryEmployer, sourceEmployer);

            await _employerRepository.UpdateAsync(resultEmployer);
            
            return Json(Ok());
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employer = await _employerRepository.GetByIdAsync(id);
            if (employer == null) return NotFound();
            
            await _projectEmployerRepository.RemoveRangeAsync(employer.Projects);
            await _employerRepository.RemoveAsync(employer);
            
            return Json(Ok());
        }
    }
}
