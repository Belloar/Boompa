
using Amazon.S3;
using Amazon.S3.Model;
using Boompa.Interfaces.IService;


namespace Boompa.Implementations.Services 
{
    public class BBb2StorageService: ICloudService
    {
        private readonly IAmazonS3 _client;
        private readonly string _bucketName;
        

        public BBb2StorageService(IAmazonS3 client,string bucketName)
        {
           _client = client;
            _bucketName = bucketName;

        }


        public async Task UploadFilesAsync(Dictionary<string,IFormFile> files)
        {
            foreach (var file in files)
            {
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = file.Key,
                    InputStream = file.Value.OpenReadStream(),
                    ContentType = file.Value.ContentType
                };

                await _client.PutObjectAsync(request);
            }
        }

        public async Task<string> GetFileUrlAsync(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };
            var fileName = key.Split("|");
            var response = await _client.GetPreSignedURLAsync(request);
            return response;

        }

        public async Task<ICollection<string>> ListFilesAsync()
        {
            var response = await _client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = _bucketName
            });

            return response.S3Objects.Select(o => o.Key).ToList();

            
        }
        public async Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys)
        {
            var result = new List<Stream>();
            foreach(var key in keys)
            {
                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };
                var response = await _client.GetObjectAsync(request);
                result.Add(response.ResponseStream);
            }
            return result;
        }

        public async Task UploadFileAsync(IFormFile file, string key)
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

             await _client.PutObjectAsync(request);
        }
    }
}
