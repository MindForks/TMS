using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;
using TMS.Interfaces;
using TMS.Bootstrap.Automapper;
using TMS.Entities;
using TMS.Data.Repositories;

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
            services.AddSingleton<Interfaces.IMapper, TMSAutoMapper>();


            services.AddDbContext<TMSIdentityDbContext>(options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));

            #region register services as Transient
            #endregion
            #region register repositories as scoped
            services.AddScoped<IRepository<NotificationType>, BasicRepository<NotificationType>>();
            #endregion


            #region register services as Transient
            #endregion
        }
    }
}
