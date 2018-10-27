using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Web.Areas.Identity.Data;

namespace TMS.Data
{
    public class TMSIdentityDbContext:IdentityDbContext<UserApp>
    {
        public TMSIdentityDbContext(DbContextOptions<TMSIdentityDbContext> options)
        : base(options)
        {
        }

       // public DbSet<UserApp> UsersApp { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
