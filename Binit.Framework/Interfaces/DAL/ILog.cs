namespace Binit.Framework.Interfaces.DAL
{
    public interface ILog : IEntity
    {
        string Message { get; set; }
        string Detail { get; set; }
    }
}