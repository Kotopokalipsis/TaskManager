using Core.DTO.Employees;
using Newtonsoft.Json;
using Web.Resources.Employees;

namespace Web.Resources.Tasks
{
    public class TaskResource
    {
        public int Id { get; set; }
        [JsonProperty("taskName")]
        public string Name { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }

        public EmployerResource Author { get; set; }
        
        public EmployerResource Performer { get; set; }
    }
}