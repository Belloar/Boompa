using Boompa.Context;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Interfaces.IRepository;

namespace Boompa.Implementations.Repositories
{
    public class QuestionRepository(BoompaContext context) : IQuestionRepository
    {
        public async Task AddFreeTextQuestionAsync(FreeTextQuestion model)
        {
            await context.FreeTextQuestions.AddAsync(model);
        }

        public Task AddMultiChoiceQuestionAsync(MultiChoiceQuestion model)
        {
            throw new NotImplementedException();
        }

        public Task AddMultiResponseQuestionAsync(MultipleResponseQuestion model)
        {
            throw new NotImplementedException();
        }

        
    }
}
