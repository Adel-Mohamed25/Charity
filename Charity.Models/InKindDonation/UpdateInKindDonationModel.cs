using Charity.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Charity.Models.InKindDonation
{
    public class UpdateInKindDonationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DonationItemType ItemType { get; set; } // "Clothes", "Food", "Medical Supplies"
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public IList<IFormFile>? Images { get; set; }
        public IList<string>? ImageUrls { get; set; }
        public bool IsAllocated { get; set; }
        public string DonorId { get; set; }
        public string? ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
