using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTO.Employees;
using Core.DTO.Tasks;
using Core.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Resources.Employees;
using Web.Resources.Tasks;
using Task = Core.Entities.Task;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployerRepository _employerRepository;

        public TasksController(
            IMapper mapper,
            ITaskRepository taskRepository,
            IEmployerRepository employerRepository
        )
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _employerRepository = employerRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] TaskDTO taskDto)
        {
            var task = _mapper.Map<TaskDTO, Task>(taskDto);
            
            Employer author = null;
            if (taskDto.Author != null)
                author = await _employerRepository.GetByIdAsync(taskDto.Author.Id);

            Employer performer = null;
            if (taskDto.Performer != null)
                performer = await _employerRepository.GetByIdAsync(taskDto.Performer.Id);
            
            task.Author = author;
            task.Performer = performer;

            await _taskRepository.AddAsync(task);

            var taskResource = _mapper.Map<Task, TaskResource>(task);
            return Json(taskResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var tasks = await _taskRepository.GetListAsync();
            var mappedTaskResources = _mapper.Map<List<Task>, List<TaskResource>>(tasks);
            
            return Json(mappedTaskResources);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return NotFound();
            
            var mappedTaskResource = _mapper.Map<Task, TaskResource>(task);
            
            return Json(mappedTaskResource);
        }
        
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] Dictionary<string, List<string>> query)
        {
            var tasks = new List<Task>();
            
            try
            {
                tasks = _taskRepository.Search(query);
            }
            catch (KeyNotFoundException e)
            {
                return NoContent();
            }

            var mappedTaskResources = _mapper.Map<List<Task>, List<TaskResource>>(tasks);

            return Json(mappedTaskResources);
        }
        
        [HttpPatch]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, [FromBody] TaskDTO taskDto)
        {
            var sourceTask = await _taskRepository.GetByIdAsync(id, true);
            if (sourceTask == null) return NotFound();

            sourceTask = _mapper.Map(taskDto, sourceTask);
            
            Employer author = null;
            if (taskDto.Author != null)
                author = await _employerRepository.GetByIdAsync(taskDto.Author.Id);

            Employer performer = null;
            if (taskDto.Performer != null)
                performer = await _employerRepository.GetByIdAsync(taskDto.Performer.Id);
            
            sourceTask.Author = author;
            sourceTask.Performer = performer;
            
            await _taskRepository.UpdateAsync(sourceTask);
            
            return Json(Ok());
        }
     
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return NotFound();
        
            await _taskRepository.RemoveAsync(task);
            
            return Json(Ok());
        }
    }
}
