using Charity.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Charity.Models.User
{
    public class UpdateUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
    }
}
