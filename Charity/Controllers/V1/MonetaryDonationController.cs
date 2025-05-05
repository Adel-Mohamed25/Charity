using Charity.Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MonetaryDonationController : BaseApiController
    {
    }
}
