using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.SPs;
using System.Collections.Generic;

namespace Domain.Logic.Interfaces
{
    /// <summary>
    /// CategoryService specific method definitions.
    /// </summary>
    public interface ICategoryService : IService<Category>
    {
        /// <summary>
        /// Creates a category by calling a SP
        /// </summary>
        List<SPReturnCategory> CreateCategoryBySP();
    }
}
