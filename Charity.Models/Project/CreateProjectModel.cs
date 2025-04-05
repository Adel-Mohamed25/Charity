using Microsoft.AspNetCore.Http;
namespace Charity.Models.Project
{
    public class CreateProjectModel
    {
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ManagerId { get; set; }

    }
}
