using Charity.Domain.Enum;

namespace Charity.Models.User
{
    public class CreateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public UserRole UserType { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
