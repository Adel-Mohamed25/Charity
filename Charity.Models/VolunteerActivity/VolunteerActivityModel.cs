namespace Charity.Models.VolunteerActivity
{
    public class VolunteerActivityModel
    {
        public string Id { get; set; }
        public string OrganizerId { get; set; }
        public string Name { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
