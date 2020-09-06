using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<ProjectEmployer> ProjectEmployers { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManager)
                .WithMany(e => e.ProjectManagerOnProjects)
                .HasForeignKey(p => p.ProjectManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
            ;
            
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Author)
                .WithMany(e => e.TasksAuthor)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
            ;
            
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Performer)
                .WithMany(e => e.TasksPerformer)
                .HasForeignKey(t => t.PerformerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
            ;
            
            modelBuilder.Entity<Employer>()
                .HasMany(e => e.TasksAuthor)
                .WithOne(t => t.Author)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
            ;
            
            modelBuilder.Entity<Employer>()
                .HasMany(e => e.TasksPerformer)
                .WithOne(t => t.Performer)
                .HasForeignKey(t => t.PerformerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
            ;
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}