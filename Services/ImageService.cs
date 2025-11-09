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

        public string IamgeInternalSave(IFormFile imageFile)
        {
            var pasta = Path.Combine(_env.WebRootPath, "imagensInterior");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            var url = $"/imagens_interior/{nomeArquivo}";
            return url;
        }

        public string ImageEngineSave(IFormFile imageFile)
        {
            var pasta = Path.Combine(_env.WebRootPath, "imagensMotor");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            var url = $"/imagens_motor/{nomeArquivo}";
            return url;
        }

        public string VideoSave(IFormFile videoFile)
        {
            var pasta = Path.Combine(_env.WebRootPath, "videos");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(videoFile.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                videoFile.CopyTo(stream);
            }
            var url = $"/videos/{nomeArquivo}";
            return url;
        }


    }
}
