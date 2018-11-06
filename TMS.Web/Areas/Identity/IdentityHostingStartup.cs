using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;
using TMS.Entities;

[assembly: HostingStartup(typeof(TMS.Web.Areas.Identity.IdentityHostingStartup))]
namespace TMS.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<TMSDbContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("DefaultConnection")));
                services.AddDefaultIdentity<UserApp>()
                     .AddEntityFrameworkStores<TMSDbContext>();
            });
        }
    }
}