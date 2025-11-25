namespace Ecommerce.Interface
{
    public interface ICloudinaryService
    {
        string UploadImage(IFormFile imageFile, string folder = "ecommerce/products");
        string UploadVideo(IFormFile videoFile, string folder = "ecommerce/videos");
        string UploadFile(IFormFile file, string folder = "ecommerce/models3D");
    }
}