using Amazon.S3;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

namespace Boompa.Extensions
{
    public static class BBb2StorageExtension
    {
        public static IServiceCollection AddBBb2Storage(this IServiceCollection services,IConfiguration configuration) 
        {
            var section = configuration.GetSection("Cloud");
            var endpoint = section["Endpoint"];
            var keyId = section["KeyId"];
            var applicationKey = section["ApplicationKey"];
            var bucketName = section["BucketName"];

            var clientConfig = new AmazonS3Config()
            {
                ServiceURL = endpoint,
                ForcePathStyle = true,
            };
            var client = new AmazonS3Client(keyId, applicationKey, clientConfig);

            services.AddSingleton<IAmazonS3>(client);
            services.AddSingleton(sp => new Implementations.Services.BBb2StorageService(client,bucketName));

            return services;
        }
    }
}
