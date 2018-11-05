using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;
using TMS.Interfaces;
using TMS.Bootstrap.Automapper;
using TMS.Entities;
using TMS.Data.Repositories;
using TMS.Business;

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
                   options.UseSqlServer(connection));

            #region register services as Transient
            #endregion

            #region register repositories as scoped
            services.AddScoped<IRepository<NotificationType>, BasicRepository<NotificationType>>();
            services.AddScoped<IRepository<Label>, BasicRepository<Label>>();
            services.AddScoped<IRepository<Task>, BasicRepository<Task>>();
            services.AddScoped<IRepository<UserApp>, BasicRepository<UserApp>>();

            #endregion


            #region register services as Transient
            services.AddTransient<LabelService>();
            #endregion
        }
    }
}
