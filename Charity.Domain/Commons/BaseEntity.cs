namespace Charity.Domain.Commons
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
