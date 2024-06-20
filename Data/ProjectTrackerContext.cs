using Microsoft.EntityFrameworkCore;
using ProjectTrackerAPI.Models;

namespace ProjectTrackerAPI.Data
{
    public class ProjectTrackerContext : DbContext
    {
        public ProjectTrackerContext(DbContextOptions<ProjectTrackerContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectTasks) // Référence à ProjectTasks au lieu de Tasks
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ProjectTasks) // Référence à ProjectTasks au lieu de Tasks
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
