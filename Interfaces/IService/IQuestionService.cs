using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Implementations.Services.AppServices;

namespace Boompa.Interfaces.IService
{
    public interface IQuestionService
    {
        //Task<MultiChoiceQuestion> AddMultiChoiceQuestion(MaterialDTO.QuestionModel request);
        //Task<MultipleResponseQuestion> AddMultiResponseQuestion(MaterialDTO.QuestionModel request);
        //Task<FreeTextQuestion> AddFreeQuestion(MaterialDTO.QuestionModel request);
        Task<int> DeleteQuestion(Guid QuestionId);

        Task AddQuestion(MaterialDTO.QuestionModel request,QuestionType type);
        Task<ICollection<BaseQuestion>> GetQuestions(Guid sourceId);
    }
}
