namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        Task<int> AddChallengeAsync();
        Task<int> AddCategoryAsync();
        Task<int> AddFileDeets(IEnumerable<IFormFile> files);
        Task<int> AddQuestionAsync();
        Task<int> AddOptionAsync();
        Task<int> DeleteSourceMaterial();
        Task<int> DeleteQuestionAsync();
        Task<int> UpdateQuestion();
        //Task<int> UploadArticleAsync(IFormFile file);
        //Task<int> CreateArticleAsync();
        //Task<int> DeleteArticleAsync(string articleName);
        
        //Task<int> UploadImage(string filePath, string fileName);
    }
}
