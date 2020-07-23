using System.Linq;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// This interface is used to access a view from the db, which returns a dto that is not an entity
    /// </summary>
    /// <typeparam name="IDbView">the dtos must implement this interface</typeparam>
    public interface IViewService<IDbView>
    {
        /// <summary>
        /// This method is used to recover the elements from the view in the db
        /// </summary>
        /// <returns></returns>
        IQueryable<IDbView> GetAll();
    }
}
