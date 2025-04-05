namespace Charity.Models.VolunteerApplication
{
    public class CreateVolunteerApplicationModel
    {
        public string VolunteerId { get; set; }
        public string? RequestDetails { get; set; }
        public string? VolunteerActivityId { get; set; }
        public string? ProjectId { get; set; }
    }
}
