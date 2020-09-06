using System.ComponentModel.DataAnnotations;
using Core.DTO.Employees;
using Newtonsoft.Json;

namespace Core.DTO.Tasks
{
    public class TaskDTO
    {
        [Required]
        [JsonProperty("taskName")]
        public string Name { get; set; }
        [Required]
        [JsonProperty("status")]
        public int Status { get; set; }
        [Required]
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [Required]
        [JsonProperty("priority")]
        public int Priority { get; set; }
        
        [JsonProperty("author")]
        public EmployerIdDTO Author { get; set; }
        [JsonProperty("performer")]
        public EmployerIdDTO Performer { get; set; }
    }
}