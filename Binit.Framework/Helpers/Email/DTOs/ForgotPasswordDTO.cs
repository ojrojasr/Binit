using Binit.Framework.Constants.Email;
using Binit.Framework.Interfaces.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Lang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.Helpers.Email.Views.ForgotPassword;

namespace Binit.Framework.Helpers.Email.DTOs
{
    public class ForgotPasswordDTO : IEmailDTO
    {
        public ForgotPasswordDTO(IConfiguration configuration, IStringLocalizer<SharedResources> localizer)
        {
            this.SystemName = configuration.GetSection("General")["SystemName"];
            this.Title = localizer[Lang.Title];
            this.Intro = string.Format(localizer[Lang.Intro], this.Name);
            this.CallToActionText = localizer[Lang.CallToActionText];
            this.CallToActionButton = localizer[Lang.CallToActionButton];
            this.Footer = string.Format(localizer[Lang.Footer], this.SystemName);
            this.SystemUrl = configuration.GetSection("General")["SystemUrl"];
        }

        public TemplateConstants.TemplateEnum Template
        {
            get
            {
                return TemplateConstants.TemplateEnum.ForgotPassword;
            }
        }
        public string Name { get; set; }
        public string CallbackUrl { get; set; }

        public string SystemName { get; set; }

        public string SystemUrl { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public string CallToActionText { get; set; }

        public string CallToActionButton { get; set; }

        public string Footer { get; set; }
    }
}
