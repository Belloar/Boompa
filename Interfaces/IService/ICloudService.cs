using Boompa.Implementations.Services;
namespace Boompa.Interfaces.IService
{
    public interface ICloudService
    {
        Task UploadFilesAsync(ICollection<IFormFile> files);
        Task UploadFileAsync(IFormFile file);
        Task<Stream> GetFileAsync(string Key);
        Task<ICollection<string>> ListFilesAsync();

        Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys);

    }
}
