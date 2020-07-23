namespace WebAPI.Interfaces.Configuration
{
    public interface IDeeplinksConfiguration
    {
        string Scheme { get; set; }
        string Host { get; set; }
    }
}