using Microsoft.AspNetCore.Http;

namespace Charity.Contracts.ServicesAbstractions
{
    public interface IFileServices
    {
        Task<string> UploadImageAsync(string location, IFormFile file);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
