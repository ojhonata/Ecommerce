namespace Ecommerce.Interface
{
    public interface IImageService
    {
        public string ImageSave(IFormFile imageFile);
        public string IamgeInternalSave(IFormFile imageFile);
        public string ImageEngineSave(IFormFile imageFile);
        public string VideoSave(IFormFile videoFile);
    }
}
