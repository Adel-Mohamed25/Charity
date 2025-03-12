namespace Charity.Models.Authentication
{
    public class RefreshJwtRequestModel
    {
        public string Jwt { get; set; }
        public string RefreshJwt { get; set; }
    }
}
