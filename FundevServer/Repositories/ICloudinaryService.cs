using FundevServer.Data;

namespace FundevServer.Repositories
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImageAsync(IFormFile file);
        public Task<bool> DeleteImageAsync(string publicId);
        public string GetPublicIdFromUrl(string imageUrl);
    }

}
