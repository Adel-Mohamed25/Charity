using Charity.Domain.Enum;

namespace Charity.Models.ResponseModels
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public OrderByDirection OrderByDirection { get; set; }
    }
}
