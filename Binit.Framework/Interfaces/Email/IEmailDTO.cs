using Binit.Framework.Constants.Email;

namespace Binit.Framework.Interfaces.Email
{
    /// <summary>
    /// Interface that defines the required fields for an Email DTO.
    /// </summary>
    public interface IEmailDTO
    {
        /// <summary>
        /// Which template should be used for the email.
        /// </summary>
        TemplateConstants.TemplateEnum Template { get; }

        /// <summary>
        /// Localized title for the email.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Localized call to action text for the email.
        /// </summary>
        string CallToActionText { get; }

        /// <summary>
        /// Localized call to action button text for the email.
        /// </summary>
        string CallToActionButton { get; }

        /// <summary>
        /// Localized footer for the email.
        /// </summary>
        string Footer { get; }

        /// <summary>
        /// SystemUrl retrieved from AppSettings.
        /// </summary>
        string SystemUrl { get; }

        /// <summary>
        /// SystemName retrieved from AppSettings.
        /// </summary>
        string SystemName { get; }
    }
}