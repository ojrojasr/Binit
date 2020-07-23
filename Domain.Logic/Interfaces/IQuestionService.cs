using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IQuestionService : IService<Question>
    {
        Question GetFull(Guid id);
        Question GetQuestion(Guid id);
        IQueryable<Question> GetAllFull();
        Task<Question> GetFullAsync(Guid id);
    }
}
