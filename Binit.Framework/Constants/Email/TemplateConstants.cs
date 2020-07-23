namespace Binit.Framework.Constants.Email
{
    public static class TemplateConstants
    {
        public enum TemplateEnum
        {
            Welcome,
            ForgotPassword,
            PasswordRecovery
        }

        /// <summary>
        /// Retrieves the Assembly path to the Email Template.
        /// </summary>
        public static string GetTemplateAssemblyPath(this TemplateEnum templateEnum)
        {
            string response = "Binit.Framework.Helpers.Email.Views.";
            switch (templateEnum)
            {
                case TemplateEnum.Welcome:
                    response += "Welcome.cshtml";
                    break;
                case TemplateEnum.ForgotPassword:
                    response += "ForgotPassword.cshtml";
                    break;
                case TemplateEnum.PasswordRecovery:
                    response += "PasswordRecovery.cshtml";
                    break;
                default:
                    response = null;
                    break;
            }
            return response;
        }
    }
}
