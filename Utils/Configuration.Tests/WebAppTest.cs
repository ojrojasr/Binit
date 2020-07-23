using Binit.Framework;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Email;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using Xunit;

namespace Configuration.Tests
{
    public class WebAppTest : IClassFixture<WebApplicationFactory<WebApp.Startup>>
    {
        #region Initializer
        private readonly WebApplicationFactory<WebApp.Startup> _factory;

        public WebAppTest(WebApplicationFactory<WebApp.Startup> factory)
        {
            _factory = factory;
        }
        #endregion

        [Fact]
        public void TestEmailConfiguration()
        {
            var serviceProvider = (IServiceProvider)_factory.Services.GetService(typeof(IServiceProvider));
            var _localizer = (IStringLocalizer<SharedResources>)_factory.Services.GetService(typeof(IStringLocalizer<SharedResources>));
            var _configuration = (IConfiguration)_factory.Services.GetService(typeof(IConfiguration));
            using (var scope = serviceProvider.CreateScope())
            {
                IEmailSender emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                emailSender.SendEmailAsync("metcharran@binit.com.ar", "Test", new WelcomeDTO(_configuration, _localizer) { Name = "Martin", CallbackUrl = "Test.html" })
                        .GetAwaiter().GetResult();
            }
        }
    }
}
