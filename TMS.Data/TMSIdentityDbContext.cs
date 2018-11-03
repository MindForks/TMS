using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Entities;
using TMS.EntitiesDTO;

namespace TMS.Data
{
    public class TMSIdentityDbContext:IdentityDbContext<UserAppDTO>
    {
        public TMSIdentityDbContext(DbContextOptions<TMSIdentityDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
