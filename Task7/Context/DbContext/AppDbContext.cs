using Microsoft.EntityFrameworkCore;
using Task7.Utilities.Configuration;
namespace Task7.Context.AppDbContext
{
    class AppDbContext : DbContext
    {
        public DbSet<Entities.Project> Projects { get; set; }
        public DbSet<Entities.Test> Tests { get; set; }        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseMySQL(ConfigurationManager.Configuration.ConnectionString("unionReporting"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Project>().ToTable("project");
            modelBuilder.Entity<Entities.Test>().ToTable("test");            
            modelBuilder.Entity<Entities.Test>().Property(test => test.StatusId).HasColumnName("status_id");
            modelBuilder.Entity<Entities.Test>().Property(test => test.MethodName).HasColumnName("method_name");
            modelBuilder.Entity<Entities.Test>().Property(test => test.ProjectId).HasColumnName("project_id");
            modelBuilder.Entity<Entities.Test>().Property(test => test.SessionId).HasColumnName("session_id");
            modelBuilder.Entity<Entities.Test>().Property(test => test.StartTime).HasColumnName("start_time");
            modelBuilder.Entity<Entities.Test>().Property(test => test.EndTime).HasColumnName("end_time");
            modelBuilder.Entity<Entities.Test>().Property(test => test.Environment).HasColumnName("env");
            modelBuilder.Entity<Entities.Test>().Property(test => test.AuthorId).HasColumnName("author_id");
        }
    }
}
