using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TMS.Interfaces;
using TMS.Data;
using AutoMapper;
using TMS.Bootstrap.Automapper;

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

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            #region register services as Transient

            #endregion
        }
    }
}
