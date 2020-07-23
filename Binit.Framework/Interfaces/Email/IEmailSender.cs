using System.Threading.Tasks;

namespace Binit.Framework.Interfaces.Email
{
    /// <summary>
    /// Interface for EmailSender that works with IEmailDTO.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email using the htmlMessage (string) parameter as content.
        /// </summary>
        Task SendEmailAsync(string email, string subject, string htmlMessage);

        /// <summary>
        /// Sends an email using the IEmailDTO model to define the template and content of the email.
        /// </summary>
        Task SendEmailAsync<T>(string email, string subject, T model) where T : IEmailDTO;
    }
}
