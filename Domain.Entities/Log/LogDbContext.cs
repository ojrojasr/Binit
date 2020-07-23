using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Log
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        // Add DbSet for ILog entities.
        public DbSet<ErrorLog> Errors { get; set; }
        public DbSet<AuditLog> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add modelBuilder for ILog entities. 
            modelBuilder.Entity<ErrorLog>();
            modelBuilder.Entity<AuditLog>();
        }
    }
}