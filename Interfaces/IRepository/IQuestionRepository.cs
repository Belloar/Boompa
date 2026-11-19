using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;

namespace Boompa.Interfaces.IRepository
{
    public interface IQuestionRepository
    {
        Task AddFreeTextQuestionAsync(FreeTextQuestion model);
        Task AddMultiChoiceQuestionAsync(MultiChoiceQuestion model);
        Task AddMultiResponseQuestionAsync(MultipleResponseQuestion model);
    }
}
