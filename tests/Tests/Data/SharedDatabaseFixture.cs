﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
 using Tests.Data.Fixtures;

 namespace Tests.Data
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object(); 
        private static bool _databaseInitialized; 
    
        public SharedDatabaseFixture()
        {
            Connection = new SqlConnection(@"Server=localhost;Database=task_manager_test;Trusted_Connection=True;");
        
            Seed();
        
            Connection.Open();
        }

        public DbConnection Connection { get; }
    
        public DbContext CreateContext(DbTransaction transaction = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Connection).Options;
            var dbContext = new ApplicationDbContext(options);
            
            if (transaction != null)
            {
                dbContext.Database.UseTransaction(transaction);
            }
        
            return dbContext;
        }
    
        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        var firstProject = ProjectFixture.GetFirstProject();
                        var secondProject = ProjectFixture.GetSecondProject();

                        context.Set<Project>().Add(firstProject);
                        context.Set<Project>().Add(secondProject);
                        
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}