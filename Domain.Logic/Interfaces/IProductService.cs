using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IProductService : IService<Product>
    {
        Product GetFull(Guid id);
        Task<Guid> GetFirstId();
        Task<Product> GetFullAsync(Guid id, bool asNoTracking = false);
        IQueryable<ProductFeaturesView> GetThroughView();
    }
}
