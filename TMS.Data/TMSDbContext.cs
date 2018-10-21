using Microsoft.EntityFrameworkCore;

namespace TMS.Data
{
    public class TMSDbContext : DbContext
    {
        private string ConnectionString;

        public TMSDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region DbSet
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
