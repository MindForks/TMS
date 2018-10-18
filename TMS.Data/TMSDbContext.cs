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

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(k => k.Id);
        }
    }
}
