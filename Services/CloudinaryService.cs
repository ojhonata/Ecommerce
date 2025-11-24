using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Interface;

namespace Ecommerce.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IWebHostEnvironment _env;

        public CloudinaryService(IConfiguration config, IWebHostEnvironment env)
        {
            _env = env;

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
                Folder = folder,
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
                Folder = folder,
            };
            var result = _cloudinary.Upload(uploadParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Erro no upload: " + result.Error?.Message);
            }
            return result.SecureUrl.ToString();
        }

        public string SalvarArquivoLocal(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

            string webRootPath = _env.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            string folderName = "models3D";
            string pastaDestino = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            var extensao = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extensao}";

            var fullPath = Path.Combine(pastaDestino, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/{folderName}/{fileName}";
        }
    }
}
