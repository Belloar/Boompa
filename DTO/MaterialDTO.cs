using Boompa.Entities;
namespace Boompa.DTO
{
    public class MaterialDTO
    {

        public class ArticleModel
        {
            public ICollection<string> Categories { get; set; } = [];// the categories the material can fall under
            public string SourceMaterialName { get; set; } = default!; // the name of the article
            public string Description { get; set; } = default!;
            public string TextContent { get; set; } = default!; // the content of the article
            public DateTime CreatedOn { get; set; } // the date the material is being created
        }

        public class QuestionModel()
        {
            public string TextDescription { get; set; } = default!;
            public string Answer { get; set; } = default!;
            public string Option { get; set; } = default!;
            public string QuestionType { get; set; } = default!;
            public string OptionType { get; set; } = default!;
            

        }

        public class ConsumptionModel()
        {
            public Guid SourceId { get; set; }
            public ICollection<CategoryDTO> Categories { get; set; } = [];
            public string MaterialName { get; set; } = default!;
            public string TextContent { get; set; } = default!;
            public ICollection<QuestionDTO>? Questions { get; set; } = [];
            
        }

        public class QuestionDTO()
        {
            public string TextQuestion { get; set; } = default!;
            public string Answer { get; set; } = default!;
            public string Options { get; set; } = default!;
            public string QuestionType { get; set; } = default!;
            public string OptionType { get; set; } = default!;

        }
       
        public record SourceDescriptor()
        {
            public Guid SourceId { get; set; }
            public string SourceName { get; set; } = default!;
            public string SourceDescription { get; set; } = default!;
            public ICollection<CategoryDetails> Categories { get; set; } = [];
        }

        public record CategoryDetails()
        {
            public Guid CategoryId { get; set; }
            public string Name { get; set; } = default!;

        }

        public record TinyModel()
        {
            public ICollection<Guid> Categories { get; set; } = [];// the category the material will fall under
            public string SourceMaterialName { get; set; } = default!; // the name of the article
            public string Description { get; set; } = default!;
            public string Content { get; set; } = default!; // the content of the article
            public string CreatedBy { get; set; } = default!;
            public DateTime CreatedOn { get; set; } // the date the material is being created
        }

        public record TinyMedia(IFormFile File);

        public record AIQueGenDTO(string Material, string Prompt);

        public record CategoryDTO()
        {
            public Guid Id {  get; set; }
            public string Name { get; set; } = default!;

        }
    }
}
