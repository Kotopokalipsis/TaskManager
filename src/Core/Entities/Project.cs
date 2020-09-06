using System;
using System.Collections.Generic;
using SharedKernel;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string CustomerCompany { get; set; }
        public string PerformerCompany { get; set; }
        public int Priority { get; set; }
        
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        
        public int? ProjectManagerId { get; set; }
        public Employer ProjectManager { get; set; }
        public List<ProjectEmployer> Performers { get; set; }
    }
}