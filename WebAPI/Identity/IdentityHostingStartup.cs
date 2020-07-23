using Domain.Entities.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

[assembly: HostingStartup(typeof(WebAPI.Identity.IdentityHostingStartup))]
namespace WebAPI.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
                    {
                        config.SignIn.RequireConfirmedEmail = true;
                        config.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<ModelDbContext>()
                    .AddDefaultTokenProviders();

                // Get JWT Settings from config.
                var jwtSettings = context.Configuration.GetSection("JwtSettings");

                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;

                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtSettings["Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                            ClockSkew = TimeSpan.Zero, // Remove delay of token when expire.
                            RequireExpirationTime = true, // Tokens must have an expiration value.
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true
                        };
                    });
            });
        }
    }
}