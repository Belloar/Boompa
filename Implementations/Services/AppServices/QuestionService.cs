using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;

namespace Boompa.Implementations.Services.AppServices
{
    public class QuestionService(
        FreeTextQuestionBuilder freeTextbuilder,
        MCQuestionBuilder mcqBuilder,
        ISourceMaterialRepository _repo) : IQuestionService
    {
        public Task<FreeTextQuestion> AddFreeQuestion(MaterialDTO.QuestionModel request)
        {
            var question = freeTextbuilder.BuildQuestion(request);

            throw new NotImplementedException();
        }

        public Task<MultiChoiceQuestion> AddMultiChoiceQuestion(MaterialDTO.QuestionModel request)
        {
            throw new NotImplementedException();
        }

        public Task<MultipleResponseQuestion> AddMultiResponseQuestion(MaterialDTO.QuestionModel request)
        {
            throw new NotImplementedException();
        }

        public Task AddQuestion(MaterialDTO.QuestionModel request)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteQuestion(Guid QuestionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BaseQuestion>> GetQuestions(Guid sourceId)
        {
            throw new NotImplementedException();
        }
    }
}
