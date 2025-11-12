using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Interface;

namespace Ecommerce.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration config)
        {
            var cloudName = Environment.GetEnvironmentVariable("CloudName");
            var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            var apiSecret = Environment.GetEnvironmentVariable("ApiSecret");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public string UploadImage(IFormFile imageFile, string folder = "ecommerce/products")
        {
            using var stream = imageFile.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream),
                Folder = folder
            };

            var result = _cloudinary.Upload(uploadParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Erro no upload: " + result.Error?.Message);
            }

            return result.SecureUrl.ToString();

        }

        public string UploadVideo(IFormFile videoFile, string folder = "ecommerce/videos")
        {
            using var stream = videoFile.OpenReadStream();
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(videoFile.FileName, stream),
                Folder = folder
            };
            var result = _cloudinary.Upload(uploadParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Erro no upload: " + result.Error?.Message);
            }
            return result.SecureUrl.ToString();
        }

    }
}
