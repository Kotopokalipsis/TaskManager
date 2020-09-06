using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Tests.Data;
using Tests.Data.Fixtures;
using Xunit;

namespace Tests
{
    public class ProjectRepositoryTest : IClassFixture<SharedDatabaseFixture>
    {
        public SharedDatabaseFixture Fixture { get; }

        public ProjectRepositoryTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
        } 
        
        [Fact]
        public void GetListAsyncTest()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    var projectListTask = context.Set<Project>()
                            .Include(p => p.ProjectManager)
                            .Include(p => p.Performers)
                                .ThenInclude(e => e.Employer)
                            .ToListAsync()
                        ;
                    
                    var projectList = projectListTask.Result;
                    
                    var expectedProjectsList = ProjectFixture.GetAllProjects();
                    
                    AssertData(expectedProjectsList, projectList);
                }
            }
        }
        
        private void AssertData(List<Project> expected, List<Project> actual)
        {
            if (expected.Count == 0) Assert.Empty(actual);
            else if (expected.Count == actual.Count)
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    var expectedProject = expected[i];
                    var projectResult = actual[i];

                    Assert.Equal(expectedProject.ProjectName, projectResult.ProjectName);
                    Assert.Equal(expectedProject.CustomerCompany, projectResult.CustomerCompany);
                    Assert.Equal(expectedProject.PerformerCompany, projectResult.PerformerCompany);
                    Assert.Equal(expectedProject.Priority, projectResult.Priority);

                    for (int j = 0; j < expectedProject.Performers.Count; j++)
                    {
                        var expectedProjectEmployer = expectedProject.Performers[j];
                        var projectEmployerResult = projectResult.Performers[j];

                        Assert.Equal(expectedProjectEmployer.Employer.FirstName, projectEmployerResult.Employer.FirstName);
                        Assert.Equal(expectedProjectEmployer.Project.ProjectName, projectEmployerResult.Project.ProjectName);
                    }
                }
            }
            else Assert.True(false, "ExpectedList.count not equal ActualList.count");
        }
    }
}