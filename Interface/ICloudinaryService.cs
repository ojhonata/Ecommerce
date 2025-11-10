namespace Ecommerce.Interface
{
    public interface ICloudinaryService
    {
        public string UploadImage(IFormFile imageFile, string folder = "ecommerce/products");
        public string UploadVideo(IFormFile videoFile, string folder = "ecommerce/videos");
    }
}
