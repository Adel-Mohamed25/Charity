using Microsoft.AspNetCore.Http;

namespace Charity.Models.Email
{
    public class SendEmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }
    }
}
