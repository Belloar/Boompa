using Boompa.Implementations.Services;
namespace Boompa.Interfaces.IService
{
    public interface ICloudService
    {
        Task UploadFilesAsync(ICollection<IFormFile> files);
        Task<Stream> GetFileAsync(string Key);
        Task<ICollection<string>> ListFilesAsync();

        Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys);

    }
}
