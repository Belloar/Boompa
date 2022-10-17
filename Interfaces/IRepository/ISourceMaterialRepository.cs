namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        Task<int> CreateQuestionAsync();
        Task<int> CreateArticleAsync();
        Task<int> CreateOptionAsync();
        Task<int> CreateChallengeAsync();
        Task<int> CreateCategoryAsync();
        Task<int> DeleteQuestionAsync();
        Task<int> DeleteArticleAsync(string articleName);
        Task<int> UpdateQuestion();
        Task<int> UploadArticleAsync(IFormFile file);
        Task<int> UploadImage(string filePath, string fileName);
        
    }
}
