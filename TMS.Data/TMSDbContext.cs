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
        public DbSet<Label> Labels { get; }
        public DbSet<Task> Tasks { get; }
        public DbSet<UserApp> Users { get; }
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

            modelBuilder.Entity<TaskUser>()
             .HasKey(t => new { t.UserId, t.TaskId });

            modelBuilder.Entity<TaskUser>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.TaskUsers)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<TaskUser>()
                .HasOne(sc => sc.Task)
                .WithMany(c => c.Moderators)
                .HasForeignKey(sc => sc.TaskId);
        }
    }
}