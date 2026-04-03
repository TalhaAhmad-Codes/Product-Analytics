using ProductAnalytics.Models.Common;

namespace ProductAnalytics.Models
{
    public sealed class Logs : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateOnly SellDate { get; set; }

        public Product Product { get; set; }
    }
}
