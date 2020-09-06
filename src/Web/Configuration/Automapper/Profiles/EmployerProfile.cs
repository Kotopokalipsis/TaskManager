﻿using System.Linq;
using AutoMapper;
using Core.DTO.Employees;
 using Core.Entities;
using Web.Resources.Employees;

 namespace Web.Configuration.Automapper.Profiles
{
    public class EmployerProfile: Profile
    {
        public EmployerProfile()
        {
            CreateMap<EmployerDTO, Employer>();
            CreateMap<EmployerIdDTO, Employer>();
            CreateMap<Employer, EmployerResource>()
                .ForMember(
                    dest => dest.Projects,
                    opt => opt.MapFrom(src => src.Projects.Select(pe => pe.Project).ToList()));
            CreateMap<ProjectEmployer, EmployerResource>();
        }
    }
}