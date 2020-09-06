using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.DTO.Employees;
using Newtonsoft.Json;

namespace Core.DTO.Projects
{
    public class ProjectDTO
    {
        private Dictionary<string, DateTime> _startEndDate;
        
        [Required]
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }
        
        [Required]
        [JsonProperty("customerCompany")]
        public string CustomerCompany { get; set; }
        
        [Required]
        [JsonProperty("performerCompany")]
        public string PerformerCompany { get; set; }

        [JsonProperty("projectManager")]
        public EmployerIdDTO ProjectManagerDtoId { get; set; }
        
        [JsonProperty("performers")]
        public List<EmployerIdDTO> PerformersIds { get; set; }
        
        [Required, Range(1, 3)]
        [JsonProperty("priority")]
        public int Priority { get; set; }
        
        [Required]
        public Dictionary<string, DateTime> StartEndDate
        {
            get => _startEndDate;
            set
            {
                if (value.ContainsKey("start") && value.ContainsKey("end"))
                {
                    _startEndDate = value;
                }
            }
        }
    }
}