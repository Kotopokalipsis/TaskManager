﻿using System.Linq;
using AutoMapper;
using Core.DTO.Projects;
using Core.Entities;
using Web.Resources.Employees;
using Web.Resources.Projects;

namespace Web.Configuration.Automapper.Profiles 
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDTO, Project>()
                .ForMember(
                dest=> dest.StartDateTime,
                opt => opt.MapFrom(src => src.StartEndDate["start"]))
                .ForMember(
                    dest=> dest.EndDateTime,
                    opt => opt.MapFrom(src => src.StartEndDate["end"]))
                .ForMember(
                    dest=> dest.ProjectManager,
                    opt => opt.Ignore())
                .ForMember(
                    dest=> dest.Performers,
                    opt => opt.Ignore())
            ;
            
            CreateMap<Project, ProjectResource>()
                .ForMember(
                    dest => dest.ProjectManager,
                    opt => opt.MapFrom(src => src.ProjectManager))
                .ForMember(
                    dest => dest.Performers,
                    opt => opt.MapFrom(src => src.Performers.Select(pe => pe.Employer).ToList()))
            ;
        }
    }
}