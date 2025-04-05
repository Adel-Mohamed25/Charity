using Microsoft.AspNetCore.Http;

namespace Charity.Contracts.ServicesAbstraction
{
    public interface IFileServices
    {
        Task<string> UploadImageAsync(string location, IFormFile file);
        Task<bool> DeleteImageAsync(string location, string imageUrl);
    }
}
