using Binit.Framework.Constants.Email;
using Binit.Framework.Interfaces.Email;
using Binit.Framework.Interfaces.ExceptionHandling;
using FluentEmail.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace Binit.Framework.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly IFluentEmail mail;
        private readonly IExceptionManager exceptionManager;

        public EmailSender(IFluentEmail mail, IExceptionManager exceptionManager)
        {
            this.mail = mail;
            this.exceptionManager = exceptionManager;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                return mail
                    .To(email)
                    .Subject(subject)
                    .Body(htmlMessage)
                    .SendAsync();
            }
            catch (System.Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public Task SendEmailAsync<T>(string email, string subject, T model) where T : IEmailDTO
        {
            try
            {
                return mail
                    .To(email)
                    .Subject(subject)
                    .UsingTemplateFromEmbedded(model.Template.GetTemplateAssemblyPath(), model, this.GetType().GetTypeInfo().Assembly)
                    .SendAsync();
            }
            catch (System.Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }
    }
}