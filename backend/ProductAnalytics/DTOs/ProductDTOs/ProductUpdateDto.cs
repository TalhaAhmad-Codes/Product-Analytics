using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.ProductDTOs
{
    public sealed class ProductUpdateDto : BaseDto
    {
        public byte[] Image { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public StockStatus StockStatus { get; init; }
    }
}
