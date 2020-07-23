using Binit.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Errors;

namespace WebApp.Areas.Identity
{

    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer<SharedResources> localizer;

        public MultilanguageIdentityErrorDescriber(IStringLocalizer<SharedResources> localizer) : base()
        {
            this.localizer = localizer;
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(localizer[Lang.EmailTaken], email)
            };
        }

        public override IdentityError DuplicateUserName(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(localizer[Lang.UsernameTaken], email)
            };
        }

    }
}