namespace Boompa.DTO
{
    public class MaterialDTO
    {
        public string SourceName { get; set; }
        public ICollection<IFormFile> RawMaterials { get; set; }
        
        public class QuestionModel
        {
            public string Description { get; set; }
            public IEnumerable<object> Options { get; set; }
        }
        public class OptionModel
        {

        }
    }
}
