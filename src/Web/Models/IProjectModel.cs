using System.Collections.Generic;
using Core.DTO.Employees;
using Core.Entities;

namespace Web.Models
{
    public interface IProjectModel
    {
        public void UpdatePerformersByListEmployerIdDto(Project project, IEnumerable<EmployerIdDTO> employerIdDtos);
    }
}