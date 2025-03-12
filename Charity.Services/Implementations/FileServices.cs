using Charity.Contracts.ServicesAbstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Charity.Services.Implementations
{
    public class FileServices : IFileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileServices> _logger;

        public FileServices(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FileServices> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        /// <summary>
        /// Uploads an image to the specified location and returns its URL.
        /// </summary>
        /// <param name="location">The directory where the image should be saved.</param>
        /// <param name="file">The image file to be uploaded.</param>
        /// <returns>
        /// A string representing the full URL of the uploaded image if successful, 
        /// or an error message in case of failure.
        /// </returns>
        public async Task<string> UploadImageAsync(string location, IFormFile file)
        {
            if (string.IsNullOrEmpty(location))
            {
                return "InvalidLocation";
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, location);
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension))
            {
                return "Invalid File Extension";
            }

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var fileFullPath = Path.Combine(path, fileName);

            if (file.Length > 0)
            {
                try
                {
                    if (!File.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (var filestream = new FileStream(fileFullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                        await filestream.FlushAsync();
                    }

                    var request = _httpContextAccessor.HttpContext?.Request;
                    var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : "";
                    var relativePath = $"/{location}/{fileName}";
                    var fullUrl = $"{baseUrl}{relativePath}";
                    return fullUrl;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Faild To Upload Image");
                    return "Faild To Upload Image";
                }
            }
            else
            {
                _logger.LogWarning("NoImage");
                return "NoImage";
            }
        }

        /// <summary>
        /// Deletes an image from the server.
        /// </summary>
        /// <param name="imageUrl">The URL or relative path of the image to be deleted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.  
        /// Returns a boolean indicating whether the deletion was successful.
        /// </returns>
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogWarning("Invalid image URL.");
                return await Task.FromResult(false);
            }

            try
            {
                var uri = new Uri(imageUrl);
                var imagePath = uri.AbsolutePath.TrimStart('/');
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation($"Image deleted: {fullPath}");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Image not found: {fullPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete image.");
                return false;
            }
        }


    }
}
