using Microsoft.EntityFrameworkCore;
using TMS.Entities;

namespace TMS.Data
{
    public class TMSDbContext : DbContext
    {
        private string ConnectionString;

        public TMSDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region DbSets
        public DbSet<NotificationType> NotificationTypes { get; }
        public DbSet<Label> labels { get; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NotificationType>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<NotificationType>()
                .Property(t => t.Title).IsRequired();
        }
    }
}
