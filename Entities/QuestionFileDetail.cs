namespace Boompa.Entities
{
    public class QuestionFileDetail
    {
        public int Id { get; set; }
        public int QuestionId { get; set; } // specifies that the file is for a question
        public string Path { get; set; } // the path of the file in localhost
        public string FileType { get; set; } // describes whether it is audio,image or other type of file 

    }
}
