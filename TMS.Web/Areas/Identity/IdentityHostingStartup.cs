using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Web.Areas.Identity.Data;
using TMS.Web.Data;
using TMS.Web.Models;

[assembly: HostingStartup(typeof(TMS.Web.Areas.Identity.IdentityHostingStartup))]
namespace TMS.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TMSWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<UserApp>()
                    .AddEntityFrameworkStores<TMSWebContext>();
            });
        }
    }
}