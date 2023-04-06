using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity 
    {
        /*i want to delete some entities (photo,Questionphoto,article,raw material,audio because i'm restructuring but the collection of fileinfo in sourcematerial class has to be related to another table in the database so i'll think of how to do that later sha 
         */
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<FileDeets>  FilePaths { get; set; }
    }
}
