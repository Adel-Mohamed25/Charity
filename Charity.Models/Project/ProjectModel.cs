using Charity.Domain.Enum;

namespace Charity.Models.Project
{
    public class ProjectModel
    {
        public string Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public string Description { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectStatus ProjectStatus { get; set; }  // "Ongoing", "Completed", "Pending"
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ManagerId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
