using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.ProductDTOs
{
    public sealed class ProductFilterDto : BaseFilterDto
    {
        public StockStatus? StockStatus { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
    }
}
