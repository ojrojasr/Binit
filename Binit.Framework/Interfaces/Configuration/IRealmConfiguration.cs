namespace Binit.Framework.Interfaces.Configuration
{
    public interface IRealmConfiguration
    {
        string Name { get; set; }
        bool AllowSelfSignUp { get; set; }
        bool Allow2FA { get; set; }
    }
}