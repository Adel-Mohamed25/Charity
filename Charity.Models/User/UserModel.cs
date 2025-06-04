using Charity.Domain.Enum;

namespace Charity.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsLocked { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
    }
}
