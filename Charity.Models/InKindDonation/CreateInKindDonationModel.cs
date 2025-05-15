using Charity.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Charity.Models.InKindDonation
{
    public class CreateInKindDonationModel
    {
        public string Name { get; set; }
        public DonationItemType ItemType { get; set; } // "Clothes", "Food", "Medical Supplies"
        public DonationStatus DonationStatus { get; set; }  // "New", "UsedExcellentCondition", "UsedGoodCondition"
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public IList<IFormFile>? Images { get; set; }
        public string DonorId { get; set; }
        public string? ProjectId { get; set; }
    }
}
