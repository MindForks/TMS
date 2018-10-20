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

        #region 
        public DbSet<Book> Books { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasKey(k => k.Id);
        }
    }
}
