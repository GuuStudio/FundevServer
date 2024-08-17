using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FundevServer.Helpers;
using Microsoft.Extensions.Options;
using FundevServer.Data;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class CloudinarySerive : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinarySerive(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            } else
            {
                throw new Exception("Cant update image");
            }
        }
        public string GetPublicIdFromUrl(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var pathSegments = uri.AbsolutePath.Split('/');
            var fileName = pathSegments.Last();
            return Path.GetFileNameWithoutExtension(fileName);
        }
    }
}
