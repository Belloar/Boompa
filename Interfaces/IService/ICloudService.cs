
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces.IService
{
    public interface ICloudService
    {
        Task UploadFilesAsync(Dictionary<string,IFormFile> files);
        Task UploadFileAsync(IFormFile file,string key);
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> GetFileUrlAsync(string Key);
        Task<ICollection<string>> ListFilesAsync();
        Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys);
        

    }
}
