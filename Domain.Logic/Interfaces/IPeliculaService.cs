using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IPeliculaService : IService<Pelicula>
    {
        Pelicula GetFull(Guid id);
        IQueryable<Pelicula> GetFull();
        Task<Pelicula> GetFullAsync(Guid id);
    }
}
