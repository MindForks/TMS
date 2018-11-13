using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Entities;

namespace TMS.Data
{
    public class TMSDbContext : IdentityDbContext<UserApp>
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
        #endregion DbSets

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

            modelBuilder.Entity<Label>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Label>()
                .HasOne(o => o.User)
                .WithMany(m => m.Labels)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<TaskStatus>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<TaskStatus>()
               .Property(p => p.Title).IsRequired();
            modelBuilder.Entity<TaskStatus>()
                .HasData(
                new TaskStatus() { Title = "ToDo", Id = (int)TaskStatuses.ToDo},
                new TaskStatus() { Title = "InProgress", Id = (int)TaskStatuses.InProgress },
                new TaskStatus() { Title = "Done", Id = (int)TaskStatuses.Done }
                );

            modelBuilder.Entity<TaskModerator_User>()
             .HasKey(t => new { t.UserId, t.TaskId });

            modelBuilder.Entity<TaskModerator_User>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.TaskModerator_Users)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<TaskModerator_User>()
                .HasOne(sc => sc.Task)
                .WithMany(c => c.Moderators)
                .HasForeignKey(sc => sc.TaskId);

            modelBuilder.Entity<TaskViewer_User>()
            .HasKey(t => new { t.UserId, t.TaskId });

            modelBuilder.Entity<TaskViewer_User>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.TaskViewer_Users)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<TaskViewer_User>()
                .HasOne(sc => sc.Task)
                .WithMany(c => c.Viewers)
                .HasForeignKey(sc => sc.TaskId);
        }
    }
}