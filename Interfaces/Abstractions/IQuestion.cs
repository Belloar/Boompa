using Boompa.DTO;
using Boompa.Entities.Question;

namespace Boompa.Interfaces.Abstractions
{
    public interface IQuestion
    {
        Task<BaseQuestion> CreateQuestion(MaterialDTO.QuestionDTO model);
    }
}
