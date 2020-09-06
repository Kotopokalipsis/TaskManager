using System;
using System.Collections.Generic;
using Web.Resources.Employees;

namespace Web.Resources.Projects
{
    public class ProjectResource
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string CustomerCompany { get; set; }
        public string PerformerCompany { get; set; }
        public int Priority { get; set; }
        
        public EmployerResource ProjectManager { get; set; }
        public List<EmployerResource> Performers { get; set; }
        
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}