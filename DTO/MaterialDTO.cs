using Boompa.Entities;
namespace Boompa.DTO
{
    public class MaterialDTO
    {

        public class ArticleModel
        {
            
            public string Category { get; set; }// the category the material will fall under
            public string SourceMaterialName { get; set; } // the name of the article
            public string Description { get; set; }
            public string TextContent { get; set; } // the content of the article
            
            public DateTime CreatedOn { get; set; } // the date the material is being created
            public ICollection<FileDTO>? RawFiles { get; set; } = new HashSet<FileDTO>(); // for images or audio files that will go with the source material
        }

        public class QuestionModel()
        {
            public string? TextDescription { get; set; }
            public IFormFile? FileDescription { get; set; }
            public string Answer {  get; set; }
            public string Option {  get; set; }
            public string QuestionType { get; set; }
            

        }

        public class ConsumptionModel()
        {
            public Guid CategoryId { get; set; }
            public string Category { get; set; }
            public string MaterialName { get; set; }
            public string TextContent { get; set; }
            public ICollection<QuestionDto>? Questions { get; set; } = [];
            public ICollection<FileDTO.ReturnDTO>? SourceFiles { get; set; } = [];
            
        }

        public class QuestionDto()
        {
            public string? TextQuestion { get; set; }
            public string? FileQuestion { get; set; }
            public string Answer { get; set; }
            public string Options { get; set; }
            public string QuestionType { get; set; }

        }
       
        public class SourceDescriptor()
        {
            public Guid SourceId { get; set; }
            public string SourceName { get; set; }
            public string SourceDescription { get; set; }
        }
    }
}
