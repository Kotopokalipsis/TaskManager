using AutoMapper;
using Core.DTO.Tasks;
using Core.Entities;
using Web.Resources.Projects;
using Web.Resources.Tasks;

namespace Web.Configuration.Automapper.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDTO, Task>();
            CreateMap<Task, TaskResource>();
        }
    }
}