using System;
using audioplayer.Areas.Identity.Data;
using audioplayer.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(audioplayer.Areas.Identity.IdentityHostingStartup))]
namespace audioplayer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<audioplayerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("audioplayerContextConnection")));

                services.AddDefaultIdentity<audioplayerUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<audioplayerContext>();
            });
        }
    }
}