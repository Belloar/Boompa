using Boompa.DTO;
using Boompa.Entities.Question;

namespace Boompa.Interfaces.Abstractions
{
    public interface IQuestion
    {
        BaseQuestion Create(MaterialDTO.QuestionDTO model);
    }
}
