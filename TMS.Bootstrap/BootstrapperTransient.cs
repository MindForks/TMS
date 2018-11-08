using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;
using TMS.Interfaces;
using TMS.Bootstrap.Automapper;
using TMS.Entities;
using TMS.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using TMS.Business.Services;

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

            #region Repositories
            services.AddScoped<IRepository<NotificationType>, BasicRepository<NotificationType>>();
            services.AddScoped<IRepository<Label>, BasicRepository<Label>>();
            services.AddScoped<IRepository<Task>, BasicRepository<Task>>();
            services.AddScoped<IRepositoryAsync<UserApp>, UserRepository>();
            #endregion Repositories

            #region Services
            services.AddTransient<LabelService>();
            #endregion Services
        }
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserApp, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<TMSDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
