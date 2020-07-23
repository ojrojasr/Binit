using Binit.Framework;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Email;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Configuration.Tests
{
    public class WebAPITest : IClassFixture<WebApplicationFactory<WebAPI.Startup>>
    {
        #region Initializer
        private readonly WebApplicationFactory<WebAPI.Startup> _factory;
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IConfiguration _configuration;

        public WebAPITest(WebApplicationFactory<WebAPI.Startup> factory, IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            _factory = factory;
			_localizer = localizer;
			_configuration = configuration;
        }
        #endregion

        [Fact]
        public void TestEmailConfiguration()
        {
            var serviceProvider = (IServiceProvider)_factory.Services.GetService(typeof(IServiceProvider));
            using (var scope = serviceProvider.CreateScope())
            {
                IEmailSender emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                emailSender.SendEmailAsync("metcharran@binit.com.ar", "Test", new WelcomeDTO(_configuration, _localizer) { Name = "Martin", CallbackUrl = "Test.html" })
                        .GetAwaiter().GetResult();
            }
        }
    }
}
