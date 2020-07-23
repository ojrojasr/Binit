using Domain.Entities.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: HostingStartup(typeof(WebApp.Areas.Identity.IdentityHostingStartup))]
namespace WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
                    {
                        config.User.RequireUniqueEmail = true;
                        config.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<ModelDbContext>()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<MultilanguageIdentityErrorDescriber>();
            });
        }
    }
}