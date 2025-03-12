namespace Charity.Models.Authentication
{
    public class VerifyCodeRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
