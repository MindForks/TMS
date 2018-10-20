using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TMS.Interfaces;
using TMS.Business;
using TMS.Data;

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

            #region register services 
            services.AddScoped<BookServicies>();
            #endregion
        }
    }
}
