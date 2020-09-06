﻿using System.Collections.Generic;
using Core.Entities;
using Web.Resources.Projects;

namespace Web.Resources.Employees
{
    public class EmployerResource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string Email { get; set; }
        
        public List<ProjectResource> Projects { get; set; }
    }
}