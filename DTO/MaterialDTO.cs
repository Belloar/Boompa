using Boompa.Entities;
namespace Boompa.DTO
{
    public class MaterialDTO
    {
        public class ArticleModel
        {
            // the category the material will fall under
            public string Category { get; set; }

            // the name of the article
            public string SourceMaterialName { get; set; }

            // the content of the article
            public string Text { get; set; } 

            public string Creator { get; set; } // the user that is uploading the material to the database
            public DateTime CreatedOn { get; set; } // the date the material is being created

            // all the question for learner evaluation
           // i'm using the questionModel DTO so as not to expose my domain entity
            public ICollection<QuestionModel>? Questions {get; set;} 
            
            public ICollection<IFormFile>? RawFiles { get; set; } = new HashSet<IFormFile>(); // for images or audio files that will go with the questions 
            

        }

        public class QuestionModel
        {
            public string Description { get; set; }
            public string Answer {  get; set; }
            public ICollection<IFormFile>? RawFiles { get; set; } = new HashSet<IFormFile> ();
        }

        
        


    }
}
