using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string ImageSave(IFormFile imageFile)
        {
            var pasta = Path.Combine(_env.WebRootPath, "imagens");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            var url = $"/imagens/{nomeArquivo}";
            return url;

        }
    }
}
