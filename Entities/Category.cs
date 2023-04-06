using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Category:AuditableEntity
    {
        /*the relationship between the learning materials in this project are as follows ; the category has a collection of sourceMaterials the sourcematerials consists of the files(conversation audio,audio version of an article,pictures,videos) that the learner learns from as well as a collection of questions the questions consists of the question and a collection of options 
         */
        public string Name { get; set; }
        public ICollection<SourceMaterial> SourceMaterials { get; set; }
        //public ICollection<Article> Materials { get; set; }
        //public ICollection<Question> Questions { get; set; }
        
         
    }
}
