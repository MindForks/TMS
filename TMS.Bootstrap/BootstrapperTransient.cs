using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;
using TMS.Interfaces;

namespace TMS.Bootstrap
{
    public static class ExtensionRepositoryConfiguration
    {
        public static void RegisterDomainModels(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

             services.AddDbContext<TMSDbContext>(options =>
             {
                 options.UseSqlServer(connection);
             });

            services.AddSingleton(typeof(string), connection);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<TMSIdentityDbContext>(options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));

            #region register services as Transient

            #endregion
        }
    }
}
