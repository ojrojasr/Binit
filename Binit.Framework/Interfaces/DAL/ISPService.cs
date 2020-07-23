using System.Linq;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// Service to call a SP with parameters
    /// </summary>
    /// <typeparam name="SPCommand"></typeparam>
    public interface ISPService<SPCommand> where SPCommand : ISPCommand
    {
        /// <summary>
        /// Executes a Stored Procedure with parameters
        /// </summary>
        /// <param name="spCommand"></param>
        void ExecuteSP(SPCommand spCommand);

        IQueryable<DbView> ExecuteSP<DbView>(SPCommand spCommand) where DbView : class, IDbView;
    }
}
