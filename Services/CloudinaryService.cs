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
            
            // Aumenta o timeout da API para garantir que uploads grandes não caiam
            _cloudinary.Api.Timeout = int.MaxValue; 
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
            // Videos já usam UploadLarge automaticamente em muitas versões, 
            // mas aqui mantemos o padrão se for pequeno.
            using var stream = videoFile.OpenReadStream();
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(videoFile.FileName, stream),
                Folder = folder,
            };
            
            // Se quiser garantir para vídeos grandes também, pode usar UploadLarge aqui
            var result = _cloudinary.Upload(uploadParams); 
            
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Erro no upload: " + result.Error?.Message);
            }
            return result.SecureUrl.ToString();
        }

        public string UploadFile(IFormFile file, string folder = "ecommerce/models3D")
        {
            if (file == null || file.Length == 0) return null;

            using var stream = file.OpenReadStream();
            
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                Overwrite = true,
                UseFilename = true,
                UniqueFilename = true,
            };

            // =======================================================
            // SOLUÇÃO PARA O LIMITE DE 10MB: UPLOAD LARGE (CHUNKED)
            // =======================================================
            
            // Define pedaços de 5MB (5 * 1024 * 1024 bytes)
            // Isso garante que cada pedaço passe pelo limite de 10MB do plano free
            int bufferSize = 5 * 1024 * 1024; 

            var result = _cloudinary.UploadLarge(uploadParams, bufferSize);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Erro no upload: " + result.Error?.Message);
            }

            return result.SecureUrl.ToString();
        }
    }
}