using Boompa.Entities;
namespace Boompa.DTO
{
    public class MaterialDTO
    {

        public class ArticleModel
        {
            public ICollection<string> Categories { get; set; }// the categories the material can fall under
            public string SourceMaterialName { get; set; } // the name of the article
            public string Description { get; set; }
            public string TextContent { get; set; } // the content of the article
            public DateTime CreatedOn { get; set; } // the date the material is being created
        }

        public class QuestionModel()
        {
            public string? TextDescription { get; set; }
            public string Answer {  get; set; }
            public string Option {  get; set; }
            public string QuestionType { get; set; }
            

        }

        public class ConsumptionModel()
        {
            public Guid SourceId { get; set; }
            public ICollection<CategoryDTO> Categories { get; set; }
            public string MaterialName { get; set; }
            public string TextContent { get; set; }
            public ICollection<QuestionDTO>? Questions { get; set; } = [];
            
        }

        public class QuestionDTO()
        {
            public string? TextQuestion { get; set; }
            public string? FileQuestion { get; set; }
            public string Answer { get; set; }
            public string Options { get; set; }
            public string QuestionType { get; set; }

        }
       
        public record SourceDescriptor()
        {
            public Guid SourceId { get; set; }
            public string SourceName { get; set; }
            public string SourceDescription { get; set; }
            public ICollection<CategoryDetails> Categories { get; set; }
        }

        public record CategoryDetails()
        {
            public Guid CategoryId { get; set; }
            public string Name { get; set; }

        }

        public class TinyModel()
        {
            public ICollection<Guid> Categories { get; set; }// the category the material will fall under
            public string SourceMaterialName { get; set; } // the name of the article
            public string Description { get; set; }
            public string Content { get; set; } // the content of the article
            public string CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; } // the date the material is being created
        }

        public record TinyMedia(IFormFile file);

        public record AIQueGenDTO(string Material, string Prompt);

        public record CategoryDTO()
        {
            public Guid Id {  get; set; }
            public string Name { get; set; }

        }
    }
}
