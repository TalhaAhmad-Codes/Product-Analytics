using ProductAnalytics.Enums;
using ProductAnalytics.Models.Common;

namespace ProductAnalytics.Models
{
    public sealed class Product : BaseEntity
    {
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public StockStatus StockStatus { get; set; }

        public ICollection<Logs> Logs { get; set; }
    }
}
