using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Binit.Framework.Helpers.Configuration
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public SmtpClient GenerateSmtpClient()
        {
            SmtpClient client = new SmtpClient(Host, Port);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Name, Password);
            return client;
        }

        public SmtpConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection("SmtpConfiguration").Bind(this);
            return this;
        }
    }
}