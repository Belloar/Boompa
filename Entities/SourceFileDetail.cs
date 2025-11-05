namespace Boompa.Entities
{
    public class SourceFileDetail
    {
        public int Id { get; set; }
        public int SourceMaterialId { get; set; } // if not null, specifies that the file is for a sourceMaterial
        public SourceMaterial SourceMaterial { get; set; }//navigation property
        public string Path{ get; set; } // the path of the file in localhost
        public string FileType { get; set; } // describes whether it is audio,image or other type of file 
        
    }
}
