using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.ProductDTOs
{
    public sealed class ProductCreateDto
    {
        public byte[] Image { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public StockStatus StockStatus { get; init; } = StockStatus.InStock;
    }
}
